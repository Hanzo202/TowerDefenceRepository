using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    MainMenu,
    LoadState,
    CalmTime,
    WaveAttack,
    BossAttack,
    Pause,
    LoseGame,
    Victory,
    Game
}
public  class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private State _state;
    private CanvasController _canvas;
    private SpawnerEnemies _spawnerEnemies;
    private float _timeBetweenWave = 5;
    private bool _isPause;
    private State _tempState;

    [SerializeField]private int coins = 50;
    [SerializeField] private int wave = 0;
    [SerializeField] private int countEnemies = 20;

  
    public int enemyCountDestroyed = 0;
    public float countDown;

    public int Coins { get { return coins; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        countDown = _timeBetweenWave;
        _isPause = false;
    }
    private void Update()
    {      
        if (_state == State.CalmTime)
        {
            CalmTimeMethodInUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        _state = newState;
        switch (_state)
        {
            case State.MainMenu:
                break;
            case State.LoadState:
                StartCoroutine(Loading());
                break;
            case State.CalmTime:
                CalmTimeMethod();                
                break;
            case State.WaveAttack:
                if (_isPause)
                {
                    return;
                }
                WaveAttackMethod();
                break;
            case State.BossAttack:
                BossAttack();
                break;
            case State.Game:
                break;
            case State.Pause:
                break;
            case State.LoseGame:
                LoseGame();
                break;
            case State.Victory:
                Victory();
                break;
            default:
                break;
        }
    }

    private void CalmTimeMethod()
    {
        countDown = _timeBetweenWave;
        if (_tempState == State.CalmTime)
        {
            return;
        }
        AudioManager.Instance.PlaySfx("bell");
        if ((wave + 1) % 5 == 0)
        {
            _canvas.WaveText("BOSS");
        }
        else
        {
            _canvas.WaveText("Wave " + (wave + 1));
        }
        _canvas.CoundDownTextIsAvailable();
    }

    public void PauseButtonEsc()
    {       
        if (!_isPause)
        {
            _tempState = _state;
            ChangeState(State.Pause);
            Pause();
        }
        else
        {
            UnPause();
        }
        _isPause = !_isPause;
    }

    public void Pause()
    {
        _canvas.PausePanelIsActive();
        Time.timeScale = 0;
        AudioManager.Instance.sfxSource.Pause();
    }
    public void UnPause()
    {
        _canvas.PausePanelIsNotActive();
        Time.timeScale = 1;
        ChangeState(_tempState);
        AudioManager.Instance.sfxSource.UnPause();
    }

    public void CalmTimeMethodInUpdate()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            _canvas.CountDownText();
            return;
        }

        if ((wave + 1) % 5 == 0)
        {
            ChangeState(State.BossAttack);
        }
        else
        {
            ChangeState(State.WaveAttack);
        }
    }

    public void WaveAttackMethod()
    {
        AttackMethod(countEnemies);
    }

    public void BossAttack()
    {
        AttackMethod(1);
        AudioManager.Instance.PlayMusic("Boss");
    }

    public void GetCoins(int coins) 
    {
        this.coins+=coins;
        _canvas.CoinTextChanging();
    }

    public void GetCoinsForBuilding(int coins)
    {
        this.coins += coins/2;
        _canvas.CoinTextChanging();
    }

    public void EnemyWasDestroy()
    {       
        enemyCountDestroyed--;
        _canvas.EnemiesCountTextChanging();

        if (_state == State.LoseGame)
        {
            return;
        }

        if (enemyCountDestroyed == 0)
        {
            if (wave < _spawnerEnemies.enemiesCount - 1)
            {
                wave++;
                ChangeState(State.CalmTime);
            }
            else
            {
                ChangeState(State.Victory);
            }
        }
    }

    public bool CanBuildTower(Tower tower)
    {
        if (coins >= tower.Cost)
        {
            return true;
        }
        _canvas.AnnouncementText("You do not have enough money");
        return false;
    }


    public void BuyingTower(Tower tower)
    {
        coins -= tower.Cost;
        _canvas.CoinTextChanging();
    }

    public void AttackMethod(int count)
    {
        enemyCountDestroyed = count;
        _canvas.EnemiesCountTextChanging();
        _canvas.CoundDownTextIsAvailable();
        _spawnerEnemies.Spawn(wave, enemyCountDestroyed);
        ChangeState(State.Game);
    }

    public void LoseGame()
    {
        AudioManager.Instance.PlayMusic("Lose");
        Enemy[] enemis = FindObjectsOfType<Enemy>();
        Cannon[] cannons = FindObjectsOfType<Cannon>();
        _spawnerEnemies.gameObject.SetActive(false);
        _canvas.LoseGamePanel();
        foreach (Enemy enemy in enemis)
        {
            enemy.Idle();
            enemy.enabled = false;
        }
        foreach (Cannon cannon in cannons)
        {
            cannon.enabled = false;
        }
    }

    public void Victory()
    {
        _canvas.VictoryPanel();
        AudioManager.Instance.PlayMusic("Victory");
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(3);
        _spawnerEnemies = FindObjectOfType<SpawnerEnemies>();
        _canvas = FindObjectOfType<CanvasController>();
        _canvas.DisableTheLoadPanel();
        ChangeState(State.CalmTime);
        AudioManager.Instance.PlayMusic("Game");
    }
}

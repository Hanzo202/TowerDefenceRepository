using System.Collections;
using UnityEngine;
using Enemy;
using GameUI;
using System;

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
    public static GameManager Instance { get; private set; }


    private State state;
    private GameMenuSwitcher gameMenuSwitcher;
    private GUIController gUIController; 
    private SpawnerEnemies spawnerEnemies;
    private float timeBetweenWave = 5;
    private bool isPause;
    private State tempState;
    public int enemyCountDestroyed = 0;
    public float countDown;

    [SerializeField]private int coins = 50;
    [SerializeField] private int wave = 0;
    [SerializeField] private int countEnemies = 20;

  
    public int EnemyCountDestroyed => enemyCountDestroyed;
    public float CountDown => countDown;
    public int Coins => coins;

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
        countDown = timeBetweenWave;
        isPause = false;
    }
    private void Update()
    {      
        if (state == State.CalmTime)
            CalmTimeMethodInUpdate();      
    }

    public void ChangeState(State newState)
    {
        state = newState;
        switch (state)
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
                if (isPause)
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
                throw new InvalidOperationException("Wrong State!");
        }
    }

    private void CalmTimeMethod()
    {
        countDown = timeBetweenWave;
        if (tempState == State.CalmTime)
             return;

        AudioManager.Instance.PlaySfx("bell");

        if ((wave + 1) % 5 == 0)
        {
            gUIController.WaveText("BOSS");
        }
        else
        {
            gUIController.WaveText("Wave " + (wave + 1));
        }

        gUIController.CoundDownTextIsAvailable();
    }

    public void PauseButtonEsc()
    {       
        if (!isPause)
        {
            tempState = state;
            ChangeState(State.Pause);
            Pause();
        }
        else
        {
            UnPause();
        }

        isPause = !isPause;
    }

    public void Pause()
    {
        gameMenuSwitcher.PausePanelIsActive();
        Time.timeScale = 0;
        AudioManager.Instance.SfxSource.Pause();
    }
    public void UnPause()
    {
        gameMenuSwitcher.PausePanelIsNotActive();
        Time.timeScale = 1;
        ChangeState(tempState);
        AudioManager.Instance.SfxSource.UnPause();
    }

    public void CalmTimeMethodInUpdate()
    {
        if (CountDown > 0)
        {
            countDown -= Time.deltaTime;
            gUIController.CountDownText();
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

    public void WaveAttackMethod() => AttackMethod(countEnemies);

    public void BossAttack()
    {
        AttackMethod(1);
        AudioManager.Instance.PlayMusic("Boss");
    }

    public void GetCoins(int coins) 
    {
        this.coins+=coins;
        gUIController.CoinTextChanging();
    }

    public void GetCoinsForBuilding(int coins)
    {
        this.coins += coins/2;
        gUIController.CoinTextChanging();
    }

    public void EnemyWasDestroy()
    {       
        enemyCountDestroyed--;
        gUIController.EnemiesCountTextChanging();

        if (state == State.LoseGame)
            return;

        if (EnemyCountDestroyed != 0)
             return;
        
        if (wave < spawnerEnemies.EnemiesCount - 1)
        {
            wave++;
            ChangeState(State.CalmTime);
        }
        else
        {
            ChangeState(State.Victory);
        }
    }

    public bool CanBuildTower(Tower tower)
    {
        if (coins >= tower.Cost)
        {
            return true;
        }

        gUIController.AnnouncementText("You do not have enough money");
        return false;
    }


    public void BuyingTower(Tower tower)
    {
        coins -= tower.Cost;
        gUIController.CoinTextChanging();
    }

    public void AttackMethod(int count)
    {
        enemyCountDestroyed = count;
        gUIController.EnemiesCountTextChanging();
        gUIController.CoundDownTextIsAvailable();
        spawnerEnemies.Spawn(wave, EnemyCountDestroyed);
        ChangeState(State.Game);
    }

    public void LoseGame()
    {
        AudioManager.Instance.PlayMusic("Lose");
        EnemyMove[] enemis = FindObjectsOfType<EnemyMove>();
        Cannon[] cannons = FindObjectsOfType<Cannon>();
        spawnerEnemies.gameObject.SetActive(false);
        gameMenuSwitcher.LoseGamePanel();
        foreach (EnemyMove enemy in enemis)
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
        gameMenuSwitcher.VictoryPanel();
        AudioManager.Instance.PlayMusic("Victory");
    }

    private  IEnumerator Loading()
    {
        AudioManager.Instance.MusicSource.Stop();
        yield return new WaitForSeconds(3);

        spawnerEnemies = FindObjectOfType<SpawnerEnemies>();
        gUIController = FindObjectOfType<GUIController>();
        gameMenuSwitcher = FindObjectOfType<GameMenuSwitcher>();
        gameMenuSwitcher.DisableTheLoadPanel();
        ChangeState(State.CalmTime);
        AudioManager.Instance.PlayMusic("Game");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI enemiesCountText;
    [SerializeField] private TextMeshProUGUI announcementText;
    [SerializeField] private SpawnerEnemies spawner;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;


    private void Start()
    {
        loadPanel.SetActive(true);
    }

    public void CoundDownTextIsAvailable()
    {
        countDownText.enabled = !countDownText.isActiveAndEnabled;
    }
    public void CountDownText()
    {
        countDownText.text = Mathf.Round(GameManager.Instance.countDown).ToString();
    }

    public void  WaveText(string wave)
    {
        StartCoroutine(WaveTextCoroutine(wave));
    }

    IEnumerator  WaveTextCoroutine(string wave)
    {
        announcementText.text = wave;
        yield return new WaitForSeconds(5);
        announcementText.text = string.Empty;
    }

    public void CoinTextChanging()
    {
        coinText.text = GameManager.Instance.Coins.ToString();
    }
    public void EnemiesCountTextChanging()
    {
        enemiesCountText.text = GameManager.Instance.enemyCountDestroyed.ToString();
    }

    public void LoseGamePanel()
    {
        losePanel.SetActive(true);
    }

    public void VictoryPanel()
    {
        victoryPanel.SetActive(true);
    }

    public void AnnouncementText(string announcement)
    {
        StartCoroutine(AnnouncementTextCoroutine(announcement));
    }

    IEnumerator  AnnouncementTextCoroutine(string announcement)
    {
        announcementText.text = announcement;
        yield return new WaitForSeconds(1);
        announcementText.text = string.Empty;
    }
 
    public void ExitGame()
    {
        Application.Quit();
        print("Exit");
    }

    public void Settings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void PauseAndContinueButtonOnScreen()
    {
        GameManager.Instance.PauseButtonEsc();
    }
    public void PausePanelIsActive()
    {
        pausePanel.SetActive(true);
    }

    public void PausePanelIsNotActive()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            return;
        }
        settingsPanel.SetActive(false);
    }


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void VolumeMusic(float volume)
    {
        AudioManager.Instance.VolumeMusic(volume);
        if (AudioManager.Instance.musicSource.volume == 0)
        {
            musicToggle.isOn = true;
        }
        else
        {
            musicToggle.isOn = false;
        }
    }

    public void VolumeSFX(float volume)
    {
        AudioManager.Instance.VolumeSFX(volume);
        if (AudioManager.Instance.sfxSource.volume == 0)
        {
            sfxToggle.isOn = true;
        }
        else
        {
            sfxToggle.isOn = false;
        }
    }

    public void DisableTheLoadPanel()
    {
        loadPanel.SetActive(false);
    }
}

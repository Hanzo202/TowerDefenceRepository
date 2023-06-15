using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI startText;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            startText.gameObject.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.musicSource.Stop();
        GameManager.Instance.ChangeState(State.LoadState);
    }

    public void ExitGame()
    {
        Application.Quit();
        print("Exit");
    }

    public void Settings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        menuPanel.SetActive(true);
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
}

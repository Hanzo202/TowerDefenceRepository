using UnityEngine;

namespace GameUI 
{
    public class GameMenuSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject loadPanel;

        private void Start() => loadPanel.SetActive(true);
        public void LoseGamePanel() => losePanel.SetActive(true);

        public void VictoryPanel() => victoryPanel.SetActive(true);
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

        public void PauseAndContinueButtonOnScreen() => GameManager.Instance.PauseButtonEsc();
        public void PausePanelIsActive() => pausePanel.SetActive(true);

        public void PausePanelIsNotActive()
        {
            if (pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                return;
            }
            settingsPanel.SetActive(false);
        }

        public void DisableTheLoadPanel() => loadPanel.SetActive(false);

    }
}



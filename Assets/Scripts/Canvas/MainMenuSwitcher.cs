using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUI
{
    public class MainMenuSwitcher : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject settingsPanel;
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
    }
}


using System.Collections;
using TMPro;
using UnityEngine;

namespace GameUI
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI enemiesCountText;
        [SerializeField] private TextMeshProUGUI announcementText;

        private const float AnnouncementTextTimeDelay = 1f;
        private const float WaveTextTimeDelay = 5f;

        public void CoundDownTextIsAvailable() => countDownText.enabled = !countDownText.isActiveAndEnabled;

        public void CountDownText() => countDownText.text = Mathf.Round(GameManager.Instance.CountDown).ToString();


        public void WaveText(string wave) => StartCoroutine(WaveTextCoroutine(wave));

        private IEnumerator WaveTextCoroutine(string wave)
        {
            announcementText.text = wave;
            yield return new WaitForSeconds(WaveTextTimeDelay);
            announcementText.text = string.Empty;
        }

        public void CoinTextChanging() => coinText.text = GameManager.Instance.Coins.ToString();

        public void EnemiesCountTextChanging() => enemiesCountText.text = GameManager.Instance.EnemyCountDestroyed.ToString();



        public void AnnouncementText(string announcement) => StartCoroutine(AnnouncementTextCoroutine(announcement));

        private IEnumerator AnnouncementTextCoroutine(string announcement)
        {
            announcementText.text = announcement;
            yield return new WaitForSeconds(AnnouncementTextTimeDelay);
            announcementText.text = string.Empty;
        }

    }
}



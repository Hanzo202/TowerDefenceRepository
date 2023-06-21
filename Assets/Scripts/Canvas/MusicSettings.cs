using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class MusicSettings : MonoBehaviour
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle sfxToggle;



        public void ToggleMusicMute()
        {
            AudioManager.Instance.ToggleMusic();
        }

        public void ToggleSFXMute()
        {
            AudioManager.Instance.ToggleSFX();
        }

        public void VolumeMusic(float volume)
        {
            AudioManager.Instance.VolumeMusic(volume);
            if (AudioManager.Instance.MusicSource.volume == 0)
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
            if (AudioManager.Instance.SfxSource.volume == 0)
            {
                sfxToggle.isOn = true;
            }
            else
            {
                sfxToggle.isOn = false;
            }
        }
    }
}


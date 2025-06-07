using UnityEngine;
using UnityEngine.UI;

namespace Cubreak
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] AudioSource effectPlayer;
        [SerializeField] AudioSource backgroundPlayer;
        [SerializeField] Slider volumeSlider;

        private void Start()
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(volumeSlider.value);
        }

        private void SetVolume(float volume)
        {
            effectPlayer.volume = volume;
            backgroundPlayer.volume = volume;
        }

        public void Play(AudioClip clip)
        {
            effectPlayer.pitch = 1f;
            effectPlayer.clip = clip;
            effectPlayer.Play();
        }

        public void PlayPitch(AudioClip clip, int pitchLevel)
        {
            // 반음 차이를 배율로 변환: 2^(n/12)
            float pitch = Mathf.Pow(2f, pitchLevel / 12f);
            effectPlayer.pitch = pitch;
            effectPlayer.clip = clip;
            effectPlayer.Play();
        }

        public void PlayBackground(AudioClip clip)
        {
            backgroundPlayer.clip = clip;
            backgroundPlayer.Play();
        }
    } 
}

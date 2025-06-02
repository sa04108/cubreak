using UnityEngine;
using UnityEngine.UI;

namespace Cubreak
{
    public class AudioManager : Singleton<AudioManager>
    {
        [HideInInspector] public AudioSource listener;
        [SerializeField] Slider volumeSlider;

        protected override void Awake()
        {
            base.Awake();
            listener = GetComponent<AudioSource>();
        }

        private void Start()
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(volumeSlider.value);
        }

        private void SetVolume(float volume)
        {
            listener.volume = volume;
        }
    } 
}

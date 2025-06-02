using UnityEngine;
using UnityEngine.UI;

namespace Cubreak
{
    public class AudioSync : MonoBehaviour
    {
        [SerializeField] AudioClip audioClip;
        [SerializeField] bool playOnEnabled;

        AudioSource listener;

        // Start is called before the first frame update
        void Start()
        {
            listener = AudioManager.Instance.listener;
            Button button = GetComponent<Button>();
            if (button != null)
            {
                if (audioClip == null)
                {
                    audioClip = Resources.Load<AudioClip>("Sounds/Click");
                }

                button.onClick.AddListener(() =>
                {
                    listener.clip = audioClip;
                    listener.Play();
                });
            }
        }

        private void OnEnable()
        {
            if (listener == null)
                listener = AudioManager.Instance.listener;

            if (playOnEnabled)
            {
                listener.clip = audioClip;
                listener.Play();
            }
        }
    } 
}

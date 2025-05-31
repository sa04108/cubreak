using UnityEngine;
using UnityEngine.UI;

namespace Cublocks
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
                button.onClick.AddListener(() =>
                {
                    audioClip = Resources.Load<AudioClip>("Sounds/Click");
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

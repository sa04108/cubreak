using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cubreak
{
    public class AudioSync : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] AudioClip audioClip;
        [SerializeField] bool playOnEnabled;

        AudioSource listener;
        Button button;

        // Start is called before the first frame update
        void Start()
        {
            listener = AudioManager.Instance.listener;
            button = GetComponent<Button>();
            if (button != null)
            {
                if (audioClip == null)
                {
                    audioClip = Resources.Load<AudioClip>("Sounds/Click");
                }
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (button != null)
            {
                listener.clip = audioClip;
                listener.Play();
            }
        }
    } 
}

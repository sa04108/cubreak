using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cubreak
{
    public class AudioObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] bool playOnEnabled;
        [SerializeField] bool background;
        [SerializeField] AudioClip[] audioClips;

        Button button;

        private void OnEnable()
        {
            button = GetComponent<Button>();

            if (playOnEnabled)
            {
                PlayRandomClip();
            }
        }

        private void PlayRandomClip()
        {
            if (audioClips == null || audioClips.Length == 0)
                return;

            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];

            if (background)
                AudioManager.Instance.PlayBackground(clip);
            else
                AudioManager.Instance.Play(clip);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (button != null && button.interactable)
            {
                PlayRandomClip();
            }
        }
    } 
}

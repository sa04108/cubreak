using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSync : MonoBehaviour {
    [SerializeField] AudioClip audioClip;
    [SerializeField] bool playOnAwake;

    // Start is called before the first frame update
    void Start() {
        AudioSource listener = AudioManager.Instance.listener;
        Button button = GetComponent<Button>();
        if (button != null) {
            button.onClick.AddListener(() => {
                audioClip = Resources.Load<AudioClip>("Sounds/Click");
                listener.clip = audioClip;
                listener.Play();
            });
        }

        if (playOnAwake) {
            listener.clip = audioClip;
            listener.Play();
        }
    }
}

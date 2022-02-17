using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSync : MonoBehaviour {
    new AudioSource audio;

    // Start is called before the first frame update
    void Start() {
        audio = AudioManager.Instance.listener;
        AudioManager.Instance.audios.Add(this);
        Button button = GetComponent<Button>();
        if (button != null) {
            button.onClick.AddListener(() => {audio.clip = Resources.Load<AudioClip>("Sounds/Click"); audio.Play(); });
        }
        VolumeSync();
    }

    public void VolumeSync() {
        audio.volume = AudioManager.Instance.Volume;
    }
}

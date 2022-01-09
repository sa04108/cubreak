using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSync : MonoBehaviour {
    new AudioSource audio;

    private void Awake() {
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start() {
        AudioManager.Instance.audios.Add(this);
        Button button = GetComponent<Button>();
        if (button != null) audio.clip = Resources.Load<AudioClip>("Sounds/Click");
        VolumeSync();
    }

    public void VolumeSync() {
        audio.volume = AudioManager.Instance.Volume;
    }
}

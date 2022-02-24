using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager> {
    [HideInInspector] public AudioSource listener;
    [SerializeField] AudioSource bgm;
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Awake() {
        listener = GetComponent<AudioSource>();
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    void SetVolume(float volume) {
        listener.volume = volume;
        bgm.volume = volume;
    }
}

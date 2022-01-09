using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager> {
    public List<AudioSync> audios;
    [SerializeField] private Slider volumeSlider;

    private float volume;
    public float Volume { get => volume; }

    // Start is called before the first frame update
    void Awake() {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    void SetVolume(float volume) {
        this.volume = volume;

        for (int i = 0; i < audios.Count; i++) {
            if (audios[i] != null)
                audios[i].VolumeSync();
            else
                audios.RemoveAt(i--);

        }
    }
}

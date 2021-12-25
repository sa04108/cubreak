using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            return instance;
        }
    }

    public List<AudioSync> audios;
    public Slider volumeSlider;

    float volume;
    public float Volume { get => volume; }


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(ClickSound);
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    void SetVolume(float volume)
    {
        this.volume = volume;

        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i] != null)
                audios[i].VolumeSync();
            else
                audios.RemoveAt(i--);
            
        }
    }
}

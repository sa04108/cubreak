using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.audios.Add(this);
        VolumeSync();
    }

    public void VolumeSync()
    {
        GetComponent<AudioSource>().volume = AudioManager.Instance.Volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    Button[] buttons;
    AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        buttons = FindObjectsOfType<Button>();
        audio = GetComponent<AudioSource>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(ClickSound);
        }
    }

    void ClickSound()
    {
        audio.Play();
    }
}

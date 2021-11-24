using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public GameObject tutorialPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Newbie", 1) == 1)
        {
            tutorialPanel.SetActive(true);
            PlayerPrefs.SetInt("Newbie", 0);
        }
    }

    private void OnDisable()
    {
        tutorialPanel.SetActive(false);
    }
}

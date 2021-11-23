using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public GameObject tutorialPanel;

    // Start is called before the first frame update
    void Start()
    {
        tutorialPanel.SetActive(true);
    }

    private void OnDisable()
    {
        tutorialPanel.SetActive(false);
    }
}

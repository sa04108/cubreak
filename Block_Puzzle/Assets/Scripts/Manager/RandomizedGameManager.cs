using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedGameManager : GameManager
{
    private static RandomizedGameManager instance;
    public static RandomizedGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("assign new RandomizedGameManager to instance");
                instance = FindObjectOfType(typeof(RandomizedGameManager)) as RandomizedGameManager;
            }

            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }
}

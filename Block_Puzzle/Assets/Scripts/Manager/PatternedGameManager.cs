using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternedGameManager : GameManager
{
    private static PatternedGameManager instance;
    public static PatternedGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("assign new PatternedGameManager to instance");
                instance = FindObjectOfType(typeof(PatternedGameManager)) as PatternedGameManager;
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

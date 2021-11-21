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
                instance = FindObjectOfType(typeof(PatternedGameManager)) as PatternedGameManager;

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

        // DontDestroyOnLoad(gameObject);
    }

    public void BlocksResetColor()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != null)
            {
                blocks[i].GetComponent<PatternedBlock>().ResetColor();
            }
            else
                blocks.RemoveAt(i--);
        }
    }
}

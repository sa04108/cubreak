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
                instance = FindObjectOfType(typeof(RandomizedGameManager)) as RandomizedGameManager;

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
                blocks[i].GetComponent<RandomizedBlock>().ResetColor();
            }
            else
                blocks.RemoveAt(i--);
        }
    }
}

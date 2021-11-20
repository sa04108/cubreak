using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedGameManager : GameManager
{
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedBlock : Block
{
    RandomizedGameManager rGameManager;

    protected override void Start()
    {
        base.Start();
        ResetColor();
    }

    public void ResetColor()
    {
        int numOfBlockColor = blockGroupStatus.NumOfBlockColor;
        int colorVal = Random.Range(0, numOfBlockColor);

        switch (colorVal)
        {
            case 0:
                renderer.material.color = Color.red;
                break;
            case 1:
                renderer.material.color = Color.green;
                break;
            case 2:
                renderer.material.color = Color.blue;
                break;
            case 3:
                renderer.material.color = Color.yellow;
                break;
            case 4:
                renderer.material.color = Color.black;
                break;
            case 5:
                renderer.material.color = Color.white;
                break;
            default:
                break;
        }
    }

    public override void InitGameManager()
    {
        rGameManager = FindObjectOfType<RandomizedGameManager>();
        rGameManager.blocks.Add(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedBlock : Block, ISelectGameManager
{
    RandomizedGameManager rGameManager;

    protected override void Awake()
    {
        base.Awake();
        ResetColor();
    }

    public void ResetColor()
    {
        int numOfBlockColor = blockGroupStatus.NumOfBlockColor;
        int colorVal = Random.Range(0, numOfBlockColor);
        BlockColors color = new BlockColors();

        renderer.material.color = color.colors[colorVal];
    }

    public override void InitGameManager()
    {
        rGameManager = RandomizedGameManager.Instance;
        rGameManager.blocks.Add(gameObject);
    }
}

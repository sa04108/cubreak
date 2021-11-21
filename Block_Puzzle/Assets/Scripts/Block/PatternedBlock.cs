using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternedBlock : Block, ISelectGameManager
{
    PatternedGameManager pGameManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ResetColor();
    }

    public void ResetColor()
    {
        renderer.material.color = Color.black;
    }

    public override void InitGameManager()
    {
        pGameManager = PatternedGameManager.Instance;
        pGameManager.blocks.Add(gameObject);
    }
}

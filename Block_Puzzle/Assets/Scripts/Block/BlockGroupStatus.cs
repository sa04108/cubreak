using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupStatus : MonoBehaviour
{
    private int blockCount;
    public int BlockCount { get; set; }

    private int fallingBlockCount;
    public int FallingBlockCount { get; set; }

    private int unconnectedBlockCount;
    public int UnconnectedBlockCount { get; set; }

    private int numOfBlockColor;
    public int NumOfBlockColor
    {
        get => numOfBlockColor;
        set
        {
            numOfBlockColor = value;
            if (value < 1) numOfBlockColor = 1;
            else if (value > 6) numOfBlockColor = 6;
        }
    }

    private float blockFallingSpeed;
    public float BlockFallingSpeed
    {
        get => blockFallingSpeed;
        set
        {
            blockFallingSpeed = value;
            if (value < 1.0f) blockFallingSpeed = 1.0f;
            else if (value > 10.0f) blockFallingSpeed = 10.0f;
        }
    }
}

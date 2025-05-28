using UnityEngine;

public class BlockGroupStatus : Singleton<BlockGroupStatus>
{
    [SerializeField]
    private int blockCount;
    public int BlockCount { get => blockCount; set => blockCount = value; }

    [SerializeField]
    private int fallingBlockCount;
    public int FallingBlockCount { get => fallingBlockCount; set => fallingBlockCount = value; }

    [SerializeField]
    private int unconnectedBlockCount;
    public int UnconnectedBlockCount { get => unconnectedBlockCount; set => unconnectedBlockCount = value; }

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

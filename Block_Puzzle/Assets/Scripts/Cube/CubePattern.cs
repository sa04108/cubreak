using UnityEngine;

public class CubePattern : CubeMaterial
{
    public void SetPattern(int stageNum)
    {
        BlockColors color = new BlockColors();

        switch (stageNum)
        {
            case 1:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4 }, ENUM_COLOR.BLUE);
                break;
            case 2:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 3 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 4 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2 }, ENUM_COLOR.GREEN);
                break;
            case 3:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 3, 4, 7, 9 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 5, 8 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 6 }, ENUM_COLOR.YELLOW);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 4 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 5, 7 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 3, 8, 9 }, ENUM_COLOR.YELLOW);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 2, 8 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 4 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 3, 7, 9 }, ENUM_COLOR.YELLOW);
                break;
            case 4:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 5, 9 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 8 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 6 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3 }, ENUM_COLOR.MAGENTA);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 4, 6, 8 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 3, 5, 9 }, ENUM_COLOR.MAGENTA);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 7 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 4, 8 }, ENUM_COLOR.MAGENTA);
                break;
            case 5:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3, 7 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 5, 6 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 8, 9 }, ENUM_COLOR.YELLOW);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 5, 6, 8 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 4, 7 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 9 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1 }, ENUM_COLOR.YELLOW);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 8, 9 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 5 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 2, 3, 4, 7 }, ENUM_COLOR.YELLOW);
                break;
            case 6:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 7 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 5, 6 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3, 8, 9 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2 }, ENUM_COLOR.MAGENTA);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 5 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 6, 9 }, ENUM_COLOR.YELLOW);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 4 }, ENUM_COLOR.MAGENTA);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2 }, ENUM_COLOR.CYAN);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 8, 9 }, ENUM_COLOR.RED);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 5 }, ENUM_COLOR.GREEN);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 3 }, ENUM_COLOR.BLUE);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1 }, ENUM_COLOR.MAGENTA);
                break;
            default:
                Debug.Log("Default Stage");
                break;
        }
    }
}

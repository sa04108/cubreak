using UnityEngine;

namespace Cublocks
{
    public class CubeMaterial : MonoBehaviour
    {
        bool isAlpha;
        float alphaVal;

        private CubeBlocks cubeBlocks;

        private void Awake()
        {
            cubeBlocks = GetComponent<CubeBlocks>();
            isAlpha = false;
            alphaVal = 0.0f;
        }

        public void Set333CubeAlpha()
        {
            SetFloorAlpha(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);

            isAlpha = !isAlpha;
        }

        public void Set444CubeAlpha()
        {
            SetFloorAlpha(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(cubeBlocks.floors[3].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);

            isAlpha = !isAlpha;
        }

        /// <summary>
        /// list 에 들어갈 인덱스 번호는 1~9 입니다.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="positions"></param>
        /// <param name="color"></param>
        public void SetFloorAlpha(GameObject[] floor, int[] positions, float alpha)
        {
            foreach (var pos in positions)
            {
                if (floor[pos - 1] != null)
                {
                    floor[pos - 1].GetComponent<Block>().SetAlpha(alpha);
                }
            }
        }

        /// <summary>
        /// list 에 들어갈 인덱스 번호는 1~9 입니다.
        /// </summary>
        /// <param name="floors"></param>
        /// <param name="positions"></param>
        /// <param name="color"></param>
        public void SetFloorColor(int floor, int[] positions, ENUM_COLOR colorEnum)
        {
            foreach (int pos in positions)
            {
                if (cubeBlocks.floors[floor].floor[pos - 1] != null)
                {
                    cubeBlocks.floors[floor].floor[pos - 1].GetComponent<Block>().SetColor(colorEnum);
                }
            }
        }
    } 
}

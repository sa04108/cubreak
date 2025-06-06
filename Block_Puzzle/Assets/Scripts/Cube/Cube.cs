using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cubreak
{
	[System.Serializable]
	public class Floor
	{
		public GameObject[] floor;
	}

	public class Cube : MonoBehaviour
	{
		public List<Floor> floors = new List<Floor>();
		public int dimension => floors.Count;

        bool isAlpha;
        float alphaVal;

        private void Awake()
        {
            isAlpha = false;
            alphaVal = 0.0f;
        }

        public void InitializeCube(CubeStage stage = null)
        {
            if (stage != null)
            {
                foreach (var layer in stage.Layers)
                {
                    foreach (var arrangement in layer.Arrangements)
                    {
                        foreach (int pos in arrangement.Positions)
                        {
                            if (floors[layer.Index].floor[pos - 1] != null)
                            {
                                var block = floors[layer.Index].floor[pos - 1].GetComponent<Block>();
                                block.SetCoord((pos - 1) % dimension, (pos - 1) / dimension, layer.Index);
                                block.SetColor(arrangement.Color);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var floor in floors)
                {
                    foreach (var block in floor.floor)
                    {
                        block.GetComponent<Block>().SetColor(null);
                    }
                }
            }
        }

        public (int, int, int) MoveBlockDown((int, int, int) coord)
        {
            int x = coord.Item1;
            int y = coord.Item2;
            int z = coord.Item3;

            if (z - 1 < 0)
            {
                Debug.LogWarning("Cannot move block since the block is at bottom.");
                return coord;
            }

            var blockObj = floors[z].floor[x + y * dimension];
            if (blockObj == null)
            {
                Debug.LogWarning("Block is null.");
                return coord;
            }

            floors[z - 1].floor[x + y * dimension] = blockObj;
            floors[z].floor[x + y * dimension] = null;

            return (x, y, z - 1);
        }

        public void Set333CubeAlpha()
        {
            SetFloorAlpha(floors[0].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(floors[1].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(floors[2].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);

            isAlpha = !isAlpha;
        }

        public void Set444CubeAlpha()
        {
            SetFloorAlpha(floors[0].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(floors[1].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(floors[2].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
            SetFloorAlpha(floors[3].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);

            isAlpha = !isAlpha;
        }

        /// <summary>
        /// list 에 들어갈 인덱스 번호는 1~9 입니다.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="positions"></param>
        /// <param name="color"></param>
        private void SetFloorAlpha(GameObject[] floor, int[] positions, float alpha)
        {
            foreach (var pos in positions)
            {
                if (floor[pos - 1] != null)
                {
                    floor[pos - 1].GetComponent<Block>().SetAlpha(alpha);
                }
            }
        }

        public void RevealHintBlocks()
        {
            if (BlockWatcher.Instance.FallingBlockCount != 0)
                return;

            int N = dimension;
            int[,,] grid = new int[N, N, N];

            for (int x = 0; x < N; x++)
                for (int y = 0; y < N; y++)
                    for (int z = 0; z < N; z++)
                    {
                        var blockObj = floors[z].floor[x + y * N];
                        if (blockObj != null)
                        {
                            var block = blockObj.GetComponent<Block>();
                            grid[x, y, z] = block.ColorIndex + 1;
                            block.SetHintAnimation(false);
                        }
                    }

            if (StageUtility.Solve(grid, out var hintBlocks))
            {
                foreach (var coord in hintBlocks)
                {
                    int x = coord.Item1;
                    int y = coord.Item2;
                    int z = coord.Item3;
                    floors[z].floor[x + y * N].GetComponent<Block>().SetHintAnimation(true);
                }
            }
            else
            {
                Debug.Log("This stage is not clearable now.");
            }
        }

        [Button]
        private void ExportAsJson()
        {
            var stage = new CubeStage()
            {
                Id = 0,
                Dimension = dimension,
                Layers = new()
            };

            int N = dimension;
            int[,,] grid = new int[N, N, N];

            for (int x = 0; x < N; x++)
                for (int y = 0; y < N; y++)
                    for (int z = 0; z < N; z++)
                    {
                        var blockObj = floors[z].floor[x + y * N];
                        if (blockObj == null)
                        {
                            Debug.LogWarning("Block must be not empty.");
                            return;
                        }

                        var block = blockObj.GetComponent<Block>();
                        grid[x, y, z] = block.ColorIndex + 1;
                    }

            stage.ApplyGrid(grid);

            var stageStr = JsonConvert.SerializeObject(stage, Formatting.Indented);
            File.WriteAllText(Path.Combine(Application.dataPath, "Resources/stage_data_ex.json"), stageStr);
        }
    }
}

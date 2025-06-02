using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Cubreak
{
    public class BlockWatcher : Singleton<BlockWatcher>
    {
        private Cube cube;
        private List<Block> blocks = new();

        [ShowInInspector]
        public int BlockCount => blocks.Count;

        [ReadOnly, SerializeField]
        private int fallingBlockCount;
        public int FallingBlockCount { get => fallingBlockCount; set => fallingBlockCount = value; }

        [ReadOnly, SerializeField]
        private int unconnectedBlockCount;
        public int UnconnectedBlockCount { get => unconnectedBlockCount; set => unconnectedBlockCount = value; }

        private void Update()
        {
            InputManager.Instance.Slide();
            if (InputManager.Instance.Click() && FallingBlockCount == 0)
            {
                Ray ray = Camera.main?.ScreenPointToRay(Input.mousePosition) ?? default;
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100.0f, 1 << 6))
                {
                    hit.transform.GetComponent<Block>().DestroyBlocks();
                    SetBlocksHintOff();
                }
            }

            CheckBlockStatus();
            CheckGameClear();
            CheckGameOver();
        }

        public void Initialize(Cube cube)
        {
            this.cube = cube;
            blocks.Clear();
            FallingBlockCount = 0;
            UnconnectedBlockCount = 0;
        }

        public void Subscribe(Block block)
        {
            blocks.Add(block);
        }

        public void Unsubscribe(Block block)
        {
            blocks.Remove(block);
        }

        public (int, int, int) OnBlockMovedDown(Block block)
        {
            return cube.MoveBlockDown(block.Coord);
        }

        private void CheckBlockStatus()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null)
                {
                    if (!blocks[i].IsFalling)
                    {
                        blocks[i].MoveDown();
                        blocks[i].CheckmateCheck();
                    }
                }
            }
        }

        private void CheckGameClear()
        {
            if (BlockCount == 0
                && UnconnectedBlockCount == 0
                && FallingBlockCount == 0)
            {
                UIManager.Instance.GameClear();
            }
        }

        private void CheckGameOver()
        {
            if (BlockCount > 0
                && BlockCount == UnconnectedBlockCount
                && FallingBlockCount == 0)
            {
                UIManager.Instance.GameOver();
            }
        }

        private void SetBlocksHintOff()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null)
                {
                    blocks[i].SetHintAnimation(false);
                }
            }
        }
    } 
}

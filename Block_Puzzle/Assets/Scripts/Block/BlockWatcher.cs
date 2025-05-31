using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Cublocks
{
    public class BlockWatcher : Singleton<BlockWatcher>
    {
        private List<Block> blocks = new();

        [ShowInInspector]
        public int BlockCount => blocks.Count;

        [ReadOnly, SerializeField]
        private int fallingBlockCount;
        public int FallingBlockCount { get => fallingBlockCount; set => fallingBlockCount = value; }

        [ReadOnly, SerializeField]
        private int unconnectedBlockCount;
        public int UnconnectedBlockCount { get => unconnectedBlockCount; set => unconnectedBlockCount = value; }

        private void OnEnable()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            InputManager.Instance.Slide();
            if (InputManager.Instance.Click() && FallingBlockCount == 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100.0f, 1 << 6))
                {
                    hit.transform.GetComponent<Block>().DestroyBlocks();
                }
            }

            BlocksMoveDownCheck();
            BlocksCheckmateCheck();
            GameClearCheck();
            GameOverCheck();
        }

        public void Initialize()
        {
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

        private void BlocksMoveDownCheck()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null)
                {
                    if (!blocks[i].IsFalling)
                        blocks[i].MoveDown();
                }
            }
        }

        private void BlocksCheckmateCheck()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null)
                {
                    if (!blocks[i].IsFalling)
                        blocks[i].CheckmateCheck();
                }
            }
        }

        private void GameClearCheck()
        {
            if (BlockCount == 0
                && UnconnectedBlockCount == 0
                && FallingBlockCount == 0)
            {
                UIManager.Instance.GameClear();
            }
        }

        private void GameOverCheck()
        {
            if (BlockCount > 0
                && BlockCount == UnconnectedBlockCount
                && FallingBlockCount == 0)
            {
                UIManager.Instance.GameOver();
            }
        }
    } 
}

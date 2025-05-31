using System.Collections;
using UnityEngine;

namespace Cublocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] GameObject destroyEffect;

        private BlockWatcher blockWatcher => BlockWatcher.Instance;
        private UIManager uiManager => UIManager.Instance;
        private new Renderer renderer => GetComponent<Renderer>();

        private const float maxRayDistance = 1.0f;

        private Vector3 targetPos;

        private bool isFalling = false;
        public bool IsFalling { get => isFalling; }
        private bool isUnconnected = false;
        private float fallingSpeed;

        private bool destroyed;

        private Vector3[] rayCastVec
        {
            get
            {
                Vector3[] _rayCastVec = new Vector3[6];
                _rayCastVec[0] = Vector3.forward;
                _rayCastVec[1] = Vector3.back;
                _rayCastVec[2] = Vector3.left;
                _rayCastVec[3] = Vector3.right;
                _rayCastVec[4] = Vector3.up;
                _rayCastVec[5] = Vector3.down;
                return _rayCastVec;
            }
        }

        private void Start()
        {
            blockWatcher.Subscribe(this);
            fallingSpeed = CustomPlayerPrefs.GetFloat(ENUM_PLAYERPREFS.BlockFallingSpeed);

            MoveDown();
            targetPos = transform.position;

            isUnconnected = false; // 주변에 같은 색으로 연결될 수 있는 블럭이 없는 경우 true
            destroyed = false;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * fallingSpeed);
        }

        public void SetColor(ENUM_COLOR? color)
        {
            if (color.HasValue)
            {
                renderer.material.color = BlockColors.colors[(int)color];
            }
            else
            {
                int numOfBlockColor = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.NumOfBlockColor);
                int colorVal = Random.Range(0, numOfBlockColor);
                renderer.material.color = BlockColors.colors[colorVal];
            }
        }

        public void SetAlpha(float alpha)
        {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }

        private bool CompareColor(Renderer r1, Renderer r2)
        {
            // 이 함수는 alpha 값을 제외하고 색을 비교하는 함수입니다.
            // 색이 같은 경우 true, 다른 경우 false를 반환합니다.

            if (r1.material.color.r == r2.material.color.r
                && r1.material.color.g == r2.material.color.g
                && r1.material.color.b == r2.material.color.b)
                return true;

            return false;
        }

        public void MoveDown()
        {
            StartCoroutine(CoMoveDown());
        }

        private IEnumerator CoMoveDown()
        {
            while (!Physics.Raycast(transform.position, Vector3.down, maxRayDistance))
            {
                targetPos += Vector3.down;

                if (!isFalling)
                {
                    blockWatcher.FallingBlockCount++;
                    isFalling = true;
                }

                yield return new WaitUntil(() => transform.position.y - targetPos.y <= 0.1f);
            }

            if (isFalling)
            {
                blockWatcher.FallingBlockCount--;
                isFalling = false;
            }
        }

        public void CheckmateCheck()
        {
            RaycastHit hit;

            for (int i = 0; i < rayCastVec.Length; i++)
            {
                if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance, 1 << 6))
                {
                    if (CompareColor(hit.transform.GetComponent<Renderer>(), renderer))
                    {
                        if (isUnconnected)
                            blockWatcher.UnconnectedBlockCount--;

                        isUnconnected = false;
                        return;
                    }
                }
            }

            // 6방향으로 RayCast 후 Block과 부딪치지 않았거나, 부딪친 Block과 색이 다른 경우
            if (!isUnconnected)
                blockWatcher.UnconnectedBlockCount++;

            isUnconnected = true;
        }

        public void DestroyBlocks()
        {
            if (destroyed) return;

            RaycastHit hit;

            for (int i = 0; i < rayCastVec.Length; i++)
            {
                if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance, 1 << 6))
                {
                    if (CompareColor(hit.transform.GetComponent<Renderer>(), renderer))
                    {
                        destroyed = true;
                        hit.transform.GetComponent<Block>().DestroyBlocks();

                        // 맨 끝 블럭의 경우 다시 이 함수가 실행되지 않으므로 따로 제거해주어야 한다.
                        // 맨 끝 블럭이 아닌 경우 이미 위에서 Destory 처리가 됐을 것이므로 예외처리를 한다.
                    }
                }
            }

            if (destroyed) DestroyAndScore();
        }

        private void DestroyAndScore()
        {
            uiManager.ScoreUp();
            Destroy(Instantiate(destroyEffect, transform.position, Quaternion.identity, transform.parent), 0.8f);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            blockWatcher?.Unsubscribe(this);
        }
    } 
}

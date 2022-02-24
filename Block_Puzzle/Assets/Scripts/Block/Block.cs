using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlockType {
    [SerializeField] GameObject destroyEffect;

    ENUM_BLOCK_TYPE blockType;

    private BlockGroupStatus blockGroupStatus;
    new private Renderer renderer;
    private UIManager uiManager;
    private GameManager gameManager;

    private const float maxRayDistance = 1.0f;

    private Vector3 targetPos;

    private bool isFalling;
    public bool IsFalling { get => isFalling; }

    private bool isUnconnected;
    private bool destroyed;

    private Vector3[] rayCastVec {
        get {
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

    // Start is called before the first frame update
    private void Awake() {
        blockGroupStatus = BlockGroupStatus.Instance;
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
    }

    private void Start() {
        blockGroupStatus.BlockCount++;
        renderer = GetComponent<Renderer>();

        isFalling = true;
        blockGroupStatus.FallingBlockCount++;

        StartCoroutine(MoveDown());
        targetPos = transform.localPosition;

        isUnconnected = false; // 주변에 같은 색으로 연결될 수 있는 블럭이 없는 경우 true
        destroyed = false;

        SelectBlockType();
        gameManager.blocks.Add(gameObject);
    }

    // Update is called once per frame
    private void Update() {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, Time.deltaTime * blockGroupStatus.BlockFallingSpeed);
    }

    public void SelectBlockType() {
        blockType = gameManager.blockType;
        if (blockType == ENUM_BLOCK_TYPE.UNDEFINED) {
            Debug.LogError("Block Type이 지정되지 않았습니다.");
            return;
        }
        ResetBlockColor();
    }

    public void ResetBlockColor() {
        if (blockType == ENUM_BLOCK_TYPE.RANDOMIZED) {
            int numOfBlockColor = blockGroupStatus.NumOfBlockColor;
            int colorVal = Random.Range(0, numOfBlockColor);

            renderer.material.color = BlockColors.colors[colorVal];
        }
        else {
            return;
        }
    }

    private bool CompareColor(Renderer r1, Renderer r2) {
        // 이 함수는 alpha 값을 제외하고 색을 비교하는 함수입니다.
        // 색이 같은 경우 true, 다른 경우 false를 반환합니다.

        if (r1.material.color.r == r2.material.color.r
            && r1.material.color.g == r2.material.color.g
            && r1.material.color.b == r2.material.color.b)
            return true;

        return false;
    }

    public IEnumerator MoveDown() {
        while (!Physics.Raycast(transform.position, Vector3.down, maxRayDistance)) {
            targetPos += Vector3.down;

            if (!isFalling) {
                blockGroupStatus.FallingBlockCount++;
                isFalling = true;
            }

            yield return new WaitUntil(() => transform.localPosition.y - targetPos.y <= 0.1f);
        }

        if (isFalling) {
            blockGroupStatus.FallingBlockCount--;
            isFalling = false;
        }
    }

    public void CheckmateCheck() {
        RaycastHit hit;

        for (int i = 0; i < rayCastVec.Length; i++) {
            if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance, 1 << 6)) {
                if (CompareColor(hit.transform.GetComponent<Renderer>(), renderer)) {
                    if (isUnconnected)
                        blockGroupStatus.UnconnectedBlockCount--;

                    isUnconnected = false;
                    return;
                }
            }
        }

        // 6방향으로 RayCast 후 Block과 부딪치지 않았거나, 부딪친 Block과 색이 다른 경우
        if (!isUnconnected)
            blockGroupStatus.UnconnectedBlockCount++;

        isUnconnected = true;
    }

    public void DestroyBlocks() {
        if (destroyed) return;

        RaycastHit hit;

        for (int i = 0; i < rayCastVec.Length; i++) {
            if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance, 1 << 6)) {
                if (CompareColor(hit.transform.GetComponent<Renderer>(), renderer)) {
                    destroyed = true;
                    hit.transform.GetComponent<Block>().DestroyBlocks();

                    // 맨 끝 블럭의 경우 다시 이 함수가 실행되지 않으므로 따로 제거해주어야 한다.
                    // 맨 끝 블럭이 아닌 경우 이미 위에서 Destory 처리가 됐을 것이므로 예외처리를 한다.
                }
            }
        }

        if (destroyed) DestroyThis();
    }

    private void DestroyThis() {
        uiManager.ScoreUp();
        blockGroupStatus.BlockCount--;
        Destroy(Instantiate(destroyEffect, transform.position, Quaternion.identity, transform.parent), 0.8f);
        Destroy(gameObject);
    }
}

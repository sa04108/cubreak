using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    GameManager gameManager;
    BlockGroupStatus blockGroupStatus;
    Renderer renderer;
    const float maxRayDistance = 1.0f;

    Vector3 targetPos;
    
    private bool isFalling;
    public bool IsFalling { get => isFalling; }

    [SerializeField]
    bool isUnconnected;
    bool destroyed;

    private Vector3[] rayCastVec {
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

    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.blocks.Add(gameObject);
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();
        blockGroupStatus.BlockCount++;

        renderer = GetComponent<Renderer>();

        isFalling = true;
        blockGroupStatus.FallingBlockCount++;
        StartCoroutine(MoveDown());
        targetPos = transform.localPosition;

        isUnconnected = false; // 주변에 같은 색으로 연결될 수 있는 블럭이 없는 경우 true
        destroyed = false;

        ResetColor();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, Time.deltaTime * blockGroupStatus.BlockFallingSpeed);
    }

    void ResetColor()
    {
        int numOfBlockColor = blockGroupStatus.NumOfBlockColor;
        int colorVal = Random.Range(0, numOfBlockColor);

        switch (colorVal)
        {
            case 0:
                renderer.material.color = Color.red;
                break;
            case 1:
                renderer.material.color = Color.green;
                break;
            case 2:
                renderer.material.color = Color.blue;
                break;
            case 3:
                renderer.material.color = Color.yellow;
                break;
            case 4:
                renderer.material.color = Color.black;
                break;
            case 5:
                renderer.material.color = Color.white;
                break;
            default:
                break;
        }
    }

    public IEnumerator MoveDown()
    {
        while (!Physics.Raycast(transform.position, Vector3.down, maxRayDistance))
        {
            targetPos += Vector3.down;

            if (!isFalling)
            {
                blockGroupStatus.FallingBlockCount++;
                isFalling = true;
            }

            yield return new WaitUntil(() => transform.localPosition.y - targetPos.y <= 0.1f);
        }

        if (isFalling)
        {
            blockGroupStatus.FallingBlockCount--;
            isFalling = false;
        }
    }

    public void CheckmateCheck()
    {
        RaycastHit hit;

        for (int i = 0; i < rayCastVec.Length; i++)
        {
            if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance))
            {
                if (hit.transform.CompareTag("Block")
                    && hit.transform.GetComponent<Renderer>().material.color == renderer.material.color)
                {
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

    public void DestroyBlocks()
    {
        if (destroyed) return;

        RaycastHit hit;

        for (int i = 0; i < rayCastVec.Length; i++)
        {
            if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance))
            {
                if (hit.transform.CompareTag("Block")
                    && hit.transform.GetComponent<Renderer>().material.color == renderer.material.color)
                {
                    destroyed = true;
                    hit.transform.GetComponent<Block>().DestroyBlocks();

                    // 맨 끝 블럭의 경우 다시 이 함수가 실행되지 않으므로 따로 제거해주어야 한다.
                    // 맨 끝 블럭이 아닌 경우 이미 위에서 Destory 처리가 됐을 것이므로 예외처리를 한다.
                }
            }
        }

        if (destroyed) DestroyThis();
    }

    private void DestroyThis()
    {
        blockGroupStatus.BlockCount--;
        Destroy(Instantiate(destroyEffect, transform.position, Quaternion.identity, transform.parent), 0.8f);
        Destroy(gameObject);
    }
}

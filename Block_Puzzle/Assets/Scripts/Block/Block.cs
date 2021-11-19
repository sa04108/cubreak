using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    GameManager gameManager;
    BlockGroupStatus blockGroupStatus;
    Renderer renderer;
    const float maxRayDistance = 1.0f;

    Coroutine prevCoroutine;
    Coroutine nowCoroutine;
    Vector3 targetPos;
    bool isFalling;

    [SerializeField]
    bool isUnconnected;

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
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();
        blockGroupStatus.BlockCount++;

        renderer = GetComponent<Renderer>();

        targetPos = transform.localPosition;
        isFalling = false;
        isUnconnected = false; // 주변에 같은 색으로 연결될 수 있는 블럭이 없는 경우 true

        ResetColor();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyCheck();

        if (!isFalling)
            nowCoroutine = StartCoroutine(MoveDown());

        if (Input.GetMouseButtonDown(0) && blockGroupStatus.FallingBlockCount == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Block"))
                    hit.transform.GetComponent<Block>().DestroyBlocks();
            }

            CheckmateCheck();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, Time.deltaTime * blockGroupStatus.BlockFallingSpeed);
    }

    IEnumerator MoveDown()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, maxRayDistance))
        {
            if (prevCoroutine != null)
                StopCoroutine(prevCoroutine);
            targetPos += Vector3.down;

            if (!isFalling)
                blockGroupStatus.FallingBlockCount++;
            isFalling = true;

            yield return new WaitUntil(() => transform.localPosition.y - targetPos.y <= 0.1f);

            prevCoroutine = nowCoroutine;
            nowCoroutine = StartCoroutine(MoveDown());

            yield return new WaitUntil(() => prevCoroutine == null);

            blockGroupStatus.FallingBlockCount--;
            isFalling = false;
        }

        prevCoroutine = null;
    }

    public void DestroyCheck()
    {
        if (gameObject.CompareTag("Destroyed"))
        {
            blockGroupStatus.BlockCount--;
            gameManager.ScoreUp();

            if (transform.parent.childCount <= 1)
            {
                Destroy(transform.parent.gameObject);
            }
            else
                Destroy(gameObject);
        }
    }

    private void CheckmateCheck()
    {
        RaycastHit hit;

        for (int i = 0; i < rayCastVec.Length; i++)
        {
            if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance))
            {
                if ((hit.transform.CompareTag("Block") || hit.transform.CompareTag("Destroyed"))
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

    public void DestroyBlocks()
    {
        RaycastHit hit;

        for (int i = 0; i < rayCastVec.Length; i++)
        {
            if (Physics.Raycast(transform.position, rayCastVec[i], out hit, maxRayDistance))
            {
                if (hit.transform.CompareTag("Block")
                    && hit.transform.GetComponent<Renderer>().material.color == renderer.material.color)
                {
                    Destroy(Instantiate(destroyEffect, hit.transform.position, Quaternion.identity, transform.parent), 0.8f);
                    hit.transform.tag = "Destroyed";
                    hit.transform.GetComponent<Block>().DestroyBlocks();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    GameManager gameManager;
    Renderer renderer;
    float maxRayDistance = 1.0f;

    Vector3 targetPos;
    bool isFalling;

    [SerializeField]
    bool isUnconnected;

    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.BlockCount++;
        renderer = GetComponent<Renderer>();

        targetPos = transform.localPosition;
        isFalling = false;
        isUnconnected = false; // 주변에 같은 색으로 연결될 수 있는 블럭이 없는 경우 true

        ResetColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Destroyed"))
        {
            gameManager.BlockCount--;
            gameManager.ScoreUp();

            if (transform.parent.childCount <= 1)
            {
                Destroy(transform.parent.gameObject);
            }
            else
                Destroy(gameObject);
        }

        if (!isFalling)
            StartCoroutine(MoveDown());

        if (Input.GetMouseButtonDown(0) && gameManager.FallingBlockCount == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Block"))
                    hit.transform.GetComponent<Block>().DestroyBlocks();
            }

            CheckCheckmate();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, Time.deltaTime * 5.0f);
    }

    IEnumerator MoveDown()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, maxRayDistance))
        {
            targetPos += Vector3.down;

            gameManager.FallingBlockCount++;
            isFalling = true;

            yield return new WaitForSeconds(0.25f);

            gameManager.FallingBlockCount--;
            isFalling = false;
        }
    }

    public void CheckCheckmate()
    {
        RaycastHit hit;

        for (int i = 0; i < gameManager.RayCastVec.Length; i++)
        {
            if (Physics.Raycast(transform.position, gameManager.RayCastVec[i], out hit, maxRayDistance))
            {
                if ((hit.transform.CompareTag("Block") || hit.transform.CompareTag("Destroyed"))
                    && hit.transform.GetComponent<Renderer>().material.color == renderer.material.color)
                {
                    if (isUnconnected)
                        gameManager.UnconnectedBlockCount--;

                    isUnconnected = false;
                    return;
                }
            }
        }

        // 6방향으로 RayCast 후 Block과 부딪치지 않았거나, 부딪친 Block과 색이 다른 경우
        if (!isUnconnected)
            gameManager.UnconnectedBlockCount++;

        isUnconnected = true;
    }

    void ResetColor()
    {
        int numOfBlockColor = gameManager.NumOfBlockColor;
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

        for (int i = 0; i < gameManager.RayCastVec.Length; i++)
        {
            if (Physics.Raycast(transform.position, gameManager.RayCastVec[i], out hit, maxRayDistance))
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

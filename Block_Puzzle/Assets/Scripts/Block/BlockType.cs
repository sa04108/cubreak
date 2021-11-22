using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour
{
    public GameObject destroyEffect;

    // Block Type 지정은 반드시 Block 객체가 인스턴스화 되기 전에 미리 되어있어야 합니다.
    void Awake()
    {
        if (PatternedGameManager.Instance.gameObject.activeSelf)
            gameObject.AddComponent<PatternedBlock>();
        else
            gameObject.AddComponent<RandomizedBlock>();
    }
}

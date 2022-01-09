using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = default(T);
    public static T Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType(typeof(T)) as T;
            return instance;
        }
    }

    private void Awake() {
        if (instance == null) instance = GetComponent<T>();
        else Destroy(gameObject);
    }
}

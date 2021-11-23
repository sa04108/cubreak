using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    int distance;

    public void SetCameraDistance(int distance)
    {
        this.distance = 6 + distance;
        transform.position = new Vector3(-this.distance, 0.0f, -this.distance);
    }
}

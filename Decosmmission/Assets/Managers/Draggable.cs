using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    float a = 0;
    void Update()
    {
        gameObject.transform.position = new Vector3(30 * Mathf.Cos(a), 30 * Mathf.Sin(a), 0);
        a += 0.01f;
    }
}

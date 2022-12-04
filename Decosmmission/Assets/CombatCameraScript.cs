using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraScript : MonoBehaviour
{
    public static CombatCameraScript instance;
    public Camera Cam;
    public Transform target;
    Vector3 offset;
    public Vector3 baseoffset;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.5f);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            offset = Vector3.Normalize(Cam.ScreenToWorldPoint(Input.mousePosition)-target.position)*10 + baseoffset;
        }
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            offset = baseoffset;
        }
    }


}

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
        transform.position = SnapToPixels(Vector3.Lerp(transform.position, target.position + offset, 0.5f));
        if (Input.GetKey(KeyCode.Mouse1))
        {
            offset = Vector3.Normalize(Cam.ScreenToWorldPoint(Input.mousePosition)-target.position)*10 + baseoffset;
        }
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            offset = baseoffset;
        }
    }
    public static Vector3 SnapToPixels(Vector3 vector3)
    {
        return new Vector3(
            Mathf.Round(vector3.x * 32) / 32,
            Mathf.Round(vector3.y * 32) / 32,
            Mathf.Round(vector3.z * 32) / 32
            );
    }


}

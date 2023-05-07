using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePointer : MonoBehaviour
{
    GameObject Boss;
    public float explosionTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer -= Time.deltaTime;
        if (explosionTimer < 0)
        {
            foreach(RaycastHit2D raycastHit in Physics2D.CircleCastAll(transform.position, 1, Vector2.zero))
            {
                BaseEntity hit = raycastHit.rigidbody.gameObject.GetComponent<BaseEntity>();
                if (hit != null)
                {
                    hit.TakeDamage(Boss, 20);
                }
            }
            GameObject.Destroy(gameObject);
        }
    }
}

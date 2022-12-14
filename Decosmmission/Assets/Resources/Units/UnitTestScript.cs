using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitTestScript : Unit
{
    int state;
    float Timer;
    Vector3 dashpos;
    protected override void SetDefaults()
    {
        detectionRange = 10;
        MaxHP = 30;
        CurrentHP = 30;
        state = 0;
    }
    // Update is called once per frame
    protected override void AI()
    {
        switch (state)
        {
            case 0:
                rb.velocity = (savedposition - transform.position).magnitude > 0.1 ? (savedposition - transform.position).normalized * 5 : new Vector3(0, 0, 0);
                if (Target != null && (savedposition - transform.position).magnitude < 5)
                {
                    state = 1;
                }
                break;
            case 1:
                Timer = 1;
                state = 2;
                dashpos = savedposition;
                break;
            case 2:
                Timer -= Time.deltaTime;
                rb.velocity = (dashpos - transform.position).normalized * -1;
                if(Timer <= 0)
                    state = 3;
                break;
            case 3:
                Timer = 1;
                state = 4;
                rb.velocity = (dashpos - transform.position).normalized * 10;
                break;
            case 4:
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                    state = 0;
                break;
            case 5:
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                    state = 0;
                break;
        }
    }
    protected override void FixedAI()
    {
        if(state == 4)
            foreach(var i in Physics2D.CircleCastAll(transform.position, 1, dashpos, 0.1f,LayerMask.GetMask("Player")))
            {
                i.collider.gameObject.GetComponent<BaseEntity>().TakeDamage(this.gameObject,10);
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 7 && state == 4)
        {
            Debug.Log("Collide");
            rb.velocity = rb.velocity / -10;
            state = 5;
            Timer = 2;
        }
    }


}

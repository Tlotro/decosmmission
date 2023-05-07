using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HelicopterBoss : Unit
{
    int state;
    float Timer;
    Vector3 dashpos;
    Vector3 newScale;
    [SerializeField]
    GameObject leftGun;
    [SerializeField]
    GameObject rightGun;
    GameObject bullet;
    float PhaseTimer;
    protected override void SetDefaults()
    {
        newScale = transform.localScale;
        detectionRange = 60;
        MaxHPBase = 500;
        MaxHPmultiplyer = 1;
        state = 1;
        bullet = Resources.Load<GameObject>("Units/Turret/TurretLaser");
    }
    // Update is called once per frame
    protected override void AI()
    {
        DeathDelegate.Invoke(this);
        transform.localRotation = Quaternion.AngleAxis(-rb.velocity.x / 10 * 45, Vector3.forward);
        switch (state)
        {
            case 0:

            break;
            case 1:
                leftGun.SetActive(true);
                rightGun.SetActive(true);
                state = 2;
                PhaseTimer = 1000;
                break;
            case 2:
                GameObject Cannon = PlayerBase.player.transform.position.x<transform.position.x?leftGun:rightGun;
                float angle = Vector2.SignedAngle(Cannon.transform.up, PlayerBase.player.transform.position - transform.position);
                float absAngle = Mathf.Abs(angle);
                Cannon.transform.Rotate(0, 0, absAngle > 0.01 ? Mathf.Clamp(angle * Time.deltaTime * 25, -absAngle, absAngle) : 0);
                if (Target != null && Timer <= 0 && Mathf.Abs(angle) < 1)
                {
                    Timer = 0.2f;
                    Projectile.Create(bullet, Cannon, transform.position, Cannon.transform.up, 50, 5, LayerMask.GetMask("Player"));
                }
                GameObject NonCannon = PlayerBase.player.transform.position.x >= transform.position.x ? leftGun : rightGun;
                float Nangle = Vector2.SignedAngle(NonCannon.transform.up, Vector2.up);
                float NabsAngle = Mathf.Abs(Nangle);
                NonCannon.transform.Rotate(0, 0, NabsAngle > 0.01 ? Mathf.Clamp(Nangle * Time.deltaTime * 25, -NabsAngle, NabsAngle) : 0);
                break;
            case 3:

                break;
            case 4:

                break;
        }
        Timer -= Time.deltaTime;
        PhaseTimer -= Time.deltaTime;
    }
    protected override void FixedAI()
    {
        if(state == 4)
            foreach(var i in Physics2D.CircleCastAll(transform.position, 0.8f, dashpos, 0.1f,LayerMask.GetMask("Player")))
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

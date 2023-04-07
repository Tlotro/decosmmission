using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitTestScript2 : StaticUnit
{
    [SerializeField]
    GameObject Cannon;
    [SerializeField]
    Light laser;
    GameObject bullet;
    int state;
    float Timer;

    protected override void Awake()
    {
        base.Awake();
        bullet = Resources.Load<GameObject>("Projectiles/BaseBullet");
    }

    protected override void SetDefaults()
    {
        detectionRange = 30;
        MaxHPBase = 50;
        MaxHPmultiplyer = 1;
        state = 0;
    }

    protected override void AI()
    {
        //Cannon.transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(Cannon.transform.rotation.z,Vector2.Angle(savedposition - transform.position, -Cannon.transform.up),0.15f),Vector3.back);
        //Cannon.transform.up = Cannon.transform.position-savedposition;
        //float angle = Vector2.Angle(-Cannon.transform.up, savedposition - transform.position);
        //angle = angle > 0 ? angle : 360 - angle;
        //Debug.Log(angle);
        //Cannon.transform.Rotate(Vector3.forward, Mathf.Abs(angle)>1?Mathf.Clamp(Mathf.LerpAngle(Cannon.transform.rotation.eulerAngles.z, angle, 0.015f),-1,1) : 0) ;
        float angle = Vector2.SignedAngle(-Cannon.transform.up, savedposition - transform.position);
        switch (state) 
        {
            case 0:
                Cannon.transform.Rotate(0, 0, Mathf.Abs(angle) > 0.01 ? (angle * 0.1f) : 0);
                if (Target != null && Timer <= 0 && Mathf.Abs(angle) < 1)
                {
                    Timer = 0.5f;
                    state = 1;
                }
                break;
            case 1:
                if (Timer <=0)
                {
                    Timer = 1;
                    state = 0;
                    Projectile.Create(bullet, this.gameObject, transform.position, -Cannon.transform.up, 50, 15, LayerMask.GetMask("Player"));
                }
                break;
        }
        Timer -= Time.deltaTime;
    }

}

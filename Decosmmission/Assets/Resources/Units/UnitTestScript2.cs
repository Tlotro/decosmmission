using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitTestScript2 : StaticUnit
{
    [SerializeField]
    GameObject Cannon;
    float shootingDelay;
    GameObject bullet;

    protected override void Awake()
    {
        base.Awake();
        bullet = Resources.Load<GameObject>("Projectiles/BaseBullet");
    }

    protected override void SetDefaults()
    {
        detectionRange = 20;
        MaxHP = 50;
        CurrentHP = 50;
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
        Cannon.transform.Rotate(0, 0, Mathf.Abs(angle) >0.01?(angle*0.1f):0);
        if(Target != null && shootingDelay <=0 && Mathf.Abs(angle) < 1)
        {
            shootingDelay = 1;
            Projectile.Create(bullet, this.gameObject, transform.position, -Cannon.transform.up, 50, 15, LayerMask.GetMask("Player"));
        }
        shootingDelay -= Time.deltaTime;
    }

}

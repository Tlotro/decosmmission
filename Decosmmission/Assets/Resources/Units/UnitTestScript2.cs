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
        Cannon.transform.up = Cannon.transform.position-savedposition;
        if(Target != null && shootingDelay <=0)
        {
            shootingDelay = 1;
            Projectile.Create(bullet, this.gameObject, transform.position, -Cannon.transform.up, 50, 15, LayerMask.GetMask("Player"));
        }
        shootingDelay -= Time.deltaTime;
    }

}

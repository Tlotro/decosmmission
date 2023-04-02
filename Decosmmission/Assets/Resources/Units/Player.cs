using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : PlayerBase
{
    [SerializeField]
    GameObject Cannon;
    float attackDelay;
    int attackmode;
    GameObject bullet;
    //TMPro.TMP_Text text;

    //Item[] cargo;
    float MaxCargo;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //text.text = "Weapon: melee";
    }

    protected override void Awake()
    {
        base.Awake();
        bullet = Resources.Load<GameObject>("Projectiles/BaseBullet");
    }

    protected override void SetDefaults()
    {
        acceleration = 10;
        JumpVelocity = 35;
        MaxSpeedX = 20;
        MaxSpeedY = 200;
        BaseMass = 1;
        MaxHP = 100;
        CurrentHP = 100;
    }

    // Update is called once per frame
    protected override void Update()
    {
        float angle = Vector2.SignedAngle( - Cannon.transform.up, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Cannon.transform.Rotate(0, 0, Mathf.Abs(angle) > 0.01 ? (angle * 0.1f) : 0);
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackDelay <= 0)
            {
                if (attackmode == 0)
                {
                    foreach (var i in Physics2D.CircleCastAll(transform.position, 3, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 0.1f, LayerMask.GetMask("Unit")))
                        if (i.collider.gameObject.tag != "Player" && Vector2.Dot((i.transform.position - transform.position).normalized, ((Vector2)(CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized) > 0.75)
                            i.collider.GetComponent<BaseEntity>().TakeDamage(this.gameObject, 10);
                    attackDelay = 0.75f;
                }
                if (attackmode == 1)
                {
                    Projectile.Create(bullet, this.gameObject, Cannon.transform.position, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 50, 7, LayerMask.GetMask("Unit"));
                    attackDelay = 0.5f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            attackmode = (attackmode + 1) % 2;
            switch (attackmode)
            {
                case 0:
                    //text.text = "Weapon: melee";
                    break;
                    case 1:
                    //text.text = "Weapon: ranged";
                    break;
            }
        }
        attackDelay -= Time.deltaTime;
    }

    public override void TakeDamage(GameObject inflictor,int damage)
    {
        base.TakeDamage(inflictor, damage);
    }    
}

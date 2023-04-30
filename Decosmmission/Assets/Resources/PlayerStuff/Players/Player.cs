using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : PlayerBase
{
    List<Weapon> weapons = new List<Weapon>();
    int currentWeapon;
    //TMPro.TMP_Text text;

    //Item[] cargo;
    float MaxCargo;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CombatUiManager.UpdateMunitions(weapons[currentWeapon]);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SetDefaults()
    {
        acceleration = 10;
        JumpVelocity = 35;
        MaxSpeedX = 20;
        MaxSpeedY = 200;
        BaseMass = 1;
        MaxHPBase = 100;
        MaxHPmultiplyer = 1;
        weapons.Add(Instantiate(Resources.Load<GameObject>("PlayerStuff/Weapons/Gun/Gun").GetComponent<Weapon>(),transform));
        weapons.Add(Instantiate(Resources.Load<GameObject>("PlayerStuff/Weapons/Wrench/Wrench").GetComponent<Weapon>(), transform));
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        weapons[currentWeapon].gameObject.SetActive(true);
        CombatUiManager.UpdateWeapon(weapons[currentWeapon]);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        /*if (attackDelay <= 0)
        {
            if (attackmode == 0)
            {
                Cannon.transform.rotation = Quaternion.Euler(0, 0, 45 * (spriteRenderer.flipX ? 1 : -1));
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    float angle = Vector2.SignedAngle(-Cannon.transform.up, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                    Cannon.transform.rotation = Quaternion.Euler(0, 0, angle + 45 * (spriteRenderer.flipX ? 1 : -1));
                    foreach (var i in Physics2D.CircleCastAll(Cannon.transform.position, 3, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 0.1f, LayerMask.GetMask("Unit")))
                        if (i.collider.gameObject.tag != "Player" && Vector2.Dot((i.transform.position - transform.position).normalized, ((Vector2)(CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized) > 0.75)
                            i.collider.GetComponent<BaseEntity>().TakeDamage(this.gameObject, 10);
                    attackDelay = 0.75f;
                }
            } else if (attackmode == 1)
            {
                float angle = Vector2.SignedAngle(-Cannon.transform.up, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                float absAngle = Mathf.Abs(angle);
                Cannon.transform.Rotate(0, 0, Mathf.Abs(angle) > 0.01 ? Mathf.Clamp(angle * Time.deltaTime * 25, -absAngle, absAngle) : 0);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Projectile.Create(bullet, this.gameObject, Cannon.transform.position, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 50, 7, LayerMask.GetMask("Unit"));
                    attackDelay = 0.5f;
                }
            }

        }*/
        if (weapons[currentWeapon].AutoFire && Input.GetKey(KeyCode.Mouse0))
        {
            if (weapons[currentWeapon].NeedReload(this))
            {
                weapons[currentWeapon].PreReload(this);
                weapons[currentWeapon].Reload(this);
            }
            if (weapons[currentWeapon].CanFire(this))
            {
                weapons[currentWeapon].PreFire(this);
                weapons[currentWeapon].Fire(this);
            }
        }
        if (!weapons[currentWeapon].AutoFire && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (weapons[currentWeapon].NeedReload(this))
            {
                weapons[currentWeapon].PreReload(this);
                weapons[currentWeapon].Reload(this);
            }
            if (weapons[currentWeapon].CanFire(this))
            {
                weapons[currentWeapon].PreFire(this);
                weapons[currentWeapon].Fire(this);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (weapons[currentWeapon].OnDeselect(this))
            {
                weapons[currentWeapon].gameObject.SetActive(false);
                currentWeapon = (currentWeapon + 1) % weapons.Count;
                weapons[currentWeapon].OnSelect(this);
                weapons[currentWeapon].gameObject.SetActive(true);
                weapons[currentWeapon].gameObject.SetActive(true);
                weapons[currentWeapon].gameObject.transform.rotation = new Quaternion();
                CombatUiManager.UpdateWeapon(weapons[currentWeapon]);
            }
        }
        weapons[currentWeapon].UpdateHeld(this);
    }

    public override void TakeDamage(GameObject inflictor,int damage)
    {
        base.TakeDamage(inflictor, damage);
    }    
}

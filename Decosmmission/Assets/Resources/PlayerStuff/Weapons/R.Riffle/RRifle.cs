using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRifle : Weapon
{
    public GameObject bullet;
    public float burstDelay;
    [HideInInspector]
    Vector3 newScale;
    [HideInInspector]
    int shots;
    private void Awake()
    {
        description = "A standart-issue weapon of a well-trained private army of clones.\n" +
        "While it allows for quick suppression of any enemy, it's ammo capacity is very limited\n" +
        "It is advised to save up ammo for when you really need it.\n\n";
        newScale = transform.localScale;
    }

    public override bool CanFire(Player weilder)
    {
        return base.CanFire(weilder);
    }

    public override void Fire(Player weilder)
    {
        Projectile.Create(bullet, weilder.gameObject, transform.position, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 50, 15, LayerMask.GetMask("Unit"));
        shots++;
        base.Fire(weilder);
        if (shots == 3)
        {
            shots = 0;
            FireDelay = burstDelay;
        }
        CombatUiManager.UpdateMagazine(this);
    }

    public override void Reload(Player weilder)
    {
        base.Reload(weilder);
        CombatUiManager.UpdateAmmo(this);
        CombatUiManager.UpdateMagazine(this);
    }

    public override void OnCeaseFire(Player weilder)
    {
        shots = 0;
        FireDelay = burstDelay;
    }

    public override void UpdateHeld(Player weilder)
    {
        base.UpdateHeld(weilder); 
        float angle = Vector2.SignedAngle(transform.up, (Vector2)(CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position));
        float absAngle = Mathf.Abs(angle);
        transform.Rotate(0, 0, Mathf.Abs(angle) > 0.01 ? Mathf.Clamp(angle * Time.deltaTime * 25, -absAngle, absAngle) : 0);
        newScale.x = Mathf.Sign(transform.rotation.eulerAngles.z-180);
        transform.localScale = newScale;
    }
}

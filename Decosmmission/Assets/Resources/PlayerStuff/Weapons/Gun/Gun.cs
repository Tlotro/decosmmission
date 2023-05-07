using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bullet;
    [HideInInspector]
    Vector3 newScale;
    private void Awake()
    {
        description = "A simple weapon made from what space parts were on the ship at the time of your escape.\n"+
            "On one hand, the simplicity of ammunition allows you to bring more than you could ever fire, and the electromagnetic launcher removes the need for any gunpowder.\n" +
            "On the other, you pay for it in firepower and inability to autofire, since the capacitor won't start charging untill you break the circuit";
        /*"A standart-issue self-defence weapon for use in spacecrafts and open space.\n" +
        "As private ships became more available, attacks on civilian vessels became more and more frequent.\n" +
        "As a result, self-defence and weapon handling became parts of standart space training.\n\n" +
        */
        bullet = Resources.Load<GameObject>("PlayerStuff/Weapons/Gun/GaussBullet");
        newScale = transform.localScale;
    }

    public override bool CanFire(Player weilder)
    {
        return base.CanFire(weilder);
    }

    public override void Fire(Player weilder)
    {
        Projectile.Create(bullet, weilder.gameObject, transform.position, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 50, 7, LayerMask.GetMask("Unit"));
        base.Fire(weilder);
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

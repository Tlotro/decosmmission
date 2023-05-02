using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : Weapon
{
    [HideInInspector]
    bool swinging;
    float angleT = 120;
    private void Awake()
    {
        description = 
            "A part of standart ship repair equipment.\n" +
            "Long handle and weighty head make it easier to use in low gravity...\n" +
            "They also make it a suitable weapon\n" +
            "It's the biggest you could find\n\n" +
            "Something compells you to write \"Vengance\" on it";
    }

    public override bool CanFire(Player weilder)
    {
        return base.CanFire(weilder);
    }

    public override void Fire(Player weilder)
    {
        swinging = true;
        angleT = angleT * -1;
        base.Fire(weilder);
    }

    public override void UpdateHeld(PlayerBase weilder)
    {
        base.UpdateHeld(weilder); 
        float angle = Vector2.SignedAngle(transform.up, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position)+angleT;
        float absAngle = Mathf.Abs(angle);
        transform.Rotate(0, 0, Mathf.Abs(angle) > 0.01 ? Mathf.Clamp(angle * Time.deltaTime * 25, -absAngle, absAngle) : 0);
        if (swinging && absAngle <= 0.01) swinging = false;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (swinging)
        {
            if ((6 == other.gameObject.layer) && Physics2D.Raycast(transform.position, other.transform.position, 2.25f).collider == other);
             {
                if (other.gameObject.GetComponent<BaseEntity>() != null)
                other.gameObject.GetComponent<BaseEntity>().TakeDamage(gameObject, 20);
             }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string TrueName;
    public string description;
    public Animator animator;
    public bool AutoFire;
    public float BaseFireDelay;
    [HideInInspector]
    public float FireDelay;
    public int MaxAmmo;
    [HideInInspector]
    public int Ammo;
    public float ReloadTime;
    [HideInInspector]
    public int Magazine;
    public int MaxMagazine;
    public bool Rotating;
    public int[] unlockcost;
    [HideInInspector]
    public bool unlocked;
    public bool UseMagazine { get { return MaxMagazine > 0; } }
    public bool UseAmmo { get { return MaxAmmo > 0; } }

    public Weapon() { }
    public virtual bool OnSelect(Player weilder) { enabled = true; return true; }
    public virtual bool OnDeselect(Player weilder) { enabled = false; return true; }
    public virtual void PreFire(Player weilder) { }
    public virtual bool CanFire(Player weilder) { return ((FireDelay <= 0) && (!UseMagazine || Magazine > 0));}
    public virtual void Fire(Player weilder) { FireDelay = BaseFireDelay; if (UseMagazine) { Magazine--; } else if (UseAmmo) { Ammo--; } }
    /// <summary>
    /// Is called after every shot
    /// </summary>
    public virtual void AfterFire(Player weilder) { }
    /// <summary>
    /// Only works on automatic weapons. Is called when the weapon stops firing
    /// </summary>
    public virtual void OnCeaseFire(Player weilder) { }
    public virtual void PreReload(Player weilder) { }
    public virtual bool NeedReload(Player weilder) { return (UseMagazine && Magazine <= 0); }
    public virtual void Reload(Player weilder) { FireDelay = ReloadTime; if (UseAmmo) { Magazine = Mathf.Min(MaxMagazine, Ammo); Ammo -= Magazine; } else Magazine = MaxMagazine; }
    public virtual void AfterReload(Player weilder) { }
    public virtual void OnEmptyMag(PlayerBase weilder) { }
    public virtual void OnEmpty(PlayerBase weilder) { }
    public virtual void UpdateHeld(PlayerBase weilder) { FireDelay = FireDelay > 0? FireDelay-Time.deltaTime: 0; }
    /// <summary>
    /// NYI
    /// </summary>
    /// <param name="weilder"></param>
    public virtual void UpdateUnheld(PlayerBase weilder) { }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {

    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {

    }
}

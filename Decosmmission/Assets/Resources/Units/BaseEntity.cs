using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EmptyUnitDelegade(BaseEntity unit);
public abstract class BaseEntity : MonoBehaviour
{
    public int MaxHP { get { return Mathf.FloorToInt(MaxHPBase * MaxHPmultiplyer + MaxHPaddition); } }
    [HideInInspector]
    public float MaxHPmultiplyer;
    [HideInInspector]
    public float MaxHPaddition;
    public int MaxHPBase;
    public virtual int CurrentHP { get { return _CurrentHP; } protected set { _CurrentHP = value; } }
    protected int _CurrentHP;
    public Dictionary<GameObject, double> Iframelist = new Dictionary<GameObject, double>();
    public EmptyUnitDelegade DeathDelegate = delegate { };
    public virtual void TakeDamage(GameObject inflictor, int damage)
    {
        if (!Iframelist.ContainsKey(inflictor))
        {
            CurrentHP -= damage;
            Iframelist.Add(inflictor, 0.5);
        }
        if (CurrentHP <= 0)
            Death();
    }
    public virtual void RecoverHP(int count)
    {
        CurrentHP = Mathf.Min(CurrentHP+count,MaxHP);
    }
    protected virtual void SetDefaults() { }

    protected virtual void Awake() { StaticGameData.instance.GlobalStartDelegate.Invoke(this); }
    protected virtual void Start()
    {
        SetDefaults();
    }

    protected virtual void Update()
    {
        var keys = new List<GameObject>(Iframelist.Keys);
        foreach (var a in keys)
        {
            Iframelist[a] -= Time.deltaTime;
            if (Iframelist[a] <= 0)
                Iframelist.Remove(a);
        }
    }

    public virtual void Death()
    {
        StaticGameData.instance.GlobalDeathDelegate.Invoke(this);
        DeathDelegate.Invoke(this);
        Destroy(this.gameObject);
    }
}

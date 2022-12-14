using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EmptyUnitDelegade(BaseEntity unit);
public abstract class BaseEntity : MonoBehaviour
{
    public virtual int MaxHP { get { return _maxHP; } protected set { _maxHP = value; } }
    protected int _maxHP;
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
    protected virtual void SetDefaults() { }

    protected virtual void Awake() { }
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
        DeathDelegate.Invoke(this);
        Destroy(this.gameObject);
    }
}

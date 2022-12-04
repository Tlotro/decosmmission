using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : StaticUnit
{
    protected Rigidbody2D rb;
    override protected void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }
}

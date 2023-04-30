using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class AbstractUpgrade
{
    public int level;
    /// <summary>
    /// The maximum level of the upgrade, set this in SetDefaults
    /// </summary>
    public int maxlevel { get; protected set; }
    public bool unlocked { get; private set; }
    public bool enabled { get; private set; }
    /// <summary>
    /// The types of "Child" upgrades, set this in SetDefaults
    /// </summary>
    protected Type[] childTypes { private get; set; }
    public AbstractUpgrade[] children { get; private set; }
    public string Description;

    /// <summary>
    /// Use this to initialize children upgrades
    /// </summary>
    public AbstractUpgrade() 
    {
        SetDefaults();
        children = new AbstractUpgrade[childTypes.Length];
        for (int i = 0; i < childTypes.Length; i++)
        children[i] = (AbstractUpgrade)Activator.CreateInstance(childTypes[i]);
    }
    public virtual void SetDefaults()
    {
        maxlevel = 1;
        childTypes = new System.Type[] { };
    }
    public void Unlock(PlayerBase player)
    {
        unlocked = true;
        enabled = true;
        level = 1;
        OnUnlock(player);
        foreach (var child in children)
            player.availableUpgrades.Add((this, child));
    }
    public void OnStartBase(PlayerBase player)
    {
        if (enabled)
            OnStart(player);
        foreach (var child in children)
            if (child.unlocked)
            child.OnStart(player);
    }
    public void Enable(PlayerBase player)
    {
        enabled = true;
        OnEnable(player);
    }
    public void Disable(PlayerBase player)
    {
        enabled = false;
        OnDisable(player);
    }

    /// <summary>
    /// A function that is called when the upgrade is first unlocked.
    /// </summary>
    /// <param name="player"></param>
    public virtual void OnUnlock(PlayerBase player)
    {

    }


    /// <summary>
    /// A function that is called on the start of each level.
    /// </summary>
    /// <param name="player"></param>
    public virtual void OnStart(PlayerBase player)
    {

    }

    /// <summary>
    /// A function that is called each time the upgrade is enabled in the modification menu.
    /// Use this only if you need to interact with the Modification table.
    /// If you need something to happen on mission start, use OnStart(player)
    /// </summary>
    /// <param name="player"></param>
    public virtual void OnEnable(PlayerBase player)
    {

    }

    /// <summary>
    /// A function that is called each time the upgrade is disabled in the modification menu.
    /// Use this only if you need to interact with the Modification table.
    /// </summary>
    /// <param name="player"></param>
    public virtual void OnDisable(PlayerBase player)
    {

    }
}

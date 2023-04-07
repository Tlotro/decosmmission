using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class AbstractUpgrade
{
    public int level;
    static public int maxlevel;
    public bool enabled { get; private set; }
    static public Type[] children;
    public SortedDictionary<int, AbstractUpgrade> unlockedChildren = new SortedDictionary<int, AbstractUpgrade>();
    static public string Description;

    /// <summary>
    /// Use this to initialize children upgrades
    /// </summary>
    public AbstractUpgrade() { }
    public void OnUnlockBase(PlayerBase player)
    {
        enabled = true;
        OnUnlock(player);
        foreach (var child in children)
            player.availableUpgrades.Add((this, child));
    }
    public void OnStartBase(PlayerBase player)
    {
        if (enabled)
            OnStart(player);
        foreach (var child in unlockedChildren)
            child.Value.OnStart(player);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorking : MonoBehaviour
{    
    public virtual void OnCollect(Player player) { }
    public virtual void OnMissionEnd(Player player) { }
    public virtual bool CanUse(Player player) { return false; }
    public virtual void OnUse(Player player) { }
    public virtual void OnDeconstruct() { }
    public virtual void OnDiscard(Player player) { }
}

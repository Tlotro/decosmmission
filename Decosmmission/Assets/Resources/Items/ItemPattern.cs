using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPattern")]
public class ItemPattern : MonoBehaviour
{
    public Sprite sprite;
    public string ItemName;
    public string Description;
    //Can an item be consumed mid-mission for some effects
    public bool Consumable;
    //If an item is fragile, it will be automatically deconstructed when returning to the ship
    public bool Fragile;
    //Resources that are given on deconstruction
    public int[] deconstructionResources = new int[6];
    // Start is called before the first frame update
    public virtual void OnCollect(Player player) { }
    public virtual void OnMissionEnd(Player player) { }
    public virtual bool CanUse(Player player) { return false; }
    public virtual void OnUse(Player player) { }
    public virtual void OnDeconstruct() { }
    public virtual void OnDiscard(Player player) { }
}

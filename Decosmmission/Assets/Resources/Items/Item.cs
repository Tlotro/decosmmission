using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource { Metals, Dust, Chemicals, Polymers, Organics, Fiber}

public static class ResourcesExtensions
{
    public static string Description(Resource res)
    {
        switch (res)
        {
            case Resource.Metals: return "Assorted metals of various toughness, strength, conductivity and radioactivity";
            case Resource.Dust: return "Non-metals that are mostly used in industry: sulfur, silicon. And various gasses";
            case Resource.Chemicals: return "Various non-organic compounds: acids, bases, salts and water.";
            case Resource.Polymers: return "Resins, plastics, crystaline structures, various isomeres of carbon";
            case Resource.Organics: return "Proteins, fats, carbohydrates, spirits. Almost everything that has a carbon \"frame\"";
            case Resource.Fiber: return "Due to special properties, utility and frequency of kerotene, chitin and cellulose they had to be separated into their own category";
            default: return "if you see this, something is wrong";
        }
    }
}

public abstract class Item
{
    public Sprite icon;
    public string ItemName;
    public string Description;
    public int MaxCount;
    public int Count;
    public abstract void SetDefaults();
    //Can an item be consumed mid-mission for some effects
    public bool Consumable;
    //If an item is fragile, it will be automatically deconstructed when returning to the ship
    public bool Fragile;
    //Resources that are given on deconstruction
    public int[] deconstructionResources = new int[6];
    public virtual void OnCollect(Player player) {}
    public virtual void OnMissionEnd(Player player) {}
    public virtual void OnUse(Player player) {}
    public virtual void OnDeconstruct() {}
}

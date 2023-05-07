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

public delegate void PlayerItemDelegate(Player player, Item item);
public delegate void ItemDelegate(Item item);

public class Item
{
    //public int MaxCount;
    [HideInInspector]
    public int count;
    public ItemPattern pattern;
    public static PlayerItemDelegate OnCollectDelegate = delegate { };
    public static PlayerItemDelegate OnMissionEndDelegate = delegate { };
    public static PlayerItemDelegate OnUseDelegate = delegate { };
    public static ItemDelegate OnDeconstructDelegate = delegate { };
    public static PlayerItemDelegate OnDiscardDelegate = delegate { };

    public Item(ItemPattern pattern, int count = 0)
    {
        this.pattern = pattern;
        this.count = count;
    }
}

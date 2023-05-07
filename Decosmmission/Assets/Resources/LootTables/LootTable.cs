using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class ItemDrop
{
    public ItemPattern item;
    public int MinDrop;
    public int MaxDrop;
}

[Serializable]
public class ItemCategory
{
    public ItemDrop[] items;
    public float chance;
}

[CreateAssetMenu(fileName = "LootTable")]
public class LootTable : ScriptableObject
{
    ItemCategory[] categories;

    public void DiectTransfer()
    {
        foreach (var category in categories)
        {
            if (UnityEngine.Random.value < category.chance)
            {
                ItemDrop drop = category.items[UnityEngine.Random.Range(0, category.items.Length)];
                if (StaticGameData.instance.items.Any(x => x.pattern == drop.item))
                    StaticGameData.instance.items.Find(x => x.pattern == drop.item).count += UnityEngine.Random.Range(drop.MinDrop, drop.MaxDrop);
                else
                {
                    Item item = new Item(drop.item, UnityEngine.Random.Range(drop.MinDrop, drop.MaxDrop));
                    if (item.pattern.Fragile)
                    {
                        foreach (int i in Enum.GetValues(typeof(Resource)))
                            StaticGameData.instance.resources[i] += item.pattern.deconstructionResources[i];
                        item.pattern.working.OnDeconstruct();
                    }
                    else
                    {
                        StaticGameData.instance.items.Add(item);
                    }
                }
            }
        }
    }
    public void PlayerTransfer()
    {
        foreach (var category in categories)
        {
            if (UnityEngine.Random.value < category.chance)
            {
                ItemDrop drop = category.items[UnityEngine.Random.Range(0, category.items.Length)];
                if (Player.player.items.Any(x => x.pattern == drop.item))
                    Player.player.items.First(x => x.pattern == drop.item).count += UnityEngine.Random.Range(drop.MinDrop, drop.MaxDrop);
                else
                {
                    Item item = new Item(drop.item, UnityEngine.Random.Range(drop.MinDrop, drop.MaxDrop));

                    for (int i = 0; i < Player.player.items.Length; i++)
                    {
                        if (Player.player.items[i] == null)
                        {
                            Player.player.items[i] = item;
                            break;
                        }
                    }
                }
            }
        }
    }
}

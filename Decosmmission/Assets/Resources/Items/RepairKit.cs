using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : ItemWorking
{
    public override void OnUse(Player player)
    {
        player.RecoverHP(player.MaxHP / 20 + 20);
    }
}

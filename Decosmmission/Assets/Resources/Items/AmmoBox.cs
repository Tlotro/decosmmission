using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : ItemPattern
{
    public override bool CanUse(Player player)
    {
        return player.weapons[player.currentWeapon].UseAmmo;
    }
    public override void OnUse(Player player)
    {
        player.weapons[player.currentWeapon].Ammo = Mathf.Min(player.weapons[player.currentWeapon].MaxAmmo, player.weapons[player.currentWeapon].Ammo + player.weapons[player.currentWeapon].MaxAmmo/4);
        CombatUiManager.UpdateAmmo(player.weapons[player.currentWeapon]);
    }
}

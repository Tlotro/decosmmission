using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHpUpgrade : AbstractUpgrade
{
    
    public override void OnStart(PlayerBase player)
    {
        switch (level)
        {
            case 1:
                player.MaxHPmultiplyer = 1.25f;
                break;
            case 2:
                player.MaxHPmultiplyer = 1.5f;
                break;
            case 3:
                player.MaxHPmultiplyer = 1.75f;
                break;
        }
    }
}

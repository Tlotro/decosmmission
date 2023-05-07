using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillMissionTask : MissionOrder
{
    public override bool complete => _complete;

    public override string showString => "Defeat the boss: " + (_complete ? "Complete" : "");

    public override void Setup()
    {
        _complete = false;
        BaseEntity.GlobalStartDelegate += bossStartCheck;
    }
    private bool _complete;
    public void bossDeathCheck(BaseEntity boss)
    {
        _complete = true;
        CombatUiManager.UpdateMission();
    }

    public void bossStartCheck(BaseEntity boss)
    {
        if (boss is HelicopterBoss)
        {
            boss.DeathDelegate += bossDeathCheck;
        }
    }
}

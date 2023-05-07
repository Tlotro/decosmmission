using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryKillEnemiesMission : MissionOrder
{
    public override bool complete => _killedEnemies >= _enemyCount;

    public override string showString => "Defeat enemies: "+(complete?("Complete"):_killedEnemies.ToString()+"/"+_enemyCount.ToString());

    private int _enemyCount;
    private int _killedEnemies;
    public override void Setup()
    {
        _enemyCount = 0;
        _killedEnemies = 0;
        StaticGameData.instance.GlobalStartDelegate += OnUnitStart;
        StaticGameData.instance.PostGeneration += PostGeneration;
    }
    public void OnUnitStart(BaseEntity entity)
    {
        if (!(entity is PlayerBase))
        {
            _enemyCount++;
            entity.DeathDelegate += OnUnitKill;
        }
    }
    public void OnUnitKill(BaseEntity entity)
    {
        _killedEnemies++;
        CombatUiManager.UpdateMission();
    }
    public void PostGeneration()
    {
        _enemyCount = _enemyCount / 4 * 3;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryBreakReactors : MissionOrder
{
    public override bool complete => _recators <= _sabotaged;

    public override string showString => "Break reactors: " + (complete?"Complete":_sabotaged.ToString()+"/"+_recators.ToString());
    int _recators;
    int _sabotaged;
    public override void Setup()
    {
        _recators = 0;
        _sabotaged = 0;
        Generator.roomGenerationDelegate += CheckReactor;
        Interactable.GlobalinteractebleDelegate += CountSabotaged;
    }

    public void CountSabotaged(PlayerBase player, Interactable interactable)
    {
        if (interactable is Reactor && !(interactable as Reactor).sabotaged)
        {
            _sabotaged++;
            CombatUiManager.UpdateMission();
        }
    }

    public void CheckReactor(RoomDesign design)
    {
        if (design.GetComponentInChildren<Reactor>() != null)
            _recators++;
    }
}

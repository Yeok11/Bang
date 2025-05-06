using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Skill
{
    private Player player;
    public override void Act()
    {
        player.SetPower(STATE.MOVE, 1);
        player.ChangeState(STATE.MOVE);
    }

    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    protected override void Awake()
    {
        base.Awake();
        player = gd.player;
    }
}

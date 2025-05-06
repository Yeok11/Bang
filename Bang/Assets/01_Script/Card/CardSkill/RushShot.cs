using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushShot : Skill
{
    private Player player;
    public override void Act()
    {
        player.ChangeState(STATE.BATTLE);
        player.SetPower(STATE.BATTLE, 1);
        player.SetPower(STATE.MOVE, 1);
    }

    protected override void Awake()
    {
        base.Awake();
        player = gd.player;
    }
}

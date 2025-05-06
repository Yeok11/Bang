using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickShot : Skill
{
    private Player player;

    public override void Act()
    {
        player.Reload(1);

        player.ShowBullet();

        player.ChangeState(STATE.BATTLE);
        player.SetPower(STATE.BATTLE, 1);
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

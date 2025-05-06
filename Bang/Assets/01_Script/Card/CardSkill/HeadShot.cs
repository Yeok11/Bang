using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : Skill
{
    private Player player;
    protected override void Awake()
    {
        base.Awake();
        player = gd.player;
    }

    public override bool CanSkill()
    {
        return true;
    }

    public override void Act()
    {
        player.ChangeState(STATE.BATTLE);
        player.weapon.nextBulletCri = true;
        player.SetPower(STATE.BATTLE, 1);
    }

}

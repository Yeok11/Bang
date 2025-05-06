using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : Skill
{
    private Player player;
    public override void Act()
    {
        player.Reload(1);
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

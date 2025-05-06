using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : Skill
{
    private Player player;
    public override void Act()
    {
        player.Heal();
    }

    protected override void Awake()
    {
        base.Awake();
        player = gd.player;
    }
}

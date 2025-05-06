using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : Skill
{
    public override void Act()
    {
        StartCoroutine(GameData.instance.gm.hm.CardDrawbyCnt(2));
    }

    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    protected override void Awake()
    {
        base.Awake();
    }
}

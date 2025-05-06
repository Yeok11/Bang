using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_ShotGun : Skill
{
    private Player player;
    [SerializeField] private Weapon gun;

    public override void Act()
    {
        player.ChangeGun(gun);
    }

    protected override void Awake()
    {
        base.Awake();
        player = gd.player;
    }
}

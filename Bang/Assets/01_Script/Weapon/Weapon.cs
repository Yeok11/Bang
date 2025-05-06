using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Weapon : ScriptableObject
{
    public string gunName;
    public int damage;
    public int range;
    public int maxBullet;
    public int nowBullet;
    public GUNTYPE gunType;


    public bool nextBulletCri = false;

    public Sprite image;
}

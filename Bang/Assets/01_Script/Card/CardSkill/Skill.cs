using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected GameData gd;

    protected virtual void Awake()
    {
        gd = GetComponent<CardEffectManager>().gd;
    }

    public virtual bool CanSkill()
    {
        return true;
    }

    public virtual void Act()
    {
    }
}

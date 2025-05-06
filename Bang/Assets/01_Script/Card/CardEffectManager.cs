using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public GameData gd;

    public bool CheckAct(int _cost)
    {
        return gd.player.actPower >= _cost;
    }

    public void ActCard(CardSO _card)
    {
        Skill skill = GetComponent(_card.cardName) as Skill;

        --gd.gm.hm.handCnt;
        skill.Act();
        gd.player.actPower -= _card.cost;
        gd.player.SetMessage();
    }
}

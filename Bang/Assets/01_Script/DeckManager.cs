using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] protected List<CardSO> deck;

    public void SettingDeck()
    {
        deck = new List<CardSO>();
        for (int i = 0; i < GameData.instance.deck.Count; i++)
        {
            deck.Insert(Random.Range(0, deck.Count), GameData.instance.deck[i]);
        }
    }
}

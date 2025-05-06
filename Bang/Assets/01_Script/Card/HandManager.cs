using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandManager : DeckManager
{
    [SerializeField] internal List<HandCard> handCards;
    private HandSortManager hsm;
    internal int handCnt;
    internal bool nowDrag = false;
    [SerializeField] private Transform cardPos;

    private void Awake()
    {
        hsm = GetComponent<HandSortManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) DrawCard();
    }

    public IEnumerator CardDrawbyCnt(int _cnt = 1)
    {
        for (int i = 0; i < _cnt; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(0.12f);
        }
    }

    public void DrawCard()
    {
        if (handCnt == 10) return;

        if (deck.Count == 0)
            SettingDeck();

        ++handCnt;
        if (PoolManager.instance.gameObject.transform.childCount == 0)
            PoolManager.instance.AddItem(0);
        HandCard card = PoolManager.instance.gameObject.transform.GetComponentInChildren<HandCard>();
        handCards.Add(card);
        card.cardData = deck[0];
        card.InitData(cardPos);
        deck.RemoveAt(0);

        hsm.SortHand();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeckMakingManager : MonoBehaviour
{
    [SerializeField] private StagePlayerData spd;

    private Transform mid;
    private Transform right;
    private Transform left;

    public static int cardViewNum = -1;
    private int deckCnt;

    private CardSO nowCard;

    private void Awake()
    {
        mid = transform.GetChild(0);
        right = transform.GetChild(1);
        left = transform.GetChild(2);
    }

    public void DeckMakingButton(string _dir)
    {
        Debug.Log("_dir = " + _dir);
        if(_dir == "L")
        {
            --cardViewNum;
        }
        else if (_dir == "R")
        {
            ++cardViewNum;
        }

        DataUpdate();
    }

    public void DataUpdate()
    {
        Debug.Log(" <= " + spd);
        deckCnt = spd.userDeck.Count;

        mid.gameObject.SetActive(cardViewNum >= 0 && cardViewNum < deckCnt);
        
        left.gameObject.SetActive(cardViewNum > 0);

        right.gameObject.SetActive(cardViewNum < deckCnt-1);

        Debug.Log(cardViewNum + " / " + deckCnt);

        
        if (!mid.gameObject.activeSelf) return;
        
        
        //세팅 시작
        nowCard = spd.userDeck[cardViewNum];

        mid.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = nowCard.icon;
        mid.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = nowCard.cardName;
        mid.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = nowCard.cost.ToString();
        mid.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = nowCard.explain;
    }

    private void Start()
    {
        cardViewNum = -1;
        mid.gameObject.SetActive(false);
        left.gameObject.SetActive(false);

        DataUpdate();
    }
}

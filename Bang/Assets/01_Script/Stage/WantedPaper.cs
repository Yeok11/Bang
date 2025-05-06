using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Reward
{
    public string name;
    public CardSO reward;
    public int per;
}

[System.Serializable]
public class StageData
{
    public WANTED_STATE stageState;
    public int paperCnt;
    public List<Reward> rewardsList;
}

public class WantedPaper : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private WantedManager wm;
    [SerializeField] private CardSO card;

    private bool isBattle;
    private bool isEnd;

    public void DataSet(Reward _selectCard)
    {
        if (wm == null) wm = GameObject.Find("Manager").GetComponent<WantedManager>();

        isBattle = wm.spd.stage[wm.stageNum].stageState == WANTED_STATE.BATTLE;
        isEnd = wm.spd.stage[wm.stageNum].stageState == WANTED_STATE.END;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(isBattle ? "WANTED" : isEnd ? "END" : "REWARD");

        if (_selectCard != null)
        {
            Debug.Log(_selectCard.name);
            card = _selectCard.reward;
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(_selectCard.name);
            transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = card.icon;
        }
        else if (wm.spd.stage[wm.stageNum].stageState == WANTED_STATE.END)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("YOU");
            transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = wm.endillust;
        }
        else
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("ROBBERY");
            transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = wm.joji;
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        OnPointerExit(null);
        if (isBattle)
        {
            //배틀로
            SceneManager.LoadScene(2);
        }
        else if(wm.spd.stage[wm.stageNum].stageState == WANTED_STATE.END)
        {
            SceneManager.LoadScene(4);
        }
        else if (!isBattle)
        {
            //덱에 보상 추가
            wm.spd.userDeck.Add(card);
            wm.StageUpdate();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        wm.ShowWantedEffectOn(transform);
        if(!isBattle && !isEnd)
        {
            Transform explainPos = transform.GetChild(3);
            explainPos.gameObject.SetActive(true);
            Debug.Log(card);
            explainPos.GetComponentInChildren<TextMeshProUGUI>().SetText(card.explain);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.parent = wm.ShowWantedEffectOff();
        transform.GetChild(3).gameObject.SetActive(false);
    }
}

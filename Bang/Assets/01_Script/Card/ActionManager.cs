using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private GameObject blackUI;

    private void Awake()
    {
        blackUI.SetActive(false);
    }

    private string SetStateMessage(STATE _state)
    {
        switch (_state)
        {
            case STATE.NONE:
                return "현재 : 적의 차례";
            case STATE.BATTLE:
                return "현재 : 공격";
            case STATE.MOVE:
                return "현재 : 이동";
        }

        return "";
    }

    public void PreventUseCard(STATE _state)
    {
        blackUI.SetActive(_state != STATE.SET);

        //string stateMessage = SetStateMessage(_state);

        GameData.instance.gm.tm.TurnMesSign();

        blackUI.GetComponentInChildren<TextMeshProUGUI>().SetText("카드를 선택할 때가 아닙니다.");
    }
}

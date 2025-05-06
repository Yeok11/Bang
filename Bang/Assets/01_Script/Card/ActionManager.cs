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
                return "���� : ���� ����";
            case STATE.BATTLE:
                return "���� : ����";
            case STATE.MOVE:
                return "���� : �̵�";
        }

        return "";
    }

    public void PreventUseCard(STATE _state)
    {
        blackUI.SetActive(_state != STATE.SET);

        //string stateMessage = SetStateMessage(_state);

        GameData.instance.gm.tm.TurnMesSign();

        blackUI.GetComponentInChildren<TextMeshProUGUI>().SetText("ī�带 ������ ���� �ƴմϴ�.");
    }
}

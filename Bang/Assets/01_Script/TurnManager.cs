using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TurnManager : GameManager
{
    [SerializeField] private TextMeshProUGUI turnMes;

    public IEnumerator ActTurn(float delay = 2.5f)
    {
        Debug.Log("���� ����");
        if (gd == null) base.Start();

        //�÷��̾ �׾��°�?
        if(gd.player.state == STATE.DEAD)
        {
            SceneManager.LoadScene(3);
        }

        //���� �� ���
        for (int i = gd.enemys.Count - 1; i >= 0; --i)
        {
            if (gd.enemys[i].state == STATE.DEAD)
            {
                if (gd.characterOrder.Contains(gd.enemys[i]))
                    gd.characterOrder.Remove(gd.enemys[i]);
                gd.enemys.RemoveAt(i);
            }
        }

        //���� ���� ���
        if(gd.enemys.Count == 0)
            SceneManager.LoadScene(1);

        if (gd.characterOrder.Count <= 0) SetTurn(gd.enemys);

        yield return new WaitForSeconds(delay);

        Turn(gd.characterOrder[0]);
    }

    protected override void Update()
    {
    }


    private int CheckOrderValue(List<User> _lastData, int _value)
    {
        int i = 0;
        foreach (User item in _lastData)
            if (_lastData[i++].speed > _value) return i;

        return i;
    }

    public void Turn(User _user)
    {
        gd.playerTurn = _user == gd.player;

        if(_user != null)
        Debug.Log(_user.gameObject + "�� �׼�");

        SetTurnMes();

        if (gd.playerTurn)
        {
            gd.player.ChangeState(STATE.SET);
            gd.player.SetPower(STATE.SET, 5);
            gd.characterOrder.RemoveAt(0);
            StartCoroutine(hm.CardDrawbyCnt(hm.handCnt < 5 ? 5 - (hm.handCnt) : 1));
        }
        else
        {
            Enemy enemy = gd.characterOrder[0].GetComponent<Enemy>();
            gd.characterOrder.RemoveAt(0);
            enemy.EnemyAction();
        }
    }

    public string TurnMesSign()
    {
        switch (gd.player.state)
        {
            case STATE.BATTLE:
                return "���� ��";
            case STATE.MOVE:
                return "�̵� ��";
            default:
                return "�ൿ ���� ��";
        }

        return "ERROR";
    }

    private void SetTurnMes()
    {
        if (gd.characterOrder.Count != 0)
            turnMes.SetText(gd.characterOrder[0].role != CHARACTER.PLAYER ? "���� ����" : TurnMesSign());
    }

    public void SetTurn(List<User> _enemys)
    {
        gd.characterOrder.Add(gd.player);

        for (int i = 0; i < _enemys.Count; i++)
            gd.characterOrder.Insert(CheckOrderValue(gd.characterOrder, _enemys[i].speed), _enemys[i]);

        Debug.Log("���� ���� �Ϸ�");
    }
}

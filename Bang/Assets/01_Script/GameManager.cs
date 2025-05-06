using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    internal GameData gd;
    internal TurnManager tm;
    internal HandSortManager hm;
    internal CameraEffectManager cm;

    protected virtual void Start()
    {
        cm = GetComponentInChildren<CameraEffectManager>();
        tm = GetComponent<TurnManager>();
        gd = GameData.instance;
        hm = FindObjectOfType<HandSortManager>();
    }

    private void Awake()
    {
       
    }

    public void Init()
    {
        Debug.Log("시작");

        gd.player.ShowLife();
        gd.player.ChangeGun(gd.playerWeapon);
        StartCoroutine(tm.ActTurn(1f));
    }

    //임시 작동 키
    protected virtual void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    hm.sortBig = !hm.sortBig;
        //    hm.SortHand();
        //}
    }

    public void EndPlayerTurn()
    {
        if(gd.player)
        {
            gd.playerTurn = false;
            gd.player.ChangeState(STATE.NONE);
            StartCoroutine(tm.ActTurn());
        }
    }
}

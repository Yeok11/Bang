using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : User
{
    [SerializeField] internal int actPower = 5;
    [SerializeField] internal int movePower = 0;
    [SerializeField] internal int shotPower = 0;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private TextMeshProUGUI actPowerText;
    [SerializeField] private Transform gunPos;

    public Transform gunParentsPos;


    public void ChangeState(STATE _state)
    {
        state = _state;
        
        GameData.instance.am.PreventUseCard(state);
        GameData.instance.gm.tm.TurnMesSign();

        GameData.instance.gm.hm.SortHand();
    }

    public void SetPower(STATE _powerType, int _powerValue, bool _addPower = false)
    {
        switch (_powerType)
        {
            case STATE.BATTLE:
                shotPower = _powerValue + (_addPower ? shotPower : 0);
                break;
            case STATE.MOVE:
                movePower = _powerValue + (_addPower ? movePower : 0);
                break;
            case STATE.SET:
                actPower = _powerValue + (_addPower ? actPower : 0);
                break;
        }

        SetMessage();
    }

    public void SetMessage()
    {
        actPowerText.SetText("남은 행동력 : " + actPower);
    }

    public void Move(Pos _pos)
    {
        if (tileManager.CheckBlock(new Pos(pos.x + _pos.x, pos.z + _pos.z)) != (int)TILETYPE.ROAD || movePower <= 0)
            return;

        --movePower;
        SetMessage();
        pos.Move(_pos, transform);

        Next();
    }

    public override void AnimeFin()
    {
        Debug.Log("fin" + shotPower + " / " + movePower + " / " + actPower);

        base.AnimeFin();

        

        ShowBullet();

        Next();
    }

    private void Next()
    {
        GameData.instance.playerTurn = true;
        Debug.Log("????");
        
        if (shotPower != 0) ChangeState(STATE.BATTLE);

        else if (movePower != 0) ChangeState(STATE.MOVE);

        else
        {
            ChangeState(STATE.SET);
        }
    }

    public void ChangeGun(Weapon _changeWeapon)
    {
        weapon = _changeWeapon;
        
        //2d 이미지
        gunPos.GetChild(1).GetComponent<Image>().sprite = weapon.image;
        //3d 이미지
        for (int i = 1; i < 4; i++)
            gunParentsPos.GetChild(i).gameObject.SetActive(false);

        switch (weapon.gunType)
        {
            case GUNTYPE.RIFLE:
                gunParentsPos.GetChild(2).gameObject.SetActive(true);
                break;
            case GUNTYPE.SHOTGUN:
                gunParentsPos.GetChild(3).gameObject.SetActive(true);
                break;
            case GUNTYPE.REVOLVER:
                gunParentsPos.GetChild(1).gameObject.SetActive(true);
                break;
        }

        nowBullet = weapon.nowBullet;
        ShowBullet();
    }

    public override void Reload(int reloadCnt = 1)
    {
        base.Reload(reloadCnt);

        ShowBullet();
    }

    public void ShowBullet()
    {
        for (int i = 0; i < gunPos.GetChild(0).childCount; i++)
            gunPos.GetChild(0).GetChild(i).gameObject.SetActive(nowBullet > i);
    }

    public override void Bang(User _target, Vector3 _transform)
    {
        base.Bang(_target, _transform);
    }
}

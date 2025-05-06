using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : User
{
    public void EnemyAction()
    {
        if (DetectPlayer())
        {
            Debug.Log("Find Player pos");
            if (nowBullet == 0)
                ++nowBullet;
            Bang(GameData.instance.player, GameData.instance.player.transform.position);
        }
        else
        {
            Move();
            StartCoroutine(GameData.instance.gm.tm.ActTurn());
        }
    }

    private void Start()
    {
        if (weapon == null)
            weapon = GetComponent<User>().weapon;
        if (pos == null)
            pos = GetComponent<User>().pos;
    }

    //�÷��̾ ���� ���� �ִ°�
    public bool DetectPlayer()
    {
        int xroop = weapon.range * 2 + 1, q, p;
        Pos pPos = GameData.instance.player.pos;

        for (int i = 0; i < xroop; i++)
        {
            q = xroop - (weapon.range + 1 + i);
            p = xroop - Mathf.Abs(q * 2);
            for (int y = 0; y < p; ++y)
                if (pPos.x == (pos.x - weapon.range) + i && pPos.z == pos.z - ((p - 1) / 2) + y) return true;
        }

        Debug.Log("player�� ���� ���� ����");

        return false;
    }

    private bool TileCheck(Route _tilePos)
    {
        //���� ���� ���������̸� �� �̻� ã�� ����
        if(_tilePos.beforeRoute != null)
        {
            if (_tilePos.pos.EqualPos(GameData.instance.player.pos)) return false;
        }

        //���� ����
        if (_tilePos.pos.x >= GameData.instance.size || _tilePos.pos.x < 0) return false;
        if (_tilePos.pos.z >= GameData.instance.size || _tilePos.pos.z < 0) return false;

        //���� ��� Ž��
        foreach (var item in _tilePos.checkedTiles)
        {
            if (item.EqualPos(_tilePos.pos)) return false;
        }

        //Ÿ�� Ȯ��
        return 1 == GameData.instance.nowMap[_tilePos.pos.x, _tilePos.pos.z];
    }

    private Pos[] SetCrossTileOrder(Pos _pos)
    {
        Pos[] returnPos = { new Pos(0,1), new Pos(1, 0), new Pos(0, -1), new Pos(-1, 0) };

        if(_pos.z > GameData.instance.player.pos.z)
        {
            Pos temp = returnPos[0];
            returnPos[0] = returnPos[2];
            returnPos[2] = temp;
        }

        if (_pos.x < GameData.instance.player.pos.x)
        {
            Pos temp = returnPos[1];
            returnPos[1] = returnPos[3];
            returnPos[3] = temp;
        }

        return returnPos;
    }

    #region ��ã��
    private Route CrossRouteCheck(Route route)
    {
        Route imRoute = new Route(route.pos, route);
        Pos[] crossTile = SetCrossTileOrder(route.pos);

        for (int i = 0; i < 4; i++)
        {
            Route nextRoute = new Route(new Pos(route.pos.x + crossTile[i].x, route.pos.z + crossTile[i].z), route);
            nextRoute.Check();
            if (TileCheck(nextRoute))
            {
                imRoute.nextRoute.Add(CrossRouteCheck(nextRoute));
            }
            else
            {
                if (nextRoute.pos.EqualPos(GameData.instance.player.pos))
                    imRoute.nextRoute.Add(nextRoute);
            }
        }

        if (imRoute.nextRoute.Count == 0) 
            return null;

        return imRoute;
    }
    private List<Route> selectRoute = new List<Route>();
    public void Move()
    {
        //ó�� ����
        Route route = new Route(pos);

        route = CrossRouteCheck(route);

        ShowRoute(route, 0);
        Route a = selectRoute[0];
        a.beforeRoute = selectRoute[0].beforeRoute;

        //�ڽ�Ʈ Ȯ��
        for (int i = 1; i < selectRoute.Count; i++)
        {
            if (a.cost > selectRoute[i].cost)
            {
                a = selectRoute[i];
                a.beforeRoute = selectRoute[i].beforeRoute;
            }
        }

        //���� �θ�� ���� �ڵ�
        while (!a.beforeRoute.pos.EqualPos(pos))
        {
            //���� ó��
            if (a.beforeRoute == null)
            {
                Debug.Log("error");
                return;
            }
            a = a.beforeRoute;
        }

        Pos result;

        result = new Pos(a.pos.x - pos.x, a.pos.z - pos.z);
        pos.Move(result, transform);
    }
    private void ShowRoute(Route _route, int _cnt)
    {
        if(_route.pos.EqualPos(GameData.instance.player.pos))
        {
            Route answer = new Route(_route.pos, _route.beforeRoute);
            answer.cost = _cnt;

            selectRoute.Add(answer);
        }

        foreach (Route item in _route.nextRoute)
        {
            if(item != null)
                ShowRoute(item, ++_cnt);
        }
    }
    #endregion

    public override void AnimeFin()
    {
        StartCoroutine(GameData.instance.gm.tm.ActTurn());
        base.AnimeFin();
    }
}
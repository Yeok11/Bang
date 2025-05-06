using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public Pos pos;
    public TILETYPE tileType = TILETYPE.NONE;

    private User player;

    private MeshRenderer selfMeshRender;
    private Material originMat;

    private TileManager tm;
    private bool canTrigger = false;

    private void Start()
    {
        selfMeshRender = GetComponent<MeshRenderer>();
        originMat = selfMeshRender.material;
        tm = GetComponentInParent<TileManager>();
        player = GameData.instance.player;
    }

    private bool CanPlayerMoveTile()
    {
        int canMoveTileCnt = 1;

        for (int i = -canMoveTileCnt; i <= canMoveTileCnt; i++)
        {
            if (i == 0) continue;

            if (player.pos.x + i == pos.x && player.pos.z == pos.z) return true;
        }

        for (int i = -canMoveTileCnt; i <= canMoveTileCnt; i++)
        {
            if (i == 0) continue;

            if (player.pos.z + i == pos.z && player.pos.x == pos.x) return true;
        }

        return false;
    }

    private bool CanPlayerAttackTile()
    {
        Weapon weapon = GameData.instance.player.weapon;
        int xroop = weapon.range * 2 + 1, q, p;
        Pos pPos = GameData.instance.player.pos;


        for (int i = 0; i < xroop; i++)
        {
            q = xroop - (weapon.range + 1 + i);
            p = xroop - Mathf.Abs(q * 2);
            for (int y = 0; y < p; ++y)
                if (pos.x == (pPos.x - weapon.range) + i && pos.z == pPos.z - ((p - 1) / 2) + y) return true;
        }
        return false;
    }

    private void OnMouseEnter()
    {
        if (tileType != TILETYPE.ROAD) return;

        if ((player.state == STATE.BATTLE && CanPlayerAttackTile()) || (player.state == STATE.MOVE && CanPlayerMoveTile()))
        {
            selfMeshRender.material = tm.selectMat;
            canTrigger = true;
        }       
    }

    private void OnMouseDown()
    {
        if (tileType != TILETYPE.ROAD || !GameData.instance.playerTurn || (GameData.instance.player.state != STATE.BATTLE && GameData.instance.player.state != STATE.MOVE)) return;

        if (canTrigger)
        {
            canTrigger = false;
            GameData.instance.playerTurn = false;
            Debug.Log(player.state + " 상호작용");
            tm.ClickBlock(pos, transform);
        }       
    }

    private void OnMouseExit()
    {
        selfMeshRender.material = originMat;
        canTrigger = false;
    }
}

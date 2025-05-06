using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : GameManager
{
    private User player;
    private Player pPlayer;

    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] private Transform tileParent;

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Material[] tileColor;
    public Material selectMat;
    [SerializeField] private List<Tile> tiles;

    private int[,] map =
    {
        { 0, 1, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 0, 0, 0, 1 },
        { 0, 1, 1, 1, 1, 0, 1 },
        { 0, 1, 0, 1, 1, 1, 1 },
        { 1, 1, 1, 0, 1, 0, 0 },
        { 0, 1, 0, 1, 1, 0, 0 },
        { 0, 1, 1, 1, 1, 0, 0 }
    };
    private int[,] characterMap =
    {
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 2 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 2, 0, 0, 0, 0 }
    };

    [SerializeField] private TurnManager turnm;


    protected override void Start()
    {
        base.Start();
        player = gd.player;
        pPlayer = player.GetComponent<Player>();

        SpawnTile();
        gd.nowMap = map;
        gd.nowTiles = tiles;

        cm.UseEffect("BattleStart");
    }

    protected override void Update()
    {
    }

    private void InitCharacter(int _height, int _width)
    {
        int p = characterMap[_width, _height];

        if (p != 0)
        {
            //플레이어인지 판단
            if ((CHARACTER)p == CHARACTER.PLAYER)
            {
                player.pos = new Pos(_width, _height);
                player.transform.localPosition = tiles[tiles.Count - 1].transform.localPosition + Vector3.up * 1.2f;
                player.role = CHARACTER.PLAYER;
                return;
            }

            //에너미 소환
            SpawnEnemy(new Pos(_width, _height), p);
        }
    }

    private void SpawnEnemy(Pos _pos, int type)
    {
        Enemy enemy = Instantiate(enemyPrefabs[type - 2]).GetComponent<Enemy>();
        enemy.role = (CHARACTER)type;
        enemy.pos = _pos;
        enemy.ShowLife();
        enemy.nowBullet = enemy.weapon.nowBullet;
        enemy.transform.localPosition = tiles[tiles.Count - 1].transform.localPosition + Vector3.up * 1.2f;
        gd.enemys.Add(enemy);
    }

    private void SpawnTile()
    {
        int mapSize = gd.size / 2;

        for (int z = 0; z < gd.size; ++z)
        {
            for (int x = 0; x < gd.size; ++x)
            {
                Transform trm = Instantiate(tilePrefab, transform).transform;
                trm.parent = tileParent;
                trm.localPosition = new Vector3(-5 * (mapSize - x), 0, 5 * (mapSize - z));
                trm.gameObject.GetComponent<MeshRenderer>().material = tileColor[map[x, z]];

                Tile tile = trm.GetComponent<Tile>();
                tile.pos = new Pos(x, z);
                tile.tileType = (TILETYPE)map[x, z];

                tiles.Add(tile);

                InitCharacter(z, x);
            }
        }
    }

    private User IsTileEnemy(Pos _pos)
    {
        foreach (User item in gd.enemys)
        {
            Debug.Log(item.gameObject.name + "의 pos값은 " + item.pos.x + " / " + item.pos.z);
            if (item.pos.x == _pos.x && item.pos.z == _pos.z)
                return item;
        }

        return null;
    }

    public void ClickBlock(Pos _pos, Transform _tileTransform)
    {
        Debug.Log("보드 클릭");
        switch (gd.player.state)
        {
            case STATE.BATTLE:
                User target = IsTileEnemy(_pos);
                
                Vector3 targetPos = new Vector3(_tileTransform.position.x, 0, _tileTransform.position.z);
                gd.player.Bang(target, targetPos);
                --gd.player.shotPower;
                break;
            case STATE.MOVE:
                pPlayer.Move(new Pos(_pos.x - player.pos.x, _pos.z - player.pos.z));
                break;

            case STATE.NONE:
            case STATE.SET:
            default:
                return;
        }
    }

    public int CheckBlock(Pos _pos)
    {
        return map[_pos.x, _pos.z];
    }
}

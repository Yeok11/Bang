using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public Player player;
    internal bool playerTurn = false;
    [SerializeField] internal Weapon playerWeapon;
    [SerializeField] internal List<User> characterOrder;
    [SerializeField] internal List<User> enemys;

    //µ¦
    [SerializeField] internal List<CardSO> deck;
    [SerializeField] internal List<GameObject> trashCard;

    internal int[,] nowMap;
    internal List<Tile> nowTiles;
    public int size = 7;

    public GameManager gm;
    public ActionManager am;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(instance);
        }
    }
}

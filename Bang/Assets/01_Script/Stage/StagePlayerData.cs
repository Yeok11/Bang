using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePlayerData : MonoBehaviour
{
    [SerializeField] internal List<CardSO> userDeck;
    [SerializeField] private int Hp;
    [SerializeField] private int startHandCnt;
    [SerializeField] internal List<StageData> stage;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded()
    {
        GameData gd = FindObjectOfType<GameData>();
        if(gd != null)
        {
            gd.deck = userDeck;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public CARD cardType;
    public int cost;
    public Sprite icon;
    [TextArea]
    public string explain;
}

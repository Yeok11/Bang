using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandSortManager : HandManager
{
    [Header("SmallHandPos")]
    [SerializeField] private Transform sLeft;
    [SerializeField] private Transform sRight;
    [SerializeField] private Transform sMid;
    [Header("BigHandPos")]
    [SerializeField] private Transform bLeft;
    [SerializeField] private Transform bRight;
    [SerializeField] private Transform bMid;


    [Header("other Data")]
    public Board board;
    public CardEffectManager cem;
    public GameObject otherUI;

    public bool sortBig = false;

    private void Start()
    {
        board.gameObject.SetActive(false);
    }


    public void SortHand(bool settingSize = true)
    {
        sortBig = GameData.instance.player.state == STATE.SET;

        Transform left = sortBig ? bLeft : sLeft;
        Transform right = sortBig ? bRight : sRight;
        Transform mid = sortBig ? bMid : sMid;

        for (int i = 1; i < handCards.Count; i++)
        {
            if(settingSize)
            //크기 조정
            handCards[i].SetSize(sortBig ? new Vector3(1f, 1.6f, 1f) : new Vector3(0.8f, 1.2f));

            //위치 설정 - 가로
            Vector3 movePos = Vector3.Lerp(left.position, right.position, (float)i / handCards.Count);

            //위치 설정 - 세로
            handCards[i].transform.DOMove(new Vector3(movePos.x,
                Vector3.Lerp(movePos, mid.position,
                1.0f - Mathf.Abs((float)i - (float)handCards.Count / 2) * 0.2f
                ).y, movePos.z), 1.2f);

            //각도 조정
            handCards[i].transform.rotation = Quaternion.Lerp(left.rotation, right.rotation, (float)i / handCards.Count);

            //부모 지정 및 핸드메니저 삽입
            handCards[i].transform.SetParent(transform);
            handCards[i].handManager = this;
        }
    }
}

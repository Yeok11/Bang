using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleStart : CameraEffect
{
    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform bot;
    [SerializeField] private Image face;
    [SerializeField] private TextMeshProUGUI message;

    private Sequence sq;

    public override void Init()
    {
        top.parent.gameObject.SetActive(true);
        top.DOScale(new Vector3(1, 1, 1), 0);
        bot.DOScale(new Vector3(1, 1, 1), 0);
        face.transform.DOLocalMove(new Vector3(-1500, 50, 0), 0);
        message.transform.DOLocalMove(new Vector3(1500, -30, 0), 0);
    }

    public override void ActionEffect()
    {
        sq = DOTween.Sequence();

        sq.AppendInterval(0.7f);

        sq.Append(bot.DOScaleY(0.5f, 1f));
        sq.Join(top.DOScaleY(0.5f, 1f));

        sq.AppendInterval(1.25f);

        sq.Append(face.transform.DOLocalMove(new Vector3(-20, 50), 0.4f));
        sq.Join(message.transform.DOLocalMove(new Vector3(70, -30), 0.4f));

        sq.Append(face.transform.DOLocalMove(new Vector3(50 + 70, 50), 0.8f));
        sq.Join(message.transform.DOLocalMove(new Vector3(-70, -30), 0.8f));

        sq.Append(face.transform.DOLocalMove(new Vector3(1500, 50), 0.6f));
        sq.Join(message.transform.DOLocalMove(new Vector3(-1500, -30), 0.6f));

        sq.Append(bot.DOScaleY(0, 1f));
        sq.Join(top.DOScaleY(0, 1f).OnComplete(()=>top.parent.gameObject.SetActive(false)));

        
        sq.OnComplete(()=> GameData.instance.gm.Init());
    }
}

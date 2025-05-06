using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private Transform mes;
    [SerializeField] private Transform cam;

    [SerializeField] private Transform btTitle;


    public void toTitle()
    {
        SceneManager.LoadScene(0);
    }

    private void Init()
    {
        cam.DOLocalMoveZ(15, 0);
        mes.DOScale(Vector3.zero, 0);
        btTitle.DOScale(Vector3.zero, 0);
    }

    public void Start()
    {
        GameObject g = GameObject.Find("Manager");
        if (g != null)
        {
            Destroy(g);
        }

        Init();
        Act();
    }

    private void Act()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(cam.DOLocalMoveZ(20, 4)).AppendInterval(1.2f);
        sq.Append(mes.DOScale(new Vector3(1,1,1), 0.3f).SetEase(Ease.InSine));
        sq.Append(btTitle.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.InBounce));
    }
}

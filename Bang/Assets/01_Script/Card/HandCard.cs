using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HandCard : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
{
    [SerializeField] private Vector3 lastScale;

    public bool inBoard = false;
    private bool canDrag = false;

    public HandSortManager handManager;

    public CardSO cardData;

    private RectTransform rectTransform;

    private Vector3 bigSize = new Vector3(1.2f, 2f, 1f);

    private int childPos;


    public void InitData(Transform _cardPos)
    {
        transform.SetParent(_cardPos);
        transform.position = _cardPos.position;

        if (cardData.icon != null) transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardData.icon;
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(cardData.cardName);
        transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(cardData.cost.ToString());
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetSize(Vector3 _size)
    {
        lastScale = _size;
        transform.DOScale(lastScale, 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (handManager.nowDrag) return;

        canDrag = true;
        handManager.nowDrag = true;
        childPos = rectTransform.GetSiblingIndex();
        rectTransform.SetAsLastSibling();
        SetSize(bigSize);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(inBoard && handManager.cem.CheckAct(cardData.cost))
        {
            handManager.handCards.Remove(this);
            handManager.cem.ActCard(this.cardData);
            PoolManager.instance.BackItem(this.gameObject);
        }
        DOTween.Kill(transform);

        handManager.board.gameObject.SetActive(false);
        handManager.otherUI.SetActive(true);

        handManager.SortHand(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canDrag && handManager.nowDrag)
        {
            rectTransform.SetSiblingIndex(childPos);
            DOTween.Kill(transform);
            handManager.SortHand();
            canDrag = false;
            handManager.nowDrag = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Camera maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(maincam, eventData.position);
        transform.localPosition = pos;
        //transform.DOLocalMove(eventData.position, 0);
    }

    private void CardWaveEffect(int _z)
    {
        transform.DOLocalRotate(new Vector3(0, 0, _z), 0.2f).OnComplete(()=>CardWaveEffect(-_z));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (childPos == rectTransform.GetSiblingIndex())
            rectTransform.SetAsLastSibling();

        handManager.board.gameObject.SetActive(true);
        handManager.otherUI.SetActive(false);

        DOTween.KillAll(transform);
        transform.DOScale(bigSize, 0);
        CardWaveEffect(2);
    }
}

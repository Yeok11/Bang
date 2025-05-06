using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WantedManager : MonoBehaviour
{
    private Transform wantedParentPos;
    
    //마우스 올려 놓았을 때 이펙트
    private Transform wantedEffectPos;
    private Transform lastPos;
    private GameObject preventPanel;
    
    private GameObject DarkView;

    [SerializeField] internal StagePlayerData spd;

    [SerializeField] internal WANTED_STATE wantedState;

    [SerializeField] internal float tableView;
    [SerializeField] internal float boardView;
    private Camera cam;

    [SerializeField] internal Sprite endillust;
    [SerializeField] internal Sprite joji;

    private bool isViewTable = false;

    internal int stageNum = -1;

    private void Init()
    {
        wantedParentPos = GameObject.Find("Wanted Parent").transform;
        wantedEffectPos = GameObject.Find("EffectPos").transform;
        preventPanel = GameObject.Find("PreventPanel");
        DarkView = GameObject.Find("DarkView");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    private void Start()
    {
        Init();

        spd = FindObjectOfType<StagePlayerData>();
        DarkView.SetActive(false);
        StageUpdate();
    }

    private void OnLevelWasLoaded(int level)
    {
        Init();
        StageUpdate();
    }

    private void Awake()
    {
        if (GameObject.FindWithTag("Manager") != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameObject.tag = "Manager";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            CameraMove();
        }
    }

    public void CameraMove()
    {
        cam.transform.DOLocalMoveY((isViewTable ? tableView : boardView), 1.3f);
        isViewTable = !isViewTable;
    }

    public void StageUpdate()
    {
        Debug.Log("Stage Update");
        StartCoroutine(InitWantedPaper());
    }

    private Reward GetCardData(List<Reward> _rewards)
    {
        int allPer = 0;

        //전체 퍼센트
        for (int i = 0; i < _rewards.Count; i++)
            allPer += _rewards[i].per;

        allPer = Random.Range(0, allPer);

        for (int i = 0; i < _rewards.Count; i++)
        {
            allPer -= _rewards[i].per;
            if(allPer <= 0)
            {
                Reward cardReward = _rewards[i];
                _rewards.RemoveAt(i);

                return cardReward;
            }
        }

        return null;
    }

    private void ShowWantedPaper(int _cnt = 0)
    {
        List<Reward> rewardCards = new List<Reward>();
        Sequence sq = DOTween.Sequence();

        for (int i = 0; i < spd.stage[stageNum].rewardsList.Count; i++)
        {
            rewardCards.Add(spd.stage[stageNum].rewardsList[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            Transform g = wantedParentPos.GetChild(i);
            g.GetComponentInChildren<WantedPaper>().DataSet(GetCardData(rewardCards));
            if (_cnt > i)
            {
                sq.Append(g.DOLocalMoveY(0, 1.2f));
            }
        }

        sq.Delay();
        sq.Delay();

        sq.OnComplete(()=>
        {
            preventPanel.SetActive(false);
        });
    }

    private IEnumerator InitWantedPaper()
    {
        preventPanel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            GameObject g = wantedParentPos.GetChild(i).gameObject;
            g.transform.DOLocalMoveY(1000, 0);
        }

        yield return new WaitForSeconds(0.8f);
        ShowWantedPaper(spd.stage[++stageNum].paperCnt);
    }

    public void ShowWantedEffectOn(Transform _target)
    {
        lastPos = _target.parent;
        _target.parent = wantedEffectPos;
        DarkView.SetActive(true);
    }

    public Transform ShowWantedEffectOff()
    {
        DarkView.SetActive(false);
        return lastPos;
    }
}

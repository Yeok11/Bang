using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class DeadAnime : MonoBehaviour
{
    [SerializeField] private Animator anime;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private Transform subCam;
    [SerializeField] private GameObject ui;
    [SerializeField] private Animator playerAnime;

    private void Awake()
    {
        GameObject _manager = GameObject.Find("Manager");
        if (_manager != null)
        {
            Destroy(_manager);
        }
    }

    private void Start()
    {
        BangAnime();
    }

    public void BangAnime()
    {
        anime.SetBool("SuccessShot", true);
        anime.SetBool("return", true);
        anime.SetTrigger("Shot");
    }

    public void AnimeFin()
    {
        StartCoroutine(AnimeCoroutine());
    }

    private IEnumerator AnimeCoroutine()
    {
        playerAnime.SetTrigger("Die");
        yield return new WaitForSeconds(3.5f);
        mainCam.SetActive(false);
        subCam.DOLocalMoveY(5, 2.5f).OnComplete(() => {
            ui.SetActive(true);
        });   
    }

    public void StageScene()
    {
        SceneManager.LoadScene(1);
    }

}

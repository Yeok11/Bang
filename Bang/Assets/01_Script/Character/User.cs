using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class User : MonoBehaviour
{
    public int maxHp = 20;
    public int hp = 8;
    public int speed = 5;
    public int nowBullet;

    internal Pos pos;
    public STATE state = STATE.NONE;
    public CHARACTER role;
    public Transform hpParentPos;

    public Weapon weapon;
    public Animator anime;
    private User bangTarget;

    public Transform GunHead;

    public virtual void Reload(int reloadCnt = 1)
    {
        for (int i = 0; i < reloadCnt; i++)
        {
            ++nowBullet;
        }
        if (weapon.maxBullet < nowBullet) nowBullet = weapon.maxBullet;
    }

    public virtual void Bang(User _target, Vector3 _transform)
    {
        bool canShot = nowBullet != 0;

        bangTarget = _target;

        transform.GetChild(0).DOLookAt(_transform, 0.75f);

        anime.SetBool("SuccessShot", canShot);
        anime.SetTrigger("Shot");

        if (!canShot)
        {
            Debug.Log("no bullet");
            return;
        }
    }

    public void Damage()
    {
        float damage = weapon.damage;

        if (weapon.nextBulletCri)
        {
            damage *= 1.5f;
            weapon.nextBulletCri = false;
        }

        if (bangTarget == null) return;

        bangTarget.hp -= (int)damage;
        --nowBullet;

        bangTarget.ShowLife();

        bangTarget.Dead();

        bangTarget = null;
    }

    public virtual void AnimeFin()
    {
        Debug.Log("fin anime");

        if(anime.GetBool("Success Shot"))
        Damage();
    }

    public void Heal(int _value = 1)
    {
        hp += _value;
        if (hp > maxHp) hp = maxHp;
        ShowLife();
    }

    public void ShowLife()
    {
        int _hp = Mathf.CeilToInt((float)hp / 2);
        Debug.Log(hp + " / " + _hp);

        for (int i = 0; i < hpParentPos.childCount; i++)
        {
            hpParentPos.GetChild(i).gameObject.SetActive(_hp > i);
        }

        if (hp % 2 == 1)
            hpParentPos.GetChild(_hp - 1).GetComponent<Image>().fillAmount = 0.5f;
    }


    public virtual void Dead()
    {
        if(hp <= 0)
        {
            state = STATE.DEAD;
            gameObject.SetActive(false);
        }
    }
}

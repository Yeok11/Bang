using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> soPoolList;
    [SerializeField] private List<int> poolCnt;

    public static PoolManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            if (instance != this)
            Destroy(instance);

        for (int i = 0; i < soPoolList.Count; i++)
        {
            AddItem(i);
        }
    }

    public void AddItem(int _value, int _cnt = 10)
    {
        for (int j = 0; j < _cnt; j++)
        {
            Instantiate(soPoolList[_value], transform.position, Quaternion.identity).transform.SetParent(transform);
        }
    }

    public void BackItem(GameObject _item)
    {
        _item.transform.parent = transform;
        _item.transform.position = transform.position;
    }
}

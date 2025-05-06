using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    public static CameraEffectManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
                Destroy(instance);
        }
    }

    public void UseEffect(string _name)
    {
        CameraEffect effect = GetComponent(_name) as CameraEffect;
        effect.Init();
        effect.ActionEffect();
    }
}

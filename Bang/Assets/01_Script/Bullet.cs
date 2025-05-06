using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Shot()
    {
        transform.position += Vector3.forward * 5f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] float destroyLifeTime = .5f;
    void Start()
    {
        Destroy(gameObject, destroyLifeTime);
    }

}

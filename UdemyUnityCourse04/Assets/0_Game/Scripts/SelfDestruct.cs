using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    void Start()
    {
        float particleLifeTime = GetComponent<ParticleSystem>().main.duration;
        Invoke("DestroySelf", particleLifeTime);
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"**I got hit by** {other.gameObject.name}");
        GameObject.Destroy(this.gameObject);
    }
}

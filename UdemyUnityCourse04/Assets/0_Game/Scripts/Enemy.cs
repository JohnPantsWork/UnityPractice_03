using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int scorePerKill = 150;
    [SerializeField] int hitpoints = 2;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        parentGameObject = GameObject.FindWithTag("SpawnARuntime");
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitpoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitpoints--;
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerKill);
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        GameObject.Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float deathDelay = 1.5f;
    [SerializeField] bool isRigidBodyAdded = true;
    [SerializeField] int enemyDeathScore = 10;
    [SerializeField] int health = 100;

    MeshCollider enemyMeshCollider;
    Rigidbody enemyRigidbody;
    bool isAlive;
    int initialHealth;
    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        isAlive = true;
        AddNonTriggerMeshCollider();
        AddNonKinematicRigidbodyWithoutGravity();
    }



    private void OnParticleCollision(GameObject other)
    {
        // TODO: add some hit vfx here.


        ReduceEnemyHealth();   
        if (isAlive && health <= 0)
        {
            CreateDeathExplosion();
            HandleEnemyDeath();
        }
    }

    private void ReduceEnemyHealth()
    {
        health -= PlayerController.Instance.turretFirePower;
    }

    private void HandleEnemyDeath()
    {
        ScoreHandler.Instance.UpdateScore(enemyDeathScore);
        GetComponent<MeshCollider>().enabled = false;
        isAlive = false;
        Destroy(gameObject, deathDelay);
    }

    private void CreateDeathExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
        explosion.transform.localScale = Vector3.one * 10;
    }
    private void AddNonTriggerMeshCollider()
    {
        enemyMeshCollider = gameObject.AddComponent<MeshCollider>();
        enemyMeshCollider.sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        enemyMeshCollider.convex = true;
        enemyMeshCollider.isTrigger = false;
    }
    private void AddNonKinematicRigidbodyWithoutGravity()
    {
        if (!gameObject.GetComponent<Rigidbody>() && isRigidBodyAdded)
        {
            enemyRigidbody = gameObject.AddComponent<Rigidbody>();
            enemyRigidbody.useGravity = false;
        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}

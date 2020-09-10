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

    MeshCollider enemyMeshCollider;
    Rigidbody enemyRigidbody;
    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;

        AddNonTriggerMeshCollider();
        if (isRigidBodyAdded)
        {
            AddNonKinematicRigidbodyWithoutGravity();
        }
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
        enemyRigidbody = gameObject.AddComponent<Rigidbody>();
        enemyRigidbody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (isAlive)
        {
            CreateDeathExplosion();
            HandleEnemyDeath();
        }
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

    // Update is called once per frame
    void Update()
    {

    }
}

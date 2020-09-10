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

    MeshCollider meshCollider;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerMeshCollider();
        if (isRigidBodyAdded)
        {
            AddNonKinematicRigidbodyWithoutGravity();
        }
    }



    private void AddNonTriggerMeshCollider()
    {
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        meshCollider.convex = true;
        meshCollider.isTrigger = false;
    }
    private void AddNonKinematicRigidbodyWithoutGravity()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
        explosion.transform.localScale = Vector3.one * 10;

        ScoreHandler.Instance.UpdateScore(enemyDeathScore);
        GetComponent<MeshCollider>().enabled = false;
        Destroy(gameObject, deathDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

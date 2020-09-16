using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float deathDelay = 0.5f;
    [SerializeField] bool isRigidBodyAdded = true;
    [SerializeField] int enemyDeathScore = 10;
    [SerializeField] int health = 100;
    [SerializeField] bool isBoss = false;
    [SerializeField] bool isHealthUIPresent = false;
    [SerializeField] Slider healthSlider;

    MeshCollider enemyMeshCollider;
    Rigidbody enemyRigidbody;
    bool isAlive;
    int initialHealth;
    [SerializeField] float explosionMultiplier = 5f;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        isAlive = true;

        InitializeHealthUI();

        AddNonTriggerMeshCollider();

        AddNonKinematicRigidbodyWithoutGravity();
    }



    private void InitializeHealthUI()
    {
        if (isHealthUIPresent)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = initialHealth;
            SetHealthSliderValue();
        }
    }

    private void SetHealthSliderValue()
    {
        if (isHealthUIPresent)
        {
            healthSlider.value = health;
        }
    }



    private void OnParticleCollision(GameObject other)
    {
        // TODO: add some hit vfx here.


        ReduceEnemyHealth();
        if (isAlive && health <= 0)
        {
            Debug.Log("Yay!");
            CreateDeathExplosion();
            HandleEnemyDeath();
        }
    }

    private void ReduceEnemyHealth()
    {
        health -= PlayerController.Instance.turretFirePower;
        SetHealthSliderValue();

    }

    private void HandleEnemyDeath()
    {
        ScoreHandler.Instance.UpdateScore(enemyDeathScore);
        if (GetComponent<MeshCollider>())
        {
            GetComponent<MeshCollider>().enabled = false;
        }
        else
        {
            GetComponent<Collider>().enabled = false;
        }
        isAlive = false;
        Destroy(gameObject, deathDelay);
    }

    private void CreateDeathExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
        explosion.transform.localScale = transform.localScale * 2 * explosionMultiplier;
    }
    private void AddNonTriggerMeshCollider()
    {
        if (!transform.GetComponent<Collider>())
        {
            enemyMeshCollider = gameObject.AddComponent<MeshCollider>();
            enemyMeshCollider.sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            enemyMeshCollider.convex = true;
            enemyMeshCollider.isTrigger = false;
        }
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
        if (isBoss)
        {

        }
    }
}

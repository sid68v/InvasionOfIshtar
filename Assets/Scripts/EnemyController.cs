using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float deathDelay = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        print($"{other.name} {gameObject.name}");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
        explosion.transform.localScale = Vector3.one * 10;
        Destroy(gameObject, deathDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

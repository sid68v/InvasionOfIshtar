﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        print($"{other.name} {gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

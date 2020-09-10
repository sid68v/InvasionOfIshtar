using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        SendMessage("OnPlayerDeath");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

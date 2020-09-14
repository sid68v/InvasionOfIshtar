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
        switch (other.gameObject.tag)
        {
            case "EnemyBullet": SendMessage("OnBulletHit"); break;
            case "EnemyMediumBullet": SendMessage("OnMediumBulletHit"); break;
            default: SendMessage("OnPlayerDeath"); break;
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}

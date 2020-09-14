using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    GameObject playerShip;
    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        print(playerShip.name);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerShip.transform);
    }
}

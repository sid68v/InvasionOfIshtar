using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] bool isGunEquipped = false;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletVelocity = 100f;
    [SerializeField] float maxRangeOfAttack = 4f;
    [SerializeField] float minRangeOfAttack = 1f;
    [SerializeField] float attackInterval = 1f;

    GameObject playerShip;
    Transform gunEnd;
    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        gunEnd = transform.GetChild(1).transform;
        Invoke(nameof(StartFiring), Random.Range(2, 5));
    }

    private void StartFiring()
    {
        if (isGunEquipped)
        {
            fireCoroutine = StartCoroutine(StartFire());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(fireCoroutine);
    }

    IEnumerator StartFire()
    {
        while (true)
        {

            if ((Vector3.Distance(transform.position, playerShip.transform.position) < maxRangeOfAttack)
                && (Vector3.Distance(transform.position, playerShip.transform.position) > minRangeOfAttack)
                )
            {
                print("player in range");
                GameObject bullet = Instantiate(bulletPrefab, gunEnd.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity, ForceMode.Impulse);
                Destroy(bullet, 5f);
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerShip.transform);

        print($"{(Vector3.Distance(transform.position, playerShip.transform.position))} , minRange : {minRangeOfAttack} , maxRange : {maxRangeOfAttack}");
    }
}

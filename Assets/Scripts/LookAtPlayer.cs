using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] bool isLookAtPlayer = true;
    [SerializeField] bool isGunEquipped = false;

    [Header("Fill below only if guns are equipped.")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletVelocity = 100f;
    [SerializeField] float maxRangeOfAttack = 4f;
    [SerializeField] float minRangeOfAttack = 1f;
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float bulletSize = 1f;
    [SerializeField] float secondsGapBetweenMultipleFires = 2f;

    GameObject playerShip;
    List<GameObject> gunEnds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        gunEnds = FindGunEnds();
        Invoke(nameof(EnableFireAtPlayer), Random.Range(2, 5));
    }

    private List<GameObject> FindGunEnds()
    {
        List<GameObject> guns = new List<GameObject>();
        foreach (Transform childItem in transform)
        {
            if (childItem.CompareTag("Gun"))
            {
                guns.Add(childItem.gameObject);
            }
        }
        return guns;
    }

    private void EnableFireAtPlayer()
    {
        if (isGunEquipped)
        {
            StartCoroutine(StartFire());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(StartFire));
    }

    IEnumerator StartFire()
    {
        while (true)
        {

            if ((Vector3.Distance(transform.position, playerShip.transform.position) < maxRangeOfAttack)
                && (Vector3.Distance(transform.position, playerShip.transform.position) > minRangeOfAttack)
                )
            {
                foreach (GameObject gunEnd in gunEnds)
                {
                    GameObject bullet = Instantiate(bulletPrefab, gunEnd.transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity, ForceMode.Impulse);
                    bullet.transform.localScale *= bulletSize;
                    Destroy(bullet, 5f);
                    if (gunEnds.Count > 1)
                    {
                        yield return new WaitForSeconds(secondsGapBetweenMultipleFires);
                    }
                }

            }
            yield return new WaitForSeconds(attackInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLookAtPlayer)
        {
            transform.LookAt(playerShip.transform);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("General")]
    [Tooltip("Speed multipliers for x and y directions.")]
    [SerializeField] Vector2 offsets = new Vector2(2, 2);
    [SerializeField] Vector2 limits = new Vector2(5, 5);
    [SerializeField] float levelLoadDelay = 6f;

    public int turretFirePower = 10;
    public int playerHealth = 1000;

    [Header("3D rotation of the ship")]
    [SerializeField] float pitchPositionFactor = -5f;
    [SerializeField] float pitchThrowFactor = 15f;
    [SerializeField] float yawPositionFactor = 5f;
    //[SerializeField] float yawThrowFactor = -15f;  
    //[SerializeField] float rollPositionFactor = 4f;
    [SerializeField] float rollThrowFactor = -60f;

    [Header("Referenced child objects")]
    [SerializeField] GameObject explosionGO;
    [SerializeField] GameObject structuralParent;
    [SerializeField] ParticleSystem[] turretsParticles;


    float horizontalThrow;
    float verticalThrow;
    bool isAlive;


    BoxCollider playerCollider;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        isAlive = true;
        SetTurretStatus(false);
    }

    private void Update()
    {

        if (isAlive)
        {
            horizontalThrow = Input.GetAxis("Horizontal");
            verticalThrow = Input.GetAxis("Vertical");

            HandleMovement();
            HandleRotation();
            HandleFiring();

        }

        if (playerHealth <= 0)
        {
            OnPlayerDeath();
        }

    }

    private void HandleFiring()
    {
        if (Input.GetButton("Fire"))
        {
            SetTurretStatus(true);
        }
        else
        {
            SetTurretStatus(false);
        }
    }

    private void SetTurretStatus(bool isEmissionActive)
    {
        foreach (ParticleSystem turretParticle in turretsParticles)
        {
            ParticleSystem.EmissionModule particleEmissionModule = turretParticle.emission;
            particleEmissionModule.enabled = isEmissionActive;
        }
    }
    public void OnPlayerDeath() // function is referenced as string.
    {
        if (isAlive)
        {
            isAlive = false;
            playerHealth = 0;
            ScoreHandler.Instance.SetHealthText();

            explosionGO.SetActive(true);
            Invoke(nameof(DelayedFlame), 1f);
            ParticleSystem.MainModule particleMain = explosionGO.GetComponent<ParticleSystem>().main;
            particleMain.loop = true;
            SetTurretStatus(false);
            

            Rigidbody structuralRB = structuralParent.AddComponent<Rigidbody>();
            playerCollider.isTrigger = false;
            Invoke(nameof(RestartLevel), levelLoadDelay);

        }
    }

    public void OnBulletHit()
    {
        PlayerHealthDownByValue(10);
        ScoreHandler.Instance.SetHealthText();
    }

    public void PlayerHealthDownByValue(int hitValue)
    {
        playerHealth -= hitValue;
    }
    private void DelayedFlame()
    {
        explosionGO.transform.localScale = Vector3.one * 2;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void HandleRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchPositionFactor;
        float pitchDueToThrow = verticalThrow * pitchThrowFactor;

        float yawDueToPosition = transform.localPosition.x * yawPositionFactor;
        //float yawDueToThrow = horizontalThrow * yawThrowFactor;

        //float rollDueToPosition =  transform.localPosition.x* rollPositionFactor;
        float rollDueToThrow = horizontalThrow * rollThrowFactor;

        float pitch = pitchDueToPosition + pitchDueToThrow;
        float yaw = yawDueToPosition;//+yawDueToThrow;
        float roll = rollDueToThrow;//+rollDueToPosition;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void HandleMovement()
    {
        float xPos = Mathf.Clamp(transform.localPosition.x + horizontalThrow * offsets.x * Time.deltaTime, -limits.x, limits.x);
        float yPos = Mathf.Clamp(transform.localPosition.y + verticalThrow * offsets.y * Time.deltaTime, -limits.y, limits.y);
        float zPos = transform.localPosition.z;

        transform.localPosition = new Vector3(xPos, yPos, zPos);
    }
   
}

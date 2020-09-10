using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    [Header("General")]
    [Tooltip("Speed multipliers for x and y directions.")]
    [SerializeField] Vector2 offsets = new Vector2(2, 2);
    [SerializeField] Vector2 limits = new Vector2(5, 5);
    [SerializeField] float levelLoadDelay = 6f;

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

    float horizontalThrow;
    float verticalThrow;
    bool isAlive;

    private void Start()
    {

    }
    private void OnEnable()
    {
        isAlive = true;
    }

    private void Update()
    {

        if (isAlive)
        {
            horizontalThrow = Input.GetAxis("Horizontal");
            verticalThrow = Input.GetAxis("Vertical");

            HandleMovement();
            HandleRotation();

        }

    }


    public void OnPlayerDeath() // function is referenced as string.
    {
        if (isAlive)
        {
            isAlive = false;
            explosionGO.SetActive(true);
            Invoke("DelayedFlame", 1f);
            explosionGO.GetComponent<ParticleSystem>().loop = true;
            Rigidbody structuralRB = structuralParent.AddComponent<Rigidbody>();
            Invoke("RestartLevel", levelLoadDelay);
        }
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
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x + horizontalThrow * offsets.x, -limits.x, limits.x),
            Mathf.Clamp(transform.localPosition.y + verticalThrow * offsets.y, -limits.y, limits.y),
            transform.localPosition.z
            );
    }
}

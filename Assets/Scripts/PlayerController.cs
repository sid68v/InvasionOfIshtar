using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Speed multipliers for x and y directions.")]
    [SerializeField] Vector2 offsets = new Vector2(2, 2);
    [SerializeField] Vector2 limits = new Vector2(5, 5);
    [SerializeField] float pitchPositionFactor = -4f;
    [SerializeField] float pitchThrowFactor = 15f;
    [SerializeField] float yawPositionFactor = 4f;
    //[SerializeField] float yawThrowFactor = -15f;  
    //[SerializeField] float rollPositionFactor = 4f;
    [SerializeField] float rollThrowFactor = -50f;


    float horizontalThrow;
    float verticalThrow;


    private void Start()
    {

    }

    private void Update()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");

        HandleMovement();
        HandleRotation();

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered {other.name}");
    }

    private void HandleRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchPositionFactor;
        float pitchDueToThrow = verticalThrow * pitchThrowFactor;

        float yawDueToPosition = transform.localPosition.x * yawPositionFactor;
        //float yawDueToThrow = horizontalThrow * yawThrowFactor;

        //float rollDueToPosition =  transform.localPosition.x* rollPositionFactor;
        float rollDueToThrow = horizontalThrow * rollThrowFactor;

        float pitch = pitchDueToPosition+pitchDueToThrow;
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

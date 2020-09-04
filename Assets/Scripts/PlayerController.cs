using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 offsets = new Vector2(2, 2);
    public Vector2 limits = new Vector2(5, 5);
    private void Start()
    {

    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x + horizontal*offsets.x, -limits.x, limits.x),
            Mathf.Clamp(transform.localPosition.y + vertical*offsets.y, -limits.y, limits.y),
            transform.localPosition.z
            );

    }
}

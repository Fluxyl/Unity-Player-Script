using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool spacePressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private bool isGrounded;
    private int superJumpsRemaining;

    // Variable/Field Declaration


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Space Key is pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }
     
    // FixedUpdate is called once every physics update (Default 100Hz)
    void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * (float)1.5, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (spacePressed)
        {
            spacePressed = false;
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    void Jump()
    {
        if (superJumpsRemaining == 0)
        {
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
        } else
        {
            superJumpsRemaining--;
            rigidBodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
        }
        
    }
    
}

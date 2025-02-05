using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBotControl : MonoBehaviour
{
    public float moveSpeed = 5f;      // Speed of forward/backward movement
    public float turnSpeed = 100f;    // Speed of rotating the bot
   
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation so Rigidbody doesn't handle it

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MoveBot();
        
    }

    void MoveBot()
    {
        float moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrow
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow

        // Move forward/backward
        Vector3 move = transform.forward * moveInput * moveSpeed * Time.deltaTime;

        // Raycast ahead of the bot to check for walls
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f)) // Adjust the 1f range as needed
        {
            if (hit.collider.CompareTag("Wall"))  // Make sure the wall has the "Wall" tag
            {
                // Stop moving if hitting a wall
                move = Vector3.zero;
            }
        }

        rb.MovePosition(rb.position + move);

        // Rotate the bot using Rigidbody angular velocity
        float turnAmount = turnInput * turnSpeed * Time.deltaTime;
        rb.angularVelocity = new Vector3(0, turnAmount, 0); // Apply rotation via Rigidbody
    }
}

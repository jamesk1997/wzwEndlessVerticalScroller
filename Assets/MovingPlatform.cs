using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float baseSpeed = 2f;          // Base speed for platform movement
    public float randomSpeedRange = 1f;  // How much the speed can vary
    public float moveRange = 2f;         // Range of movement

    private float moveSpeed;             // Actual speed for this platform
    private Vector3 startPosition;       // Starting position of the platform

    void Start()
    {
        // Save the starting position
        startPosition = transform.position;

        // Assign a random speed based on the base speed and range
        moveSpeed = baseSpeed + Random.Range(-randomSpeedRange, randomSpeedRange);
    }

    void Update()
    {
        // Move the platform back and forth
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time * moveSpeed) * moveRange, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player lands on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Parent the player to the platform so they move together
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player leaves the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unparent the player so they can move independently
            collision.transform.SetParent(null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement
    private float highestPoint;        // Tracks the player's highest position

    private Vector3 offset;            // Offset between the camera and the player

    void Start()
    {
        // Calculate initial offset
        offset = transform.position - player.position;

        // Initialize highest point at the camera's starting Y position
        highestPoint = transform.position.y;
    }

    void LateUpdate()
    {
        // Update the highest point based on the player's position
        if (player.position.y > highestPoint)
        {
            highestPoint = player.position.y;
        }

        // Target position for the camera: Always stay at or above the highest point
        Vector3 targetPosition = new Vector3(
            transform.position.x,
            Mathf.Max(player.position.y + offset.y, highestPoint + offset.y),
            transform.position.z
        );

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}

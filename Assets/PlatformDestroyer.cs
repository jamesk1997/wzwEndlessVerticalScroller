using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public Transform player;                // Reference to the player
    public float yOffset = -5f;             // The vertical distance below the player where the destroyer will be
    public string platformTag = "Platform"; // Tag for platforms to destroy

    private Vector3 offset;                 // To track the destroyer's offset from the player

    void Start()
    {
        // Initialize the offset based on the player's position
        offset = new Vector3(0, yOffset, 0);
    }

    void Update()
    {
        // Update the destroyer's position to follow the player
        transform.position = player.position + offset;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the colliding object is a platform, destroy it
        if (other.CompareTag(platformTag))
        {
            Destroy(other.gameObject);
        }
    }
}

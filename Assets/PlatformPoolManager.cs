using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPoolManager : MonoBehaviour
{
    public GameObject[] platformVariants;  // Array of platform prefabs
    public int poolSize = 20;              // Number of platforms in the pool
    public Transform player;               // Reference to the player
    public Transform ground;               // Reference to the ground object
    public float jumpForce = 10f;          // Player's jump force
    public float moveSpeed = 5f;           // Player's movement speed
    public float horizontalRangeBuffer = 1f; // Extra buffer for horizontal placement

    private Queue<GameObject> platformPool; // Holds the platforms
    private float highestPlatformY;         // Tracks the highest Y position of a platform
    private float gravity;                  // Gravity value from the physics system
    private float maxJumpHeight;            // Maximum vertical jump height
    private float maxHorizontalReach;       // Maximum horizontal distance the player can move
    private float minSpawnHeight;           // Minimum Y position for platform spawning

    void Start()
    {
        // Initialize gravity and jump calculations
        gravity = Mathf.Abs(Physics2D.gravity.y);
        maxJumpHeight = (jumpForce * jumpForce) / (2 * gravity);
        float maxAirTime = (2 * jumpForce) / gravity;
        maxHorizontalReach = maxAirTime * moveSpeed + horizontalRangeBuffer;

        // Set the minimum spawn height (4 units above the ground)
        minSpawnHeight = ground.position.y + 4f;

        // Initialize the pool
        platformPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, platformVariants.Length);
            GameObject platform = Instantiate(platformVariants[randomIndex]);
            platform.SetActive(false); // Deactivate initially
            platformPool.Enqueue(platform);
        }

        // Spawn the initial platform (only one)
        SpawnInitialPlatform();
    }

    void Update()
    {
        // Spawn new platforms dynamically as the player ascends
        if (player.position.y > highestPlatformY - 10f)
        {
            SpawnPlatform();
        }
    }

    void SpawnInitialPlatform()
    {
        // Retrieve the first platform from the pool
        GameObject platform = platformPool.Dequeue();
        platform.SetActive(true);

        // Position it slightly above the ground
        Vector3 startPosition = new Vector3(0, minSpawnHeight, 0);
        platform.transform.position = startPosition;

        // Update the highest platform position
        highestPlatformY = startPosition.y;

        // Re-enqueue the platform into the pool
        platformPool.Enqueue(platform);
    }

    public void SpawnPlatform()
    {
        // Retrieve a platform from the pool
        GameObject platform = platformPool.Dequeue();
        platform.SetActive(true);

        // Calculate camera bounds
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Calculate spawn position
        float yOffset = Random.Range(1f, maxJumpHeight * 0.9f); // Stay within max jump height
        float xPosition = Random.Range(-cameraWidth + 1f, cameraWidth - 1f); // Keep within camera width
        float spawnY = Mathf.Max(highestPlatformY + yOffset, minSpawnHeight); // Ensure spawn is above min height
        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0);

        // Set platform position and update the highest platform
        platform.transform.position = spawnPosition;
        highestPlatformY = spawnPosition.y;

        // Re-enqueue the platform into the pool
        platformPool.Enqueue(platform);
    }
}

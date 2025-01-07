using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformVariants; // Array of platform prefabs (variants)
    public int initialPlatforms = 10;     // Number of platforms to spawn at the start
    public float minYDistance = 1.5f;     // Minimum vertical distance between platforms
    public float maxYDistance = 3f;       // Maximum vertical distance between platforms
    public float horizontalRange = 3f;    // Horizontal range for platform placement

    [Header("Player Reference")]
    public Transform player;              // Reference to the player

    private float highestPlatformY;       // Tracks the highest Y position of a platform

    void Start()
    {
        // Spawn initial platforms
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform(i == 0); // Ensure the first platform is reachable
        }
    }

    void Update()
    {
        // Spawn platforms dynamically as the player ascends
        if (player.position.y > highestPlatformY - 10f)
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform(bool isInitial = false)
    {
        // Calculate spawn position
        float yOffset = isInitial ? 0 : Random.Range(minYDistance, maxYDistance);
        float xPosition = Random.Range(-horizontalRange, horizontalRange);
        Vector3 spawnPosition = new Vector3(xPosition, highestPlatformY + yOffset, 0);

        // Choose a random platform variant to spawn
        if (platformVariants.Length == 0)
        {
            Debug.LogError("No platform variants assigned!");
            return;
        }

        int randomIndex = Random.Range(0, platformVariants.Length);
        Instantiate(platformVariants[randomIndex], spawnPosition, Quaternion.identity);

        // Log spawn details for debugging
        Debug.Log($"Platform spawned at {spawnPosition} with variant {platformVariants[randomIndex].name}");

        // Update the highest platform position
        highestPlatformY = spawnPosition.y;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public Transform player;                // Reference to the player
    public TextMeshProUGUI scoreText;       // Reference to the UI Text component
    private float highestPoint = 0f;        // Tracks the player's highest point

    void Update()
    {
        // Update the highest point if the player climbs higher
        if (player.position.y > highestPoint)
        {
            highestPoint = player.position.y;
        }

        // Update the score text to show the height
        scoreText.text = "Score: " + Mathf.FloorToInt(highestPoint).ToString();
    }
}

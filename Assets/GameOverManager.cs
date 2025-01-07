using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Transform player;                // Reference to the player
    public Canvas gameOverCanvas;           // Reference to the Game Over UI Canvas
    public float cameraOffset = 5f;         // Distance below the camera to trigger Game Over

    private bool isGameOver = false;        // To prevent multiple triggers

    void Update()
    {
        // Check if the player falls below the visible area of the camera
        if (!isGameOver && player.position.y < Camera.main.transform.position.y - cameraOffset)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        gameOverCanvas.gameObject.SetActive(true); // Show the Game Over UI
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Quit the game (only works in builds)
    }
}

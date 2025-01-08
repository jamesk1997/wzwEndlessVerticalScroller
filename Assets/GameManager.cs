using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Assign your GameOverCanvas in the Inspector
    public Transform player; // Assign your Player Transform in the Inspector
    private bool isGameOver = false;

    void Start()
    {
        gameOverCanvas.SetActive(false); // Ensure the game-over screen is hidden initially
    }

    void Update()
    {
        if (!isGameOver && PlayerBelowCamera()) // Check if the player is below the camera
        {
            GameOver();
        }
    }

    bool PlayerBelowCamera()
    {
        // Get the bottom Y position of the camera's view
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        // Check if the player's Y position is below the camera's bottom
        return player.position.y < cameraBottomY;
    }

    void GameOver()
    {
        Debug.Log("Game Over Triggered");
        isGameOver = true;
        gameOverCanvas.SetActive(true); // Show the game-over screen
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}

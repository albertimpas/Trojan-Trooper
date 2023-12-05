using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    private bool isPaused = true;

    public GameObject pauseCanvas;
    public GameObject pauseButton;



    public int mainMenuSceneIndex = 0;

    void Start()
    {
    // Initially, ensure the pause menu is inactive
    pauseCanvas.SetActive(false);
    isPaused = false;

    }

    void Update()
    {

    }

    public void PauseGame()
    {

        Time.timeScale = 0f; // Pause the game.
        pauseCanvas.SetActive(true); // Show the pause menu.
        pauseButton.SetActive(false); // Hide the pause button.

        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Unpause the game.
        pauseCanvas.SetActive(false); // Hide the pause menu.
        pauseButton.SetActive(true); // Show the pause button
    }

    public void ReturnToMainMenu()
    {
        // Load your main menu scene. Replace "MainMenuScene" with your actual scene name.
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}

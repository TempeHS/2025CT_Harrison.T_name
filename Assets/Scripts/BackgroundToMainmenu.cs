using UnityEngine;
using UnityEngine.SceneManagement;

public class PixelArtToMainMenuTransitioner : MonoBehaviour
{
    public string nextSceneName = "Scenes/Main menu"; // Name of the main menu scene

    void Update()
    {
        // Check if any keyboard key or mouse button is pressed
        if (Input.anyKeyDown)
        {
            Debug.Log("Any key pressed. Loading next scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName); // Load the main menu scene
        }
    }
}
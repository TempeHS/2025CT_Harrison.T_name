using UnityEngine;
using UnityEngine.SceneManagement;

public class PixelArtToMainMenuTransitioner : MonoBehaviour
{
    public string nextSceneName = "Scenes/Main menu"; 

    void Update()
    {
    
        if (Input.anyKeyDown)
        {
            Debug.Log("Any key pressed. Loading next scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName); 
        }
    }
}
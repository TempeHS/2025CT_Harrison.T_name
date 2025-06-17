using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneTransitioner : MonoBehaviour
{
    public string nextIntroSceneName = "Scenes/TitleScreen"; // Name of the next scene to load after the video finishes

    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not found on this GameObject.");
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing. Transitioning to next scene: " + nextIntroSceneName);
        SceneManager.LoadScene(nextIntroSceneName);
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished; // Unsubscribe from the event
        }
    }
}


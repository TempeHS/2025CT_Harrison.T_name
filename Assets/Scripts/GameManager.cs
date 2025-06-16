using UnityEngine;
using UnityEngine.UI;   
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore = 0;
    public TMP_Text scoreText;

    public int scorePerHit = 89;
    public int scorePerMiss = -47;

    public GameObject startScreenUI;

    public GameObject endScreenUI;
    public TMP_Text finalScoreText;
    public TMP_Text rankText;

    public Button restartButton;
    public Button nextLevelButton;

    public int nextLevelScoreThreshold = 30000; // Score threshold to unlock next level

    public GameObject pauseScreenUI;
    private bool isPaused = false;
    private bool gameFinished = false;

    private bool isApplicationInBackground = false;

    public float visualScrollSpeedMultiplier = 1.0f;
    public Slider noteSpeedSlider;
    public TMP_Text speedValueText;

    private float originalMusicVolume;
    public float endGameMusicVolume = 0.5f; // Volume for the end game music
    public float endGameMufflePitch = 0.8f;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        currentScore = 0;
        startPlaying = false;
        gameFinished = false;
        isApplicationInBackground = false;

        UpdateScoreDisplay();

        if (endScreenUI != null)
        {
            endScreenUI.SetActive(false);
        }
        if (pauseScreenUI != null)
        {
            pauseScreenUI.SetActive(false);
        }
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (theMusic != null)
        {
            originalMusicVolume = theMusic.volume;
        }
    }


    void Start()
    {



        if (pauseScreenUI != null)
        {
            pauseScreenUI.SetActive(false);
        }

        if (noteSpeedSlider != null)
        {
            noteSpeedSlider.value = visualScrollSpeedMultiplier;
            noteSpeedSlider.onValueChanged.AddListener(SetVisualScrollSpeed);
        }
        UpdateSpeedValueText();
    }

    void Update()
    {
        if (!startPlaying && !gameFinished)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();

                if (startScreenUI != null)
                {
                    startScreenUI.SetActive(false);
                }

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        else
        {
            if (theMusic != null && !theMusic.isPlaying && theBS.hasStarted && !isPaused && !isApplicationInBackground)
            {
                EndGame();
            }
        }




        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isApplicationInBackground = !hasFocus;

        if (theMusic != null)
        {
            if (!hasFocus && theMusic.isPlaying && startPlaying && !isPaused)
            {
                theMusic.Pause();
            }
            else if (hasFocus && !theMusic.isPlaying && startPlaying && !isPaused)
            {
                theMusic.UnPause();
            }
        }
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (pauseScreenUI != null)
        {
            pauseScreenUI.SetActive(true);
        }

        if (theMusic != null && theMusic.isPlaying)
        {
            theMusic.Pause();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (pauseScreenUI != null)
        {
            pauseScreenUI.SetActive(false);
        }

        if (theMusic != null && !theMusic.isPlaying)
        {
            theMusic.UnPause();
        }
    }


    public void SetVisualScrollSpeed(float newSpeed)
    {
        visualScrollSpeedMultiplier = newSpeed;
        UpdateSpeedValueText();
        Debug.Log("Note Scroll Speed set to: " + visualScrollSpeedMultiplier.ToString("F1") + "x");
    }


    private void UpdateSpeedValueText()
    {
        if (speedValueText != null)
        {
            speedValueText.text = visualScrollSpeedMultiplier.ToString("F1") + "x";
        }
    }



    public void EndGame()
    {
        startPlaying = false;
        theBS.hasStarted = false;
        gameFinished = true;

        if (endScreenUI != null)
        {
            endScreenUI.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + currentScore.ToString();
        }

        CalculateAndDisplayRank();

        if (nextLevelButton != null)
        {
            if (currentScore >= nextLevelScoreThreshold)
            {
                nextLevelButton.gameObject.SetActive(true); // Show next level button
            }
            else
            {
                nextLevelButton.gameObject.SetActive(false); // Hide next level button
            }
        }
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true); // Always show restart button
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (theMusic != null)
        {
            theMusic.volume = endGameMusicVolume;
            theMusic.pitch = endGameMufflePitch;

            if (!theMusic.isPlaying)
            {
                theMusic.Play();
            }
        }

    }


    private void CalculateAndDisplayRank()
    {
        string rank = "F";


        if (currentScore >= 60000)
        {
            rank = "S++";
        }
        else if (currentScore >= 58000)
        {
            rank = "S+";
        }
        else if (currentScore >= 55000)
        {
            rank = "S";
        }
        else if (currentScore >= 50000)
        {
            rank = "A+";
        }
        else if (currentScore >= 45000)
        {
            rank = "A";
        }
        else if (currentScore >= 40000)
        {
            rank = "B+";
        }
        else if (currentScore >= 35000)
        {
            rank = "B";
        }
        else if (currentScore >= 30000)
        {
            rank = "C+";
        }
        else if (currentScore >= 20000)
        {
            rank = "C";
        }
        else if (currentScore >= 10000)
        {
            rank = "D+";
        }
        else if (currentScore >= 8000)
        {
            rank = "D";
        }

        if (rankText != null)
        {
            rankText.text = "Rank: " + rank;
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        currentScore += scorePerHit;
        UpdateScoreDisplay();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        currentScore += scorePerMiss;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        gameFinished = false;
        isApplicationInBackground = false;

        if (theMusic != null)
        {
            theMusic.volume = originalMusicVolume;
            theMusic.pitch = 1f;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        gameFinished = false;
        isApplicationInBackground = false;

        if (theMusic != null)
        {
            theMusic.volume = originalMusicVolume;
            theMusic.pitch = 1f;
        }
        SceneManager.LoadScene("Scenes/Level 3");
    }
    
    public void GoMainMenu()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        gameFinished = false;
        isPaused = false;
        isApplicationInBackground = false;

        if (theMusic != null)
        {
            theMusic.volume = originalMusicVolume;
            theMusic.pitch = 1f;
            theMusic.Stop();
        }
        SceneManager.LoadScene("Scenes/Main menu");
    }

}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore = 0;
    public TMP_Text scoreText;

    public int scorePerHit = 89;
    public int scorePerMiss = -113;

    public GameObject startScreenUI;

    public GameObject endScreenUI;
    public TMP_Text finalScoreText;
    public TMP_Text rankText;

    public Button restartButton;
    public Button nextLevelButton;

    public int nextLevelScoreThreshold = 110000; // Score threshold to unlock next level

    public GameObject pauseScreenUI;
    public bool isPaused = false;
    private bool gameFinished = false;

    public int currentCombo;
    public int maxCombo;
    public TextMeshProUGUI comboText;
    public float comboDisplayDuration = 0.5f;
    private Coroutine comboDisplayCoroutine;

    private bool isApplicationInBackground = false;

    public float visualScrollSpeedMultiplier = 1.0f;
    public Slider noteSpeedSlider;
    public TMP_Text speedValueText;

    private float originalMusicVolume;
    public float endGameMusicVolume = 0.5f; // Volume for the end game music
    public float endGameMufflePitch = 0.8f;
    
    public float[] comboMultiplierTiers = { 0.5f, 1.0f, 1.2f, 1.5f, 2.0f, 3.0f }; // Multipliers for combos (e.g., 1x, 1.2x, etc.)
    public int[] comboThresholds = { 0, 10, 25, 50, 100, 200 }; // Combo counts to reach each tier
    public TextMeshProUGUI multiplierText; // UI text to display the current multiplier
    private float currentScoreMultiplier = 1.0f; // Current active multiplier


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


        currentCombo = 0;
        maxCombo = 0;

        if (comboText != null)
        {
            comboText.gameObject.SetActive(false); // Hide combo text initially
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
                
                ResetCombo();

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


        if (currentScore >= 180000)
        {
            rank = "SSS";
        }
        else if (currentScore >= 175000)
        {
            rank = "SS+";
        }
        else if (currentScore >= 170000)
        {
            rank = "SS";
        }
        else if (currentScore >= 150000)
        {
            rank = "S+";
        }
        else if (currentScore >= 130000)
        {
            rank = "S";
        }
        else if (currentScore >= 120000)
        {
            rank = "A+";
        }
        else if (currentScore >= 100000)
        {
            rank = "A";
        }
        else if (currentScore >= 90000)
        {
            rank = "B+";
        }
        else if (currentScore >= 80000)
        {
            rank = "B";
        }
        else if (currentScore >= 70000)
        {
            rank = "C+";
        }
        else if (currentScore >= 65000)
        {
            rank = "C";
        }
        else if (currentScore >= 50000)
        {
            rank = "D+";
        }
        else if (currentScore >= 450000)
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

        currentCombo++;
        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
        }

        CalculateCurrentMultiplier();
        int scoreToAdd = Mathf.RoundToInt(scorePerHit * currentScoreMultiplier);
        currentScore += scoreToAdd;

        UpdateScoreDisplay();
        UpdateComboText();
        UpdateMultiplierText();

    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        currentScore += scorePerMiss;
        UpdateScoreDisplay();

        ResetCombo();
        CalculateCurrentMultiplier();
        UpdateMultiplierText();

    }

    private void UpdateMultiplierText()
    {
        if (multiplierText != null)
        {
            multiplierText.text = currentScoreMultiplier.ToString("F1") + "x";
            multiplierText.gameObject.SetActive(true);
        }
    }

    private void CalculateCurrentMultiplier()
    {
        currentScoreMultiplier = 1.0f; // Default to 1x
        for (int i = comboThresholds.Length - 1; i >= 0; i--)
        {
            if (currentCombo >= comboThresholds[i])
            {
            currentScoreMultiplier = comboMultiplierTiers[i];
            break; // Found the highest tier, exit loop
            }
    }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    IEnumerator HideGraphicAfterDelay(Graphic graphicToHide, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (graphicToHide != null)
        {
            graphicToHide.gameObject.SetActive(false);
        }
    }

    private void UpdateComboText()
    {
        if (comboText != null)
        {
            comboText.text = "" + currentCombo;

            if (comboDisplayCoroutine != null)
            {
                StopCoroutine(comboDisplayCoroutine);
            }
            if (currentCombo > 0)
            {
                comboText.gameObject.SetActive(true);
                comboDisplayCoroutine = StartCoroutine(HideGraphicAfterDelay(comboText, comboDisplayDuration));
            }
            else
            {
                comboText.gameObject.SetActive(false);
            }
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        UpdateComboText();
        Debug.Log("Combo Reset");
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
        ResetCombo();
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
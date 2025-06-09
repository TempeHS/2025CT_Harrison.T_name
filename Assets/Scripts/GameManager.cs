using UnityEngine;
using UnityEngine.UI;   
using TMPro;            
using System.Collections.Generic; 

public class GameManager : MonoBehaviour {

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

    public static GameManager instance; 


    public GameObject pauseScreenUI; 
    private bool isPaused = false;   

    
    public float visualScrollSpeedMultiplier = 1.0f; 
    public Slider noteSpeedSlider;                   
    public TMP_Text speedValueText;                 



    void Start()
    {
        instance = this; 

        
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

    void Update() {
        if (!startPlaying) 
        {
            if(Input.anyKeyDown) 
            {
                startPlaying = true;
                theBS.hasStarted = true; 
                theMusic.Play();
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

    
    public void NoteHit()
    {
        Debug.Log("Hit On Time");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}
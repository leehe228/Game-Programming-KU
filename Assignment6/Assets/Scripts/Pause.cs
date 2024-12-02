using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Audio; // AudioMixer 사용

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;
    private bool isPaused = false;
    public AudioMixer gameAudioMixer; // AudioMixer 참조 추가

    void Awake()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(OnResumePressed);
    }

    void OnResumePressed()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // AudioMixer Snapshot 복구
            gameAudioMixer.SetFloat("SFX", 0f);
            gameAudioMixer.TransitionToSnapshots(
                new AudioMixerSnapshot[] { gameAudioMixer.FindSnapshot("Default") }, 
                new float[] { 1f }, 
                0f
            );
        }
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
            {
                isPaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                
                // AudioMixer Snapshot 전환
                gameAudioMixer.TransitionToSnapshots(
                    new AudioMixerSnapshot[] { gameAudioMixer.FindSnapshot("Paused") }, 
                    new float[] { 1f }, 
                    0f
                );

                Time.timeScale = 0;
            }
            else
            {
                OnResumePressed();
            }
            
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

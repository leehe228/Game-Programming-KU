using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}

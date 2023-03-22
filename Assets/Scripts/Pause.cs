using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject MenuPauseUI;

    public static bool isPaused = false;

    private KeyCode keyCode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("PauseKey"))
        {
            keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseKey"));

        }
        if (PlayerPrefs.HasKey("PauseKey")? Input.GetKeyDown(keyCode) : Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                EnPause();
            }
            else
            {
                Reprendre();
            }
        }
    }
    
    public void EnPause()
    {
        MenuPauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Reprendre()
    {
        MenuPauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Sauvegarder()
    {
        
    }

    public void Recommencer()
    {
        
        SceneManager.LoadScene("PartieRubiksCube");
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quitter()
    {
        SceneManager.LoadScene("MenuPrincipal");
        Time.timeScale = 1f;
        isPaused = false;
    }
}

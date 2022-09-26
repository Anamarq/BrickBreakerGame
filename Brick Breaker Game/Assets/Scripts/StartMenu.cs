using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Text highscoreText;
    private void Start()
    {
        if(PlayerPrefs.GetString("HIGHSCORENAME") != "")
            highscoreText.text = "High Score set by " + PlayerPrefs.GetString("HIGHSCORENAME") + ": " + PlayerPrefs.GetInt("HIGHSCORE")+ " points."; 
    }
    public void QuitGame()
    {
        Application.Quit(); //Cierra la aplicación
        Debug.Log("Quit Button pushed");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}

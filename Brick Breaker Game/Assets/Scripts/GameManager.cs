using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  ///
using UnityEngine.SceneManagement;//


public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public InputField highScoreInput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject LoadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevel;
    private GameObject [] balls;
    private GameObject [] irrompibles;


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length; //Da un array con los game objects q tienen Brick d etiqueta, pero 
        //con Length da la longitud.
        irrompibles = GameObject.FindGameObjectsWithTag("IrrompibleBrick"); //Array con bloques irrompibles
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLives(int changeInLives)  //Actualiza vidas
    {
        lives += changeInLives;

        //Check for no lives and end of game
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumberOfBricks()  //Actualiza numero de bloques. resta 1.
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            if (currentLevel >= levels.Length-1) //Si hemos completado el juego
            {
                GameOver();
            }
            else
            {
                for (int i = 0; i < irrompibles.Length; ++i)
                {
                    Destroy(irrompibles[i].gameObject); //Se eliminan los bloques irrompibles
                }
                LoadLevelPanel.SetActive(true); //Activa el mensaje de level compete
                LoadLevelPanel.GetComponentInChildren<Text>().text = "Level complete!" +'\n' + '\n' +"Loading Level " + (currentLevel + 2);
                gameOver = true; //Solo queremos que se congele, no que sea fin del juego
                Invoke("LoadLevel",3f);  //Invoke llama a la función tras pasar 3 segundos en este caso
            }
        }
    }

    void LoadLevel()
    {
        currentLevel++;
        Instantiate(levels[currentLevel], Vector2.zero, Quaternion.identity); //Pone el siguiente nivel en la pos 0 y rotación 0
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;  //Hay q volver a contar los bloques
        gameOver = false;
        LoadLevelPanel.SetActive(false); 
        balls = GameObject.FindGameObjectsWithTag("Ball"); //Hay que eliminar las bolas sobrantes, se buscan primero
        for (int i = 1; i < balls.Length; ++i)  
        {
            Destroy(balls[i].gameObject); //Se eliminan
        }
        balls[0].GetComponent<BallScript>().inPlay = false; //Solo una se queda, y pone inplay a false para que se ponga en su pos inicial.
     
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);  //Hace aparecer el canvas game over
        int highScore = PlayerPrefs.GetInt("HIGHSCORE"); //HIGHSCORE es la key, si no existe es 0.
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);//Sobreescribe el higscore antiguo por el nuevo
            highScoreText.text = "New High Score!! " + score + '\n'+ "Enter your name below.";
            highScoreInput.gameObject.SetActive(true); //Activa el input field para poner el nombre
        }
        else
        {
            highScoreText.text =PlayerPrefs.GetString("HIGHSCORENAME")+ " High Score was " + highScore+ '\n'+ "Can you beat it?";
        }

    }
    public void NewHighScore()
    {
       string highScoreName= highScoreInput.text; //Texto que ha escrito el usuario, el nombre en este caso
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName); //Guarda el nombre en HIGHSCORENAME
        highScoreInput.gameObject.SetActive(false);  //Desactiva el bloque de texto
        highScoreText.text = "Congratulations " + highScoreName + '\n' + "Your new high score is " + score;
    }
    
public void PlayAgain()
    {
        SceneManager.LoadScene("Main");  //Recarga la escena
    }
    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

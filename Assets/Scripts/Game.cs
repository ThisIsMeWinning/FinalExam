using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Game : MonoBehaviour
{

    public Text playerText;
    public Text timerText;
    public Text livesText;
    public Text scoreText;

    public static int score = 0;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameCanvas;

    public Toggle toggle;
    public AudioSource myAudio;

    public static string username;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            toggle.isOn = true;
            myAudio.enabled = true;
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                myAudio.enabled = false;
                toggle.isOn = false;
            }
            else
            {
                myAudio.enabled = true;
                toggle.isOn = true;
            }
        }
    }
    public void ToggleMusic()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
            myAudio.enabled = true;
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            myAudio.enabled = false;
        }
        PlayerPrefs.Save();
    }

    void Start()
    {
        Resume();
    }

    void Update()
    {
        playerText.text = "Currently Playing: " + PlayerPrefs.GetString("Username");
        scoreText.text = score.ToString();
        livesText.text = Intro.lives.ToString();
        timerText.text = Intro.time.ToString();
        Intro.time -= Time.deltaTime;
        if (Intro.time < 0)
        {
            timerText.text = "0";
            GameOver();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameCanvas.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        gameCanvas.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Continue()
    {
        Resume();
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Intro.lives = 0;
            score = 0;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            Intro.lives = save.lives;
            score = save.score;
            Intro.time = save.time;

            Debug.Log("Game Loaded");
            Resume();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Intro.lives = 0;
        score = 0;

        Debug.Log("Game Saved");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.lives = Intro.lives;
        save.score = score;
        save.time = Intro.time;
        return save;
    }
    public void NewGame()
    {
        score = 0;
        Intro.SetTime(30);
        SceneManager.LoadScene("1Intro");
    }
    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    public void IncreasePoints()
    {
        score++;
    }
    public void DecreasePoints()
    {
        score--;
    }

    public void IncreaseLives()
    {
        Intro.lives++;
    }
    public void DecreaseLives()
    {
        Intro.lives--;
    }

    public void GameOver()
    {
        if (score > PlayerPrefs.GetInt("highscoreTable"))
        {
            username = PlayerPrefs.GetString("Username");
            HighScores.AddHighscoreEntry(score, username);
        }
        SceneManager.LoadScene("3Exit");

    }

    public void Stop()
    { 
        GameOver();
    }
}


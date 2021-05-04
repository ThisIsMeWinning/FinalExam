using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public void NewGame()
    {
        Game.score = 0;
        Intro.SetTime(30);
        SceneManager.LoadScene("1Intro");
    }

    public void DeleteScores()
    {
        PlayerPrefs.DeleteKey("highscoreTable");
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

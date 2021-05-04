using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public InputField inputField;
    public Slider slider;

    public static float time = 30;
    public static int lives;


    public void Play()
    {
        SceneManager.LoadScene("2Game");
    }

    public void SetUsername(string name)
    {
        PlayerPrefs.SetString("Username", name);
    }

    public static void SetTime(float newTime)
    {
        time = newTime;
    }

    public void SetLives(int val)
    {
        if(val == 0)
        {
            lives = 0;
        }
        if (val == 1)
        {
            lives = 1;
        }
        if (val == 2)
        {
            lives = 2;
        }
        if (val == 3)
        {
            lives = 3;
        }
        if (val == 4)
        {
            lives = 4;
        }
        if (val == 5)
        {
            lives = 5;
        }
        if (val == 6)
        {
            lives = 6;
        }
        if (val == 7)
        {
            lives = 7;
        }
        if (val == 8)
        {
            lives = 8;
        }
        if (val == 9)
        {
            lives = 9;
        }
    }

}

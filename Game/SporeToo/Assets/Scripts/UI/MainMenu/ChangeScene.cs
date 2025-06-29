using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void LoadIntroduction()
    {
        SceneManager.LoadScene("Introduction");
    }
    public void Playgame()
    {
        SceneManager.LoadScene("LifeBox");
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

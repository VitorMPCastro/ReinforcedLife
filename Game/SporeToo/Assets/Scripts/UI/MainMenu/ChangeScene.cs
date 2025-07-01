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
        SceneManager.LoadScene("AI Test Scene");
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

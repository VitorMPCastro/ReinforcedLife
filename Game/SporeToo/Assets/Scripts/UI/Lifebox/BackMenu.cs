using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Lifebox
{
    public class BackMenu : MonoBehaviour
    {

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
using UnityEditor.Analytics;
using UnityEngine;

namespace UI.Lifebox

{
    public class PauseTime : MonoBehaviour
    {

        public GameObject PauseTimeTick;
        
        public void PauseGame()
        {
            PauseTimeTick.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            PauseTimeTick.SetActive(true);
            Time.timeScale = 1f;
        }

    }
}
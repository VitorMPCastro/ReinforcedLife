using UnityEngine;

namespace UI.Lifebox
{
    public class TimeControl : MonoBehaviour
    {
        public void Velocidade2x()
        {
            Time.timeScale = 2f;
        }

        public void Velocidade1x()
        {
            Time.timeScale = 1f;
        }

        public void VelocidadeLenta()
        {
            Time.timeScale = 0.5f;
        }
        
    }
}
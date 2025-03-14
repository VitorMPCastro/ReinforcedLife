using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField]
    public float timeScale = 1f; // Velocidade da simulação

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log("Tick Tock " + Time.time);
        Time.timeScale = timeScale;
    }

    public void SetSimulationSpeed(float speed)
    {
        Debug.Log(speed);
        timeScale = Mathf.Clamp(speed, 0.1f, 10f); // Limitando a velocidade
    }
}
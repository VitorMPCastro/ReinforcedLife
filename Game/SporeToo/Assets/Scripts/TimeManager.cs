using System;
using UnityEngine;

public class TimeTickManager : MonoBehaviour
{
    public static event Action OnTick;
    [SerializeField] private float _tickInterval = 1f; // Default to 1 second per tick
    private float _tickTimer;

    private void Update()
    {
        _tickTimer += Time.deltaTime;
        if (_tickTimer >= _tickInterval)
        {
            _tickTimer -= _tickInterval;
            OnTick?.Invoke();
        }
    }

    public void SetTickInterval(float interval)
    {
        _tickInterval = interval;
    }
}
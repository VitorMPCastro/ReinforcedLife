using System;
using UnityEngine;

public class TimeTickManager : MonoBehaviour
{
    public static event Action OnTick;
    [SerializeField] private float tickInterval = 1f;
    private float _tickTimer;

    private void Update()
    {
        _tickTimer += Time.deltaTime;
        if (!(_tickTimer >= tickInterval)) return;
        _tickTimer -= tickInterval;
        OnTick?.Invoke();
    }

    public void SetTickInterval(float interval)
    {
        tickInterval = interval;
    }
}
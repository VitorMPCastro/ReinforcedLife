using UnityEngine;

public class Food : MonoBehaviour
{
    private int _remainingTicks;

    public void Initialize(int lifetimeTicks)
    {
        _remainingTicks = lifetimeTicks;
        TimeTickManager.OnTick += OnTick;
    }

    private void OnTick()
    {
        _remainingTicks--;
        if (_remainingTicks <= 0)
        {
            TimeTickManager.OnTick -= OnTick;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        TimeTickManager.OnTick -= OnTick;
    }
}
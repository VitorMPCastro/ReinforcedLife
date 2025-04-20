namespace FoodNamespace {

    using UnityEngine;

    public class Food : MonoBehaviour
    {
        
        public FoodTypeData foodData;
        private int _remainingTicks;

        private void Start()
        {
            _remainingTicks = foodData.lifetimeTicks;
        }

        private void OnTick()
        {
            _remainingTicks--;
            if (_remainingTicks > 0) return;
            TimeTickManager.OnTick -= OnTick;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            TimeTickManager.OnTick -= OnTick;
        }

        private void OnEnable()
        {
            TimeTickManager.OnTick += OnTick;
        }

        private void OnDisable()
        {
            TimeTickManager.OnTick -= OnTick;
        }
    }
}
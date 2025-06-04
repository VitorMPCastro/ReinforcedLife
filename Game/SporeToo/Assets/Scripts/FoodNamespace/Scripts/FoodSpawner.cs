using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FoodNamespace
{
    public class FoodSpawner : MonoBehaviour
    {
        [System.Serializable]
        public class FoodType
        {
            public GameObject prefab;
            public float spawnRatio;

        }

        [SerializeField] private List<FoodType> foodTypes;
        [SerializeField] private int maxFoodCount = 20;
        [SerializeField] private Vector3 spawnAreaSize = new(100f, 0.3f, 100f);

        private readonly List<GameObject> _activeFood = new List<GameObject>();

        private void OnEnable()
        {
            TimeTickManager.OnTick += OnTick;
        }

        private void OnDisable()
        {
            TimeTickManager.OnTick -= OnTick;
        }

        private void OnTick()
        {
            _activeFood.RemoveAll(food => food.IsUnityNull());

            while (_activeFood.Count < maxFoodCount)
            {
                SpawnFood();
            }
        }

        private void SpawnFood()
        {
            var foodPrefab = ChooseFoodType();
            if (foodPrefab.IsUnityNull()) return;

            var spawnPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0,
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            var newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            newFood.layer = LayerMask.NameToLayer("Food");
            _activeFood.Add(newFood);
        }

        private GameObject ChooseFoodType()
        {
            
            var totalWeight = foodTypes.Sum(type => type.spawnRatio);

            var randomValue = Random.Range(0, totalWeight);
            var cumulativeWeight = 0f;

            foreach (var type in foodTypes)
            {
                cumulativeWeight += type.spawnRatio;
                if (randomValue <= cumulativeWeight)
                    return type.prefab;
            }

            return null;
        }
    }
}

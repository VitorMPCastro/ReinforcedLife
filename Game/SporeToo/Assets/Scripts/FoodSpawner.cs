using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    [SerializeField] private int foodLifetimeTicks = 50;

    private List<GameObject> _activeFood = new List<GameObject>();

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
        // Remove null references (destroyed food)
        _activeFood.RemoveAll(food => food.IsUnityNull());

        // Spawn new food if needed
        while (_activeFood.Count < maxFoodCount)
        {
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        GameObject foodPrefab = ChooseFoodType();
        if (foodPrefab.IsUnityNull()) return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        newFood.GetComponent<Food>().Initialize(foodLifetimeTicks);
        _activeFood.Add(newFood);
    }

    private GameObject ChooseFoodType()
    {
        float totalWeight = 0f;
        foreach (var type in foodTypes)
            totalWeight += type.spawnRatio;

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var type in foodTypes)
        {
            cumulativeWeight += type.spawnRatio;
            if (randomValue <= cumulativeWeight)
                return type.prefab;
        }

        return null;
    }
}

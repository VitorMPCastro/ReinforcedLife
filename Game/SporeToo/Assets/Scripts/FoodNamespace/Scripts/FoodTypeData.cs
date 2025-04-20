using UnityEngine;

namespace FoodNamespace
{
    [CreateAssetMenu(fileName = "FoodTypeData", menuName = "Simulation/FoodTypeData")]
    public class FoodTypeData : ScriptableObject
    {
        public FoodType type;
        public int lifetimeTicks;
    }
}
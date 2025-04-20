using UnityEngine;

namespace AgentNamespace
{
    public class AgentConsumer : MonoBehaviour
    {
        [SerializeField] private float eatRange = 1f;
        [SerializeField] private LayerMask foodLayer;
        [SerializeField] private float energyGain = 10f;

        private void Update()
        {
            DetectAndConsumeFood();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, eatRange);
        }

        private void DetectAndConsumeFood()
        {
            Debug.Log("Detecting food...");
            Collider[] hits = Physics.OverlapSphere(transform.position, eatRange, foodLayer);

            foreach (var hit in hits)
            {
                Debug.Log("Something detected: " + hit.name);
                var food = hit.GetComponentInParent<FoodNamespace.Food>();
                Debug.Log(food);
                if (food)
                {
                    Debug.Log("Food found, destroying.");
                    Destroy(food.gameObject);
                    break;
                }
            }
        }
    }
}
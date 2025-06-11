using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using FoodNamespace;
using UnityEngine.UIElements;

namespace AgentNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAgent : Agent
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float moveHeight = 5f;
        [SerializeField] private GameObject foodPrefab;
        [SerializeField] private float foodScale = 1f;
        [SerializeField] private Transform area;

        private Rigidbody _rb;
        private Vector3 _movement;
        private Food currentFood;

        public override void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        public override void OnEpisodeBegin()
        {
            // Reset position do agente
            transform.localPosition = new Vector3(Random.Range(-4f, 4f), moveHeight, Random.Range(-4f, 4f));
            _rb.velocity = Vector3.zero;

            // Destroi comida anterior, se existir
            if (currentFood != null)
                Destroy(currentFood.gameObject);

            // Cria nova comida em posi��o aleat�ria
            Vector3 foodPos = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
            GameObject foodObj = Instantiate(foodPrefab, foodPos, Quaternion.identity, area);
            foodObj.transform.localScale = new Vector3(foodScale, foodScale, foodScale);
            currentFood = foodObj.GetComponent<Food>();
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(transform.localPosition); // posi��o do agente
            if (currentFood != null)
                sensor.AddObservation(currentFood.transform.localPosition); // posi��o da comida
            else
                sensor.AddObservation(Vector3.zero); // caso comida n�o exista
            sensor.AddObservation(_rb.velocity); // velocidade
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            float moveX = actions.ContinuousActions[0];
            float moveZ = actions.ContinuousActions[1];

            _movement = new Vector3(moveX, 0f, moveZ).normalized;

            Vector3 targetPosition = _rb.position + _movement * (moveSpeed * Time.fixedDeltaTime);
            targetPosition.y = moveHeight;
            _rb.MovePosition(targetPosition);

            if (_movement != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(_movement);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }

            // Penalidade pequena por passo para incentivar rapidez
            AddReward(-0.001f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Food"))
            {
                AddReward(1.0f);
                Destroy(other.gameObject);
                EndEpisode();
            }
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var contActions = actionsOut.ContinuousActions;
            contActions[0] = Input.GetAxis("Horizontal");
            contActions[1] = Input.GetAxis("Vertical");
        }
    }
}

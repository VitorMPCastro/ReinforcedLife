using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Linq; // Necessário para .OrderBy

namespace AgentNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAgent : Agent
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        [Header("Survival")]
        [SerializeField] private float maxEnergy = 100f;
        [SerializeField] private float energyDecayPerStep = 0.1f;
        [SerializeField] private float energyGainOnEat = 50f;

        [Header("Observation")]
        [Tooltip("What layer the food is on.")]
        [SerializeField] private LayerMask foodLayer;
        [Tooltip("How far the agent can 'see' food.")]
        [SerializeField] private float foodSensorRadius = 25f;

        private Rigidbody _rb;
        private float _currentEnergy;
        private FoodNamespace.Food _targetFood;

        void Awake()
        {
            Debug.Log("<color=cyan>AWAKE: Agente acordou e está na cena.</color>", this.gameObject);
        }

        public override void Initialize()
        {
            Debug.Log("<color=cyan>INITIALIZE: Agente está sendo inicializado pelo ML-Agents.</color>", this.gameObject);
            base.Initialize(); // Chamar base.Initialize() é uma boa prática
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        public override void OnEpisodeBegin()
        {
            Debug.Log("<color=green>ONEPISODEBEGIN: Começando um novo episódio.</color>", this.gameObject);
            // Resetar energia e posição do agente
            _currentEnergy = maxEnergy;
            transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            // O FoodSpawner será responsável por garantir que haja comida.
            // O agente não cria mais sua própria comida.
            _targetFood = null;

            Debug.Log("--- FIM de OnEpisodeBegin ---");
        }

        /// <summary>
        /// Encontra a comida mais próxima dentro do raio de visão.
        /// </summary>
        private void FindClosestFood()
        {
            Collider[] foodColliders = Physics.OverlapSphere(transform.position, foodSensorRadius, foodLayer);

            if (foodColliders.Length == 0)
            {
                _targetFood = null;
                return;
            }

            // Usando LINQ para encontrar o mais próximo.
            _targetFood = foodColliders
                .OrderBy(col => Vector3.Distance(transform.position, col.transform.position))
                .FirstOrDefault()? // Pega o primeiro (mais próximo) ou null se a lista estiver vazia
                .GetComponent<FoodNamespace.Food>();
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            Debug.Log("Coletando Observações...");
            FindClosestFood();

            // Posição relativa do agente (útil se o chão não for plano)
            // sensor.AddObservation(transform.localPosition);

            // Observa a direção e a distância para a comida mais próxima
            if (_targetFood != null)
            {
                Vector3 directionToFood = (_targetFood.transform.position - transform.position).normalized;
                sensor.AddObservation(directionToFood); // 3 observações (vetor normalizado)
                sensor.AddObservation(Vector3.Distance(transform.position, _targetFood.transform.position) / foodSensorRadius); // 1 observação (distância normalizada)
            }
            else // Se não vê comida
            {
                sensor.AddObservation(Vector3.zero); // 3 observações
                sensor.AddObservation(0f);           // 1 observação
            }

            sensor.AddObservation(_rb.velocity.normalized); // 3 observações
            sensor.AddObservation(_currentEnergy / maxEnergy); // 1 observação (energia normalizada)
            // Total de observações: 3 + 1 + 3 + 1 = 8
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            Debug.Log("Recebendo Ação...");
            // Ações
            float moveX = actions.ContinuousActions[0];
            float moveZ = actions.ContinuousActions[1];
            Vector3 movement = new Vector3(moveX, 0f, moveZ);

            _rb.AddForce(movement * moveSpeed, ForceMode.VelocityChange);

            // Rotação
            if (movement != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(movement);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }

            // Penalidades e recompensas de sobrevivência
            AddReward(-energyDecayPerStep * 0.01f); // Pequena penalidade para existir, incentiva a terminar logo.
            _currentEnergy -= energyDecayPerStep;

            if (_currentEnergy <= 0)
            {
                SetReward(-1.0f); // Grande penalidade por morrer
                EndEpisode();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<FoodNamespace.Food>(out var food))
            {
                AddReward(1.0f); // Recompensa por comer
                _currentEnergy = Mathf.Min(_currentEnergy + energyGainOnEat, maxEnergy); // Ganha energia, não passando do máximo

                // Em vez de destruir aqui, o ideal é que a comida se desative e notifique o spawner.
                // Mas para simplificar, destruir funciona.
                Destroy(other.gameObject);
                _targetFood = null; // Perde o alvo atual.
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
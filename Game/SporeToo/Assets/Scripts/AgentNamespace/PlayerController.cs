using UnityEngine;

namespace AgentNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float moveHeight = 5f;

        private Rigidbody _rb;
        private Vector3 _movement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void Update()
        {
            var moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
            var moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
            _movement = new Vector3(moveX, 0f, moveZ).normalized;
        }

        private void FixedUpdate()
        {
            Vector3 targetPosition = _rb.position + _movement * (moveSpeed * Time.fixedDeltaTime);
            targetPosition.y = moveHeight;
            _rb.MovePosition(targetPosition);

            if (_movement == Vector3.zero) return;
            var targetRotation = Quaternion.LookRotation(_movement);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
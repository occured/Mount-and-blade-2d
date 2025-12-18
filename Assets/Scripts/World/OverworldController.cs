using UnityEngine;
using MountAndBlade2D.Core;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Handles overworld movement and emits interaction events when entering nodes.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class OverworldController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Camera targetCamera;
        [SerializeField] private EventBus eventBus;

        private Rigidbody2D _rb;
        private Vector2 _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _input.normalized * (moveSpeed * Time.fixedDeltaTime));
            if (targetCamera != null)
            {
                targetCamera.transform.position = new Vector3(_rb.position.x, _rb.position.y, targetCamera.transform.position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (eventBus == null)
            {
                return;
            }

            var prompt = new WorldInteractionPrompt
            {
                Collider = other,
                WorldPosition = other.transform.position
            };

            eventBus.GetSubject<WorldInteractionPrompt>().Publish(prompt);
        }
    }

    public struct WorldInteractionPrompt
    {
        public Collider2D Collider;
        public Vector2 WorldPosition;
    }
}

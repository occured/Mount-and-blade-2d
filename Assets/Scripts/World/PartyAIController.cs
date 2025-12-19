using UnityEngine;

namespace MountAndBlade2D.World
{
    public enum PartyAIState
    {
        Idle,
        Patrol,
        Chase,
        Flee
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class PartyAIController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private float fleeRadius = 3f;
        [SerializeField] private Transform patrolTarget;
        [SerializeField] private Transform target;

        private Rigidbody2D _rb;
        private PartyAIState _state = PartyAIState.Idle;

        public PartyAIState State => _state;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            EvaluateState();
            MoveByState();
        }

        private void EvaluateState()
        {
            if (target == null)
            {
                _state = patrolTarget != null ? PartyAIState.Patrol : PartyAIState.Idle;
                return;
            }

            var distance = Vector2.Distance(target.position, transform.position);
            if (distance <= fleeRadius)
            {
                _state = PartyAIState.Flee;
            }
            else if (distance <= detectionRadius)
            {
                _state = PartyAIState.Chase;
            }
            else
            {
                _state = patrolTarget != null ? PartyAIState.Patrol : PartyAIState.Idle;
            }
        }

        private void MoveByState()
        {
            Vector2 direction = Vector2.zero;
            switch (_state)
            {
                case PartyAIState.Patrol:
                    if (patrolTarget != null)
                    {
                        direction = (patrolTarget.position - transform.position).normalized;
                    }
                    break;
                case PartyAIState.Chase:
                    if (target != null)
                    {
                        direction = (target.position - transform.position).normalized;
                    }
                    break;
                case PartyAIState.Flee:
                    if (target != null)
                    {
                        direction = (transform.position - target.position).normalized;
                    }
                    break;
            }

            if (direction == Vector2.zero)
            {
                return;
            }

            _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}

using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Basic combat AI that triggers attacks at intervals and blocks when targeted.
    /// </summary>
    public class CombatAIController : MonoBehaviour
    {
        [SerializeField] private CombatStateMachine combatStateMachine;
        [SerializeField] private BlockController blockController;
        [SerializeField] private Transform target;
        [SerializeField] private float attackInterval = 1.2f;
        [SerializeField] private float attackDistance = 1.5f;
        [SerializeField] private float blockChance = 0.25f;

        private float _attackTimer;

        private void Awake()
        {
            if (blockController != null)
            {
                blockController.SetUseLegacyInput(false);
            }
        }

        private void Update()
        {
            if (combatStateMachine == null || target == null)
            {
                return;
            }

            _attackTimer -= Time.deltaTime;
            var distance = Vector2.Distance(transform.position, target.position);
            if (_attackTimer <= 0f && distance <= attackDistance)
            {
                var direction = GetDirectionToTarget();
                combatStateMachine.StartAttack(direction);
                _attackTimer = attackInterval;
            }

            if (blockController != null)
            {
                if (distance <= attackDistance && Random.value < blockChance * Time.deltaTime)
                {
                    blockController.SetBlocking(true);
                }
                else
                {
                    blockController.SetBlocking(false);
                }
            }
        }

        private AttackDirection GetDirectionToTarget()
        {
            var delta = target.position - transform.position;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                return delta.x > 0f ? AttackDirection.Right : AttackDirection.Left;
            }

            return delta.y > 0f ? AttackDirection.Up : AttackDirection.Down;
        }

    }
}

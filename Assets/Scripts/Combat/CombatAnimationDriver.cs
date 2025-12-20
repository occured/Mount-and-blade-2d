using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Drives animator parameters based on combat state and attack direction.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CombatAnimationDriver : MonoBehaviour
    {
        [SerializeField] private CombatStateMachine stateMachine;
        [SerializeField] private string stateParam = "CombatState";
        [SerializeField] private string directionXParam = "AttackDirX";
        [SerializeField] private string directionYParam = "AttackDirY";

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            if (stateMachine != null)
            {
                stateMachine.OnStateChanged += HandleStateChanged;
            }
        }

        private void OnDisable()
        {
            if (stateMachine != null)
            {
                stateMachine.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleStateChanged(CombatState state)
        {
            if (_animator == null)
            {
                return;
            }

            _animator.SetInteger(stateParam, (int)state);
            var dir = stateMachine.Direction;
            var vector = DirectionToVector(dir);
            _animator.SetFloat(directionXParam, vector.x);
            _animator.SetFloat(directionYParam, vector.y);
        }

        private static Vector2 DirectionToVector(AttackDirection direction)
        {
            return direction switch
            {
                AttackDirection.Up => Vector2.up,
                AttackDirection.Down => Vector2.down,
                AttackDirection.Left => Vector2.left,
                AttackDirection.Right => Vector2.right,
                _ => Vector2.zero
            };
        }
    }
}

using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Simple sprite orientation helper when no animations are available.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class CombatSpriteDirection : MonoBehaviour
    {
        [SerializeField] private CombatStateMachine stateMachine;
        [SerializeField] private Color activeColor = Color.white;
        [SerializeField] private Color windupColor = new(1f, 0.9f, 0.7f);
        [SerializeField] private Color recoverColor = new(0.8f, 0.8f, 0.8f);

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
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
            if (_renderer == null || stateMachine == null)
            {
                return;
            }

            var direction = stateMachine.Direction;
            if (direction == AttackDirection.Left)
            {
                _renderer.flipX = true;
            }
            else if (direction == AttackDirection.Right)
            {
                _renderer.flipX = false;
            }

            _renderer.color = state switch
            {
                CombatState.Windup => windupColor,
                CombatState.Active => activeColor,
                CombatState.Recover => recoverColor,
                _ => Color.white
            };
        }
    }
}

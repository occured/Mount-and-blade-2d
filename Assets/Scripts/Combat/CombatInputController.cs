using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Reads player input and triggers combat actions on a CombatStateMachine.
    /// </summary>
    public class CombatInputController : MonoBehaviour
    {
        [SerializeField] private CombatStateMachine combatStateMachine;
        [SerializeField] private bool useLegacyInput = true;
        [SerializeField] private float directionThreshold = 0.3f;

#if ENABLE_INPUT_SYSTEM
        private void OnAttack(InputValue value)
        {
            if (combatStateMachine == null)
            {
                return;
            }

            if (value.isPressed)
            {
                combatStateMachine.StartAttack(ReadDirection());
            }
        }

        private void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }
#endif

        private Vector2 _moveInput;

        private void Update()
        {
            if (!useLegacyInput || combatStateMachine == null)
            {
                return;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                combatStateMachine.StartAttack(ReadDirection());
            }
        }

        private AttackDirection ReadDirection()
        {
            if (_moveInput.magnitude < directionThreshold)
            {
                return AttackDirection.Neutral;
            }

            if (Mathf.Abs(_moveInput.x) > Mathf.Abs(_moveInput.y))
            {
                return _moveInput.x > 0f ? AttackDirection.Right : AttackDirection.Left;
            }

            return _moveInput.y > 0f ? AttackDirection.Up : AttackDirection.Down;
        }
    }
}

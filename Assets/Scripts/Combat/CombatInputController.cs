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

#if ENABLE_INPUT_SYSTEM
        private void OnAttack(InputValue value)
        {
            if (combatStateMachine == null)
            {
                return;
            }

            if (value.isPressed)
            {
                combatStateMachine.StartAttack();
            }
        }
#endif

        private void Update()
        {
            if (!useLegacyInput || combatStateMachine == null)
            {
                return;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                combatStateMachine.StartAttack();
            }
        }
    }
}

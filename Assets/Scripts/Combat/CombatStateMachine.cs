using UnityEngine;

namespace MountAndBlade2D.Combat
{
    public enum CombatState
    {
        Idle,
        Windup,
        Active,
        Recover,
        Cooldown
    }

    /// <summary>
    /// Simple per-fighter state machine to drive directional attacks and timing windows.
    /// </summary>
    public class CombatStateMachine : MonoBehaviour
    {
        [SerializeField] private float windupDuration = 0.2f;
        [SerializeField] private float activeDuration = 0.15f;
        [SerializeField] private float recoverDuration = 0.3f;
        [SerializeField] private float cooldownDuration = 0.25f;

        private CombatState _state = CombatState.Idle;
        private float _timer;

        public CombatState State => _state;

        private void Update()
        {
            if (_state == CombatState.Idle)
            {
                return;
            }

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                AdvanceState();
            }
        }

        public void StartAttack()
        {
            if (_state != CombatState.Idle)
            {
                return;
            }

            _state = CombatState.Windup;
            _timer = windupDuration;
        }

        private void AdvanceState()
        {
            switch (_state)
            {
                case CombatState.Windup:
                    _state = CombatState.Active;
                    _timer = activeDuration;
                    break;
                case CombatState.Active:
                    _state = CombatState.Recover;
                    _timer = recoverDuration;
                    break;
                case CombatState.Recover:
                    _state = CombatState.Cooldown;
                    _timer = cooldownDuration;
                    break;
                default:
                    _state = CombatState.Idle;
                    _timer = 0f;
                    break;
            }
        }
    }
}

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
        [SerializeField] private float staminaCost = 10f;
        [SerializeField] private Character.StaminaComponent stamina;

        private CombatState _state = CombatState.Idle;
        private float _timer;
        private AttackDirection _direction = AttackDirection.Neutral;

        public CombatState State => _state;
        public AttackDirection Direction => _direction;
        public event System.Action<CombatState> OnStateChanged;

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

        public void StartAttack(AttackDirection direction = AttackDirection.Neutral)
        {
            if (_state != CombatState.Idle)
            {
                return;
            }

            if (stamina != null && !stamina.TrySpend(staminaCost))
            {
                return;
            }

            _direction = direction;
            _state = CombatState.Windup;
            _timer = windupDuration;
            OnStateChanged?.Invoke(_state);
        }

        private void AdvanceState()
        {
            switch (_state)
            {
                case CombatState.Windup:
                    _state = CombatState.Active;
                    _timer = activeDuration;
                    OnStateChanged?.Invoke(_state);
                    break;
                case CombatState.Active:
                    _state = CombatState.Recover;
                    _timer = recoverDuration;
                    OnStateChanged?.Invoke(_state);
                    break;
                case CombatState.Recover:
                    _state = CombatState.Cooldown;
                    _timer = cooldownDuration;
                    OnStateChanged?.Invoke(_state);
                    break;
                default:
                    _state = CombatState.Idle;
                    _timer = 0f;
                    _direction = AttackDirection.Neutral;
                    OnStateChanged?.Invoke(_state);
                    break;
            }
        }
    }
}

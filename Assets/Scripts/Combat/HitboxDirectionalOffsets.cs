using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Applies local position offsets based on attack direction.
    /// </summary>
    public class HitboxDirectionalOffsets : MonoBehaviour
    {
        [SerializeField] private CombatStateMachine stateMachine;
        [SerializeField] private Vector3 neutralOffset;
        [SerializeField] private Vector3 upOffset;
        [SerializeField] private Vector3 downOffset;
        [SerializeField] private Vector3 leftOffset;
        [SerializeField] private Vector3 rightOffset;

        private Vector3 _defaultLocalPosition;

        private void Awake()
        {
            _defaultLocalPosition = transform.localPosition;
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
            if (state != CombatState.Active && state != CombatState.Windup)
            {
                transform.localPosition = _defaultLocalPosition;
                return;
            }

            transform.localPosition = _defaultLocalPosition + GetOffset(stateMachine.Direction);
        }

        private Vector3 GetOffset(AttackDirection direction)
        {
            return direction switch
            {
                AttackDirection.Up => upOffset,
                AttackDirection.Down => downOffset,
                AttackDirection.Left => leftOffset,
                AttackDirection.Right => rightOffset,
                _ => neutralOffset
            };
        }
    }
}

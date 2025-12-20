using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Attach to weapon hitboxes; forwards trigger hits to a HitResolver.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private HitResolver resolver;
        [SerializeField] private CombatStateMachine stateMachine;
        [SerializeField] private bool alwaysActive;

        private Collider2D _collider;

        private void Reset()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            if (!alwaysActive && stateMachine != null)
            {
                _collider.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (!alwaysActive && stateMachine != null)
            {
                stateMachine.OnStateChanged += HandleStateChanged;
            }
        }

        private void OnDisable()
        {
            if (!alwaysActive && stateMachine != null)
            {
                stateMachine.OnStateChanged -= HandleStateChanged;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (resolver == null)
            {
                return;
            }

            resolver.Resolve(other);
        }

        private void HandleStateChanged(CombatState state)
        {
            if (_collider == null)
            {
                return;
            }

            _collider.enabled = state == CombatState.Active;
        }
    }
}

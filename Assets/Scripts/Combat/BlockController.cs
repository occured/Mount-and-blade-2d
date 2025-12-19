using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Toggles a defensive state to reduce incoming damage.
    /// </summary>
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private float blockReduction = 0.5f;
        [SerializeField] private bool useLegacyInput = true;
        [SerializeField] private float staminaCostPerSecond = 8f;
        [SerializeField] private Character.StaminaComponent stamina;

        private bool _isBlocking;

        public bool IsBlocking => _isBlocking;
        public float BlockReduction => blockReduction;

#if ENABLE_INPUT_SYSTEM
        private void OnBlock(InputValue value)
        {
            _isBlocking = value.isPressed;
        }
#endif

        private void Update()
        {
            if (!useLegacyInput)
            {
                return;
            }

            _isBlocking = Input.GetButton("Fire2");
            DrainStamina();
        }

        private void LateUpdate()
        {
            if (!useLegacyInput)
            {
                DrainStamina();
            }
        }

        public void SetBlocking(bool blocking)
        {
            _isBlocking = blocking;
        }

        public void SetUseLegacyInput(bool enabled)
        {
            useLegacyInput = enabled;
        }

        private void DrainStamina()
        {
            if (!_isBlocking || stamina == null)
            {
                return;
            }

            stamina.TrySpend(staminaCostPerSecond * Time.deltaTime);
        }
    }
}

using UnityEngine;

namespace MountAndBlade2D.Character
{
    /// <summary>
    /// Handles stamina spending and regeneration for actions like attacking, blocking, and sprinting.
    /// </summary>
    public class StaminaComponent : MonoBehaviour
    {
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float regenPerSecond = 15f;
        [SerializeField] private float exhaustedDelay = 1.25f;

        private float _current;
        private float _exhaustedTimer;

        public float Current => _current;
        public float Max => maxStamina;
        public bool IsExhausted => _exhaustedTimer > 0f;

        private void Awake()
        {
            _current = maxStamina;
        }

        private void Update()
        {
            if (_exhaustedTimer > 0f)
            {
                _exhaustedTimer -= Time.deltaTime;
                return;
            }

            _current = Mathf.Min(maxStamina, _current + regenPerSecond * Time.deltaTime);
        }

        public bool TrySpend(float amount)
        {
            if (amount <= 0f)
            {
                return true;
            }

            if (_current < amount)
            {
                Exhaust();
                return false;
            }

            _current -= amount;
            return true;
        }

        public void Exhaust()
        {
            _current = Mathf.Clamp(_current, 0f, maxStamina);
            _exhaustedTimer = exhaustedDelay;
        }

        public void RestoreAll()
        {
            _current = maxStamina;
            _exhaustedTimer = 0f;
        }
    }
}

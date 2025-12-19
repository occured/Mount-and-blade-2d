using UnityEngine;

namespace MountAndBlade2D.Character
{
    public class HealthComponent : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private CharacterStats stats;
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;
        public CharacterStats Stats => stats;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            var finalAmount = stats != null ? stats.GetMitigatedDamage(amount) : amount;
            _currentHealth = Mathf.Max(0f, _currentHealth - finalAmount);
            if (_currentHealth <= 0f)
            {
                OnDeath();
            }
        }

        protected virtual void OnDeath()
        {
            // Placeholder hook for death handling; override or subscribe externally.
            gameObject.SetActive(false);
        }
    }
}

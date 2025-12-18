using UnityEngine;

namespace MountAndBlade2D.Character
{
    public class HealthComponent : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth = Mathf.Max(0f, _currentHealth - amount);
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

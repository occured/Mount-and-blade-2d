using UnityEngine;

namespace MountAndBlade2D.Character
{
    /// <summary>
    /// Grants experience to a target when the attached HealthComponent dies.
    /// </summary>
    [RequireComponent(typeof(HealthComponent))]
    public class ExperienceReward : MonoBehaviour
    {
        [SerializeField] private int xpReward = 50;
        [SerializeField] private ExperienceComponent recipient;

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.Died += HandleDeath;
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.Died -= HandleDeath;
            }
        }

        private void HandleDeath(HealthComponent health)
        {
            if (recipient == null)
            {
                return;
            }

            recipient.AddXp(xpReward);
        }
    }
}

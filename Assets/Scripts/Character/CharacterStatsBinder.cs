using UnityEngine;

namespace MountAndBlade2D.Character
{
    /// <summary>
    /// Applies CharacterStats to runtime components (health, stamina).
    /// </summary>
    public class CharacterStatsBinder : MonoBehaviour
    {
        [SerializeField] private HealthComponent health;
        [SerializeField] private StaminaComponent stamina;

        public void Apply(CharacterStats stats)
        {
            if (stats == null)
            {
                return;
            }

            if (health != null)
            {
                health.ApplyStats(stats);
            }

            if (stamina != null)
            {
                stamina.ApplyStats(stats);
            }
        }
    }
}

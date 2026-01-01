using UnityEngine;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Computes a movement speed multiplier based on morale and food availability.
    /// </summary>
    public class PartyTravelSpeed : MonoBehaviour
    {
        [SerializeField] private MoraleSystem moraleSystem;
        [SerializeField] private FoodInventory foodInventory;
        [SerializeField] private float maxBonus = 0.15f;
        [SerializeField] private float minPenalty = 0.3f;

        public float GetSpeedMultiplier()
        {
            var multiplier = 1f;
            if (moraleSystem != null)
            {
                var moraleNormalized = moraleSystem.Morale / 100f;
                multiplier += Mathf.Lerp(-minPenalty, maxBonus, moraleNormalized);
            }

            if (foodInventory != null && foodInventory.Food <= 0)
            {
                multiplier *= 0.8f;
            }

            return Mathf.Clamp(multiplier, 0.5f, 1.5f);
        }
    }
}

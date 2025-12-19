using UnityEngine;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks morale from 0-100 and applies modifiers from wages, battles, and starvation.
    /// </summary>
    public class MoraleSystem : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float morale = 60f;
        [SerializeField] private float victoryBonus = 8f;
        [SerializeField] private float defeatPenalty = 12f;
        [SerializeField] private float paidWagesBonus = 5f;
        [SerializeField] private float missedWagesPenalty = 10f;
        [SerializeField] private float starvationPenaltyPerDay = 7f;

        public float Morale => morale;

        public void ApplyVictory()
        {
            morale = Mathf.Clamp(morale + victoryBonus, 0f, 100f);
        }

        public void ApplyDefeat()
        {
            morale = Mathf.Clamp(morale - defeatPenalty, 0f, 100f);
        }

        public void OnWagesPaid()
        {
            morale = Mathf.Clamp(morale + paidWagesBonus, 0f, 100f);
        }

        public void OnWagesMissed()
        {
            morale = Mathf.Clamp(morale - missedWagesPenalty, 0f, 100f);
        }

        public void OnStarvationTick()
        {
            morale = Mathf.Clamp(morale - starvationPenaltyPerDay, 0f, 100f);
        }

        public void SetMorale(float value)
        {
            morale = Mathf.Clamp(value, 0f, 100f);
        }
    }
}

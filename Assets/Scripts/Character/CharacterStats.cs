using UnityEngine;

namespace MountAndBlade2D.Character
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "MountAndBlade2D/CharacterStats")]
    public class CharacterStats : ScriptableObject
    {
        [Header("Vitals")]
        public float maxHealth = 100f;
        public float maxStamina = 100f;

        [Header("Offense")]
        public float baseDamage = 10f;
        public float attackSpeed = 1f;

        [Header("Defense")]
        public float armor = 0f;
        public float poise = 10f;

        [Header("Economy")]
        public int wage = 5;

        public float GetMitigatedDamage(float incomingDamage)
        {
            var mitigation = 1f - Mathf.Clamp01(armor / (armor + 100f));
            return incomingDamage * mitigation;
        }
    }
}

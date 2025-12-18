using UnityEngine;

namespace MountAndBlade2D.Character
{
    public enum DamageType
    {
        Slash,
        Pierce,
        Blunt
    }

    [CreateAssetMenu(fileName = "DamageProfile", menuName = "MountAndBlade2D/DamageProfile")]
    public class DamageProfile : ScriptableObject
    {
        [Range(0f, 1f)] public float slashMultiplier = 1f;
        [Range(0f, 1f)] public float pierceMultiplier = 1f;
        [Range(0f, 1f)] public float bluntMultiplier = 1f;

        public float Evaluate(DamageType type, float baseDamage)
        {
            return type switch
            {
                DamageType.Slash => baseDamage * slashMultiplier,
                DamageType.Pierce => baseDamage * pierceMultiplier,
                DamageType.Blunt => baseDamage * bluntMultiplier,
                _ => baseDamage
            };
        }
    }
}

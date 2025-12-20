using UnityEngine;

namespace MountAndBlade2D.Combat
{
    [CreateAssetMenu(fileName = "CombatData", menuName = "MountAndBlade2D/CombatData")]
    public class CombatData : ScriptableObject
    {
        public float windupDuration = 0.2f;
        public float activeDuration = 0.15f;
        public float recoverDuration = 0.3f;
        public float cooldownDuration = 0.25f;
        public float staminaCost = 10f;
    }
}

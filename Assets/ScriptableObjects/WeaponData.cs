using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "MountAndBlade2D/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public string weaponName = "Sword";
        public float damage = 15f;
        public float windup = 0.2f;
        public float active = 0.15f;
        public float recover = 0.3f;
        public float staminaCost = 10f;
        public float knockback = 2f;
    }
}

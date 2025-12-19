using UnityEngine;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "MountAndBlade2D/TroopData")]
    public class TroopData : ScriptableObject
    {
        public string troopName = "Militia";
        public CharacterStats baseStats;
        public WeaponData primaryWeapon;
        public int recruitCost = 25;
        public int upgradeCost = 50;
        public TroopData upgradeTarget;
    }
}

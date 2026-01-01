using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TroopRegistry", menuName = "MountAndBlade2D/TroopRegistry")]
    public class TroopRegistry : ScriptableObject
    {
        public List<TroopData> troops = new();

        public TroopData FindByStats(Character.CharacterStats stats)
        {
            if (stats == null)
            {
                return null;
            }

            foreach (var troop in troops)
            {
                if (troop != null && troop.baseStats == stats)
                {
                    return troop;
                }
            }

            return null;
        }
    }
}

using UnityEngine;
using MountAndBlade2D.ScriptableObjects;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Handles troop upgrades using TroopData and party currency.
    /// </summary>
    public class TroopUpgradeSystem : MonoBehaviour
    {
        [SerializeField] private PartyRoster roster;
        [SerializeField] private CurrencyWallet wallet;
        [SerializeField] private TroopRegistry troopRegistry;

        public bool CanUpgrade(CharacterStats stats, out TroopData current, out TroopData next)
        {
            current = troopRegistry != null ? troopRegistry.FindByStats(stats) : null;
            next = current != null ? current.upgradeTarget : null;
            return current != null && next != null && next.baseStats != null;
        }

        public bool TryUpgrade(CharacterStats stats)
        {
            if (roster == null)
            {
                return false;
            }

            if (!CanUpgrade(stats, out var current, out var next))
            {
                return false;
            }

            if (wallet != null && !wallet.TrySpend(current.upgradeCost))
            {
                return false;
            }

            roster.Replace(stats, next.baseStats);
            return true;
        }
    }
}

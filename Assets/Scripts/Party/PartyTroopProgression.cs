using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.Character;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks troop XP and upgrades units when thresholds are met.
    /// </summary>
    public class PartyTroopProgression : MonoBehaviour
    {
        [SerializeField] private PartyRoster roster;
        [SerializeField] private TroopRegistry troopRegistry;
        [SerializeField] private TroopUpgradeSystem upgradeSystem;
        [SerializeField] private int xpPerBattle = 25;

        [SerializeField] private List<TroopXpEntry> troopXp = new();

        public void AwardBattleXp()
        {
            if (roster == null)
            {
                return;
            }

            foreach (var member in roster.Members)
            {
                AddXp(member, xpPerBattle);
            }
        }

        public void AddXpToRoster(int amount)
        {
            if (roster == null || amount <= 0)
            {
                return;
            }

            foreach (var member in roster.Members)
            {
                AddXp(member, amount);
            }
        }

        public void AddXp(CharacterStats stats, int amount)
        {
            if (stats == null || amount <= 0)
            {
                return;
            }

            var entryIndex = troopXp.FindIndex(entry => entry.stats == stats);
            if (entryIndex < 0)
            {
                troopXp.Add(new TroopXpEntry(stats, amount));
            }
            else
            {
                var entry = troopXp[entryIndex];
                entry.xp += amount;
                troopXp[entryIndex] = entry;
            }

            TryUpgrade(stats);
        }

        private void TryUpgrade(CharacterStats stats)
        {
            if (troopRegistry == null || upgradeSystem == null)
            {
                return;
            }

            var troop = troopRegistry.FindByStats(stats);
            if (troop == null || troop.upgradeTarget == null)
            {
                return;
            }

            var entryIndex = troopXp.FindIndex(entry => entry.stats == stats);
            if (entryIndex < 0)
            {
                return;
            }

            var entry = troopXp[entryIndex];
            if (entry.xp < troop.xpToUpgrade)
            {
                return;
            }

            if (upgradeSystem.TryUpgrade(stats))
            {
                entry.xp -= troop.xpToUpgrade;
                troopXp[entryIndex] = entry;
            }
        }

        [System.Serializable]
        public struct TroopXpEntry
        {
            public CharacterStats stats;
            public int xp;

            public TroopXpEntry(CharacterStats stats, int xp)
            {
                this.stats = stats;
                this.xp = xp;
            }
        }
    }
}

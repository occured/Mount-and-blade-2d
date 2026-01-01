using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.Party
{
    public static class ReputationSerializer
    {
        public static List<ReputationEntry> Serialize(ReputationSystem system)
        {
            var entries = new List<ReputationEntry>();
            if (system == null)
            {
                return entries;
            }

            foreach (var entry in system.Reputations)
            {
                if (entry.faction != null)
                {
                    entries.Add(new ReputationEntry
                    {
                        factionId = entry.faction.factionName,
                        relation = entry.relation
                    });
                }
            }

            return entries;
        }

        public static void Deserialize(ReputationSystem system, List<ReputationEntry> entries, List<FactionData> factions)
        {
            if (system == null)
            {
                return;
            }

            system.Clear();
            foreach (var entry in entries)
            {
                var faction = factions.Find(f => f != null && f.factionName == entry.factionId);
                if (faction != null)
                {
                    system.ModifyRelation(faction, entry.relation);
                }
            }
        }

        [System.Serializable]
        public struct ReputationEntry
        {
            public string factionId;
            public int relation;
        }
    }
}

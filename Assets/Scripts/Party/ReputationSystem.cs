using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks relations with factions.
    /// </summary>
    public class ReputationSystem : MonoBehaviour
    {
        [SerializeField] private List<FactionReputation> reputations = new();

        public int GetRelation(FactionData faction)
        {
            if (faction == null)
            {
                return 0;
            }

            foreach (var entry in reputations)
            {
                if (entry.faction == faction)
                {
                    return entry.relation;
                }
            }

            return 0;
        }

        public void ModifyRelation(FactionData faction, int delta)
        {
            if (faction == null)
            {
                return;
            }

            for (var i = 0; i < reputations.Count; i++)
            {
                if (reputations[i].faction == faction)
                {
                    reputations[i] = reputations[i].WithRelation(reputations[i].relation + delta);
                    return;
                }
            }

            reputations.Add(new FactionReputation(faction, delta));
        }

        public IReadOnlyList<FactionReputation> Reputations => reputations;

        public void Clear()
        {
            reputations.Clear();
        }
        [System.Serializable]
        public struct FactionReputation
        {
            public FactionData faction;
            public int relation;

            public FactionReputation(FactionData faction, int relation)
            {
                this.faction = faction;
                this.relation = relation;
            }

            public FactionReputation WithRelation(int newRelation)
            {
                return new FactionReputation(faction, newRelation);
            }
        }
    }
}

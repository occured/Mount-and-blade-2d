using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Holds encounter metadata for a spawned overworld party.
    /// </summary>
    public class EncounterParty : MonoBehaviour
    {
        [SerializeField] private FactionData faction;
        [SerializeField] private int partySize = 10;
        [SerializeField] private List<ScriptableObjects.TroopData> troopRoster = new();

        public FactionData Faction => faction;
        public int PartySize => partySize;
        public IReadOnlyList<ScriptableObjects.TroopData> TroopRoster => troopRoster;

        public void Configure(EncounterTable.Entry entry)
        {
            faction = entry.faction;
            partySize = Random.Range(entry.minPartySize, entry.maxPartySize + 1);
            troopRoster.Clear();
            if (entry.troopPool != null && entry.troopPool.Count > 0)
            {
                for (var i = 0; i < partySize; i++)
                {
                    var troop = entry.troopPool[Random.Range(0, entry.troopPool.Count)];
                    if (troop != null)
                    {
                        troopRoster.Add(troop);
                    }
                }
            }
        }
    }
}

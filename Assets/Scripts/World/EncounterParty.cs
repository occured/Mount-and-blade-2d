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

        public FactionData Faction => faction;
        public int PartySize => partySize;

        public void Configure(EncounterTable.Entry entry)
        {
            faction = entry.faction;
            partySize = Random.Range(entry.minPartySize, entry.maxPartySize + 1);
        }
    }
}

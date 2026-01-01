using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.World
{
    public static class EncounterContext
    {
        public static FactionData Faction { get; private set; }
        public static int PartySize { get; private set; }
        public static List<Character.CharacterStats> EnemyRoster { get; private set; } = new();

        public static void Set(FactionData faction, int partySize, List<Character.CharacterStats> roster = null)
        {
            Faction = faction;
            PartySize = partySize;
            EnemyRoster.Clear();
            if (roster != null)
            {
                EnemyRoster.AddRange(roster);
            }
        }

        public static void Clear()
        {
            Faction = null;
            PartySize = 0;
            EnemyRoster.Clear();
        }
    }
}

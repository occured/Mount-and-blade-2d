using System;
using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EncounterTable", menuName = "MountAndBlade2D/EncounterTable")]
    public class EncounterTable : ScriptableObject
    {
        public List<Entry> entries = new();

        [Serializable]
        public struct Entry
        {
            public FactionData faction;
            public int minPartySize;
            public int maxPartySize;
            public float weight;
        }
    }
}

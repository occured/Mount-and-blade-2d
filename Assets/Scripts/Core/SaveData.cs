using System;
using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.Core
{
    [Serializable]
    public class SaveData
    {
        public int gold;
        public int currentTick;
        public float morale;
        public List<SerializableRosterEntry> roster = new();
    }

    [Serializable]
    public struct SerializableRosterEntry
    {
        public string characterName;
        public string assetGuid;
    }
}

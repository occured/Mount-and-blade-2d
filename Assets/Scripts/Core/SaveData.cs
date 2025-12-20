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
        public int food;
        public int renown;
        public List<SerializableInventoryEntry> inventory = new();
        public List<SerializableRosterEntry> roster = new();
        public List<string> activeQuests = new();
        public List<string> completedQuests = new();
    }

    [Serializable]
    public struct SerializableRosterEntry
    {
        public string characterName;
        public string assetGuid;
        public string characterId;
    }

    [Serializable]
    public struct SerializableInventoryEntry
    {
        public string itemId;
        public int count;
    }
}

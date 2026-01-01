using System;
using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "MountAndBlade2D/LootTable")]
    public class LootTable : ScriptableObject
    {
        public List<Entry> entries = new();

        [Serializable]
        public struct Entry
        {
            public ItemData item;
            public int minAmount;
            public int maxAmount;
            public float weight;
        }
    }
}

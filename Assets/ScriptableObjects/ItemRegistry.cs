using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemRegistry", menuName = "MountAndBlade2D/ItemRegistry")]
    public class ItemRegistry : ScriptableObject
    {
        public List<ItemData> items = new();

        public ItemData FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            foreach (var item in items)
            {
                if (item != null && item.itemId == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}

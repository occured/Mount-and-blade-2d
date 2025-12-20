using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks item stacks for the party inventory.
    /// </summary>
    public class PartyInventory : MonoBehaviour
    {
        [SerializeField] private List<ItemStack> items = new();

        public IReadOnlyList<ItemStack> Items => items;

        public void Add(ItemData item, int amount)
        {
            if (item == null || amount <= 0)
            {
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].item == item)
                {
                    items[i] = new ItemStack(item, items[i].count + amount);
                    return;
                }
            }

            items.Add(new ItemStack(item, amount));
        }

        public bool TryRemove(ItemData item, int amount)
        {
            if (item == null || amount <= 0)
            {
                return false;
            }

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].item == item)
                {
                    if (items[i].count < amount)
                    {
                        return false;
                    }

                    var remaining = items[i].count - amount;
                    if (remaining > 0)
                    {
                        items[i] = new ItemStack(item, remaining);
                    }
                    else
                    {
                        items.RemoveAt(i);
                    }

                    return true;
                }
            }

            return false;
        }

        public int Count(ItemData item)
        {
            if (item == null)
            {
                return 0;
            }

            foreach (var stack in items)
            {
                if (stack.item == item)
                {
                    return stack.count;
                }
            }

            return 0;
        }

        public void Clear()
        {
            items.Clear();
        }
    }

    [System.Serializable]
    public struct ItemStack
    {
        public ItemData item;
        public int count;

        public ItemStack(ItemData item, int count)
        {
            this.item = item;
            this.count = count;
        }
    }
}

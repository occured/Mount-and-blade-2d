using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Stores party members and handles wages, morale, and capacity.
    /// </summary>
    public class PartyRoster : MonoBehaviour
    {
        [SerializeField] private int baseCapacity = 20;
        [SerializeField] private List<CharacterStats> members = new();

        public IReadOnlyList<CharacterStats> Members => members;
        public int Capacity => baseCapacity;

        public bool TryAdd(CharacterStats recruit)
        {
            if (members.Count >= Capacity)
            {
                return false;
            }

            members.Add(recruit);
            return true;
        }

        public void Remove(CharacterStats character)
        {
            members.Remove(character);
        }

        public void Replace(CharacterStats original, CharacterStats replacement)
        {
            var index = members.IndexOf(original);
            if (index >= 0 && replacement != null)
            {
                members[index] = replacement;
            }
        }

        public void Clear()
        {
            members.Clear();
        }

        public void SetCapacity(int capacity)
        {
            baseCapacity = Mathf.Max(1, capacity);
        }

        public int CalculateDailyWages()
        {
            var total = 0;
            foreach (var member in members)
            {
                total += member.wage;
            }

            return total;
        }
    }
}

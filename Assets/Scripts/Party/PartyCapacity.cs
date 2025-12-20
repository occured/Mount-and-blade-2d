using UnityEngine;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Calculates party capacity based on renown and morale.
    /// </summary>
    public class PartyCapacity : MonoBehaviour
    {
        [SerializeField] private PartyRoster roster;
        [SerializeField] private RenownSystem renownSystem;
        [SerializeField] private MoraleSystem moraleSystem;
        [SerializeField] private int baseCapacity = 20;
        [SerializeField] private int renownPerSlot = 25;

        public int GetCapacity()
        {
            var capacity = baseCapacity;
            if (renownSystem != null && renownPerSlot > 0)
            {
                capacity += renownSystem.Renown / renownPerSlot;
            }

            if (moraleSystem != null)
            {
                var moraleModifier = Mathf.FloorToInt((moraleSystem.Morale - 50f) / 20f);
                capacity += moraleModifier;
            }

            return Mathf.Max(1, capacity);
        }

        public void SyncRosterCapacity()
        {
            if (roster != null)
            {
                roster.SetCapacity(GetCapacity());
            }
        }
    }
}

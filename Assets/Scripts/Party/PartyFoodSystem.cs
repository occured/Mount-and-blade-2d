using UnityEngine;
using MountAndBlade2D.Core;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Consumes food each day based on party size and applies morale penalties when food runs out.
    /// </summary>
    public class PartyFoodSystem : MonoBehaviour
    {
        [SerializeField] private GameClock gameClock;
        [SerializeField] private PartyRoster roster;
        [SerializeField] private FoodInventory foodInventory;
        [SerializeField] private MoraleSystem moraleSystem;
        [SerializeField] private int foodPerMemberPerDay = 1;

        private void OnEnable()
        {
            if (gameClock != null)
            {
                gameClock.OnNewDay += HandleNewDay;
            }
        }

        private void OnDisable()
        {
            if (gameClock != null)
            {
                gameClock.OnNewDay -= HandleNewDay;
            }
        }

        private void HandleNewDay(int day)
        {
            if (roster == null || foodInventory == null)
            {
                return;
            }

            var consumption = Mathf.Max(1, roster.Members.Count) * foodPerMemberPerDay;
            if (!foodInventory.TryConsume(consumption))
            {
                moraleSystem?.OnStarvationTick();
            }
        }
    }
}

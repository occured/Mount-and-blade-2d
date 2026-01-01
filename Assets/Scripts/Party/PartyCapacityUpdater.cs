using UnityEngine;
using MountAndBlade2D.Core;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Recomputes party capacity on a daily tick.
    /// </summary>
    public class PartyCapacityUpdater : MonoBehaviour
    {
        [SerializeField] private GameClock gameClock;
        [SerializeField] private PartyCapacity partyCapacity;

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

        private void Start()
        {
            partyCapacity?.SyncRosterCapacity();
        }

        private void HandleNewDay(int day)
        {
            partyCapacity?.SyncRosterCapacity();
        }
    }
}

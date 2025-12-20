using UnityEngine;
using MountAndBlade2D.Core;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Pays daily wages and applies morale changes based on whether wages were covered.
    /// </summary>
    public class PartyWageSystem : MonoBehaviour
    {
        [SerializeField] private GameClock gameClock;
        [SerializeField] private PartyRoster roster;
        [SerializeField] private CurrencyWallet wallet;
        [SerializeField] private MoraleSystem moraleSystem;

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
            if (roster == null)
            {
                return;
            }

            var wages = roster.CalculateDailyWages();
            if (wallet != null && wallet.TrySpend(wages))
            {
                moraleSystem?.OnWagesPaid();
            }
            else
            {
                moraleSystem?.OnWagesMissed();
            }
        }
    }
}

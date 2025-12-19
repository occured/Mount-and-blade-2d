using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.Character;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.Core
{
    /// <summary>
    /// Singleton-style runtime state for party and economy.
    /// </summary>
    public class GameState : MonoBehaviour
    {
        public static GameState Instance { get; private set; }

        [SerializeField] private PartyRoster partyRoster;
        [SerializeField] private CurrencyWallet wallet;
        [SerializeField] private Party.MoraleSystem moraleSystem;
        [SerializeField] private GameClock gameClock;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public PartyRoster PartyRoster => partyRoster;
        public CurrencyWallet Wallet => wallet;
        public Party.MoraleSystem MoraleSystem => moraleSystem;
        public GameClock GameClock => gameClock;

        public void SetRoster(List<CharacterStats> members)
        {
            if (partyRoster == null)
            {
                return;
            }

            partyRoster.Clear();
            foreach (var member in members)
            {
                partyRoster.TryAdd(member);
            }
        }
    }
}

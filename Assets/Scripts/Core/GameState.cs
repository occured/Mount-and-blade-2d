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
        [SerializeField] private Party.FoodInventory foodInventory;
        [SerializeField] private Party.PartyInventory partyInventory;
        [SerializeField] private Party.QuestLog questLog;
        [SerializeField] private Party.ReputationSystem reputationSystem;
        [SerializeField] private Party.RenownSystem renownSystem;
        [SerializeField] private Party.PartyCapacity partyCapacity;

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
        public Party.FoodInventory FoodInventory => foodInventory;
        public Party.PartyInventory PartyInventory => partyInventory;
        public Party.QuestLog QuestLog => questLog;
        public Party.ReputationSystem ReputationSystem => reputationSystem;
        public Party.RenownSystem RenownSystem => renownSystem;
        public Party.PartyCapacity PartyCapacity => partyCapacity;

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

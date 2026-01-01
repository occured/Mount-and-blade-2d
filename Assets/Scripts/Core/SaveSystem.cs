using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MountAndBlade2D.Character;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MountAndBlade2D.Core
{
    /// <summary>
    /// Simple JSON save/load for party and economy.
    /// </summary>
    public class SaveSystem : MonoBehaviour
    {
        [SerializeField] private string saveFileName = "save.json";
        [SerializeField] private GameState gameState;
        [SerializeField] private ScriptableObjects.CharacterRegistry characterRegistry;
        [SerializeField] private ScriptableObjects.ItemRegistry itemRegistry;
        [SerializeField] private ScriptableObjects.QuestRegistry questRegistry;
        [SerializeField] private List<ScriptableObjects.FactionData> factionCatalog = new();

        public void Save()
        {
            if (gameState == null)
            {
                return;
            }

            var data = new SaveData
            {
                gold = gameState.Wallet != null ? gameState.Wallet.Gold : 0,
                currentTick = gameState.GameClock != null ? gameState.GameClock.CurrentTick : 0,
                morale = gameState.MoraleSystem != null ? gameState.MoraleSystem.Morale : 0f,
                food = gameState.FoodInventory != null ? gameState.FoodInventory.Food : 0,
                renown = gameState.RenownSystem != null ? gameState.RenownSystem.Renown : 0,
                inventory = SerializeInventory(gameState.PartyInventory),
                roster = SerializeRoster(gameState.PartyRoster),
                activeQuests = SerializeQuests(gameState.QuestLog, completed: false),
                completedQuests = SerializeQuests(gameState.QuestLog, completed: true),
                reputation = Party.ReputationSerializer.Serialize(gameState.ReputationSystem)
            };

            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSavePath(), json);
        }

        public void Load()
        {
            if (gameState == null)
            {
                return;
            }

            var path = GetSavePath();
            if (!File.Exists(path))
            {
                return;
            }

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveData>(json);
            if (data == null)
            {
                return;
            }

            if (gameState.Wallet != null)
            {
                var delta = data.gold - gameState.Wallet.Gold;
                if (delta > 0)
                {
                    gameState.Wallet.Add(delta);
                }
                else
                {
                    gameState.Wallet.TrySpend(-delta);
                }
            }

            var members = DeserializeRoster(data.roster);
            gameState.SetRoster(members);

            if (gameState.MoraleSystem != null)
            {
                gameState.MoraleSystem.SetMorale(data.morale);
            }

            if (gameState.FoodInventory != null)
            {
                gameState.FoodInventory.SetFood(data.food);
            }

            if (gameState.RenownSystem != null)
            {
                gameState.RenownSystem.SetRenown(data.renown);
            }

            if (gameState.GameClock != null)
            {
                gameState.GameClock.SetTick(data.currentTick);
            }

            if (gameState.PartyInventory != null)
            {
                gameState.PartyInventory.Clear();
                foreach (var entry in data.inventory)
                {
                    var item = itemRegistry != null ? itemRegistry.FindById(entry.itemId) : null;
                    if (item != null)
                    {
                        gameState.PartyInventory.Add(item, entry.count);
                    }
                }
            }

            if (gameState.QuestLog != null)
            {
                gameState.QuestLog.Clear();
                LoadQuestList(data.activeQuests, completed: false);
                LoadQuestList(data.completedQuests, completed: true);
            }

            if (gameState.ReputationSystem != null && factionCatalog != null)
            {
                Party.ReputationSerializer.Deserialize(gameState.ReputationSystem, data.reputation, factionCatalog);
            }
        }

        private string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, saveFileName);
        }

        private static List<SerializableRosterEntry> SerializeRoster(Party.PartyRoster roster)
        {
            var entries = new List<SerializableRosterEntry>();
            if (roster == null)
            {
                return entries;
            }

            foreach (var member in roster.Members)
            {
                entries.Add(new SerializableRosterEntry
                {
                    characterName = member != null ? member.name : string.Empty,
                    assetGuid = GetAssetGuid(member),
                    characterId = member != null ? member.characterId : string.Empty
                });
            }

            return entries;
        }

        private List<CharacterStats> DeserializeRoster(List<SerializableRosterEntry> entries)
        {
            var members = new List<CharacterStats>();
            if (characterRegistry != null)
            {
                foreach (var entry in entries)
                {
                    var found = characterRegistry.FindById(entry.characterId);
                    if (found != null)
                    {
                        members.Add(found);
                        continue;
                    }
                }
            }

#if UNITY_EDITOR
            foreach (var entry in entries)
            {
                if (string.IsNullOrEmpty(entry.assetGuid))
                {
                    continue;
                }

                var assetPath = AssetDatabase.GUIDToAssetPath(entry.assetGuid);
                if (string.IsNullOrEmpty(assetPath))
                {
                    continue;
                }

                var stats = AssetDatabase.LoadAssetAtPath<CharacterStats>(assetPath);
                if (stats != null)
                {
                    members.Add(stats);
                }
            }
#endif
            return members;
        }

        private static List<SerializableInventoryEntry> SerializeInventory(Party.PartyInventory inventory)
        {
            var entries = new List<SerializableInventoryEntry>();
            if (inventory == null)
            {
                return entries;
            }

            foreach (var stack in inventory.Items)
            {
                if (stack.item == null)
                {
                    continue;
                }

                entries.Add(new SerializableInventoryEntry
                {
                    itemId = stack.item.itemId,
                    count = stack.count
                });
            }

            return entries;
        }

        private static List<string> SerializeQuests(Party.QuestLog questLog, bool completed)
        {
            var quests = new List<string>();
            if (questLog == null)
            {
                return quests;
            }

            var source = completed ? questLog.CompletedQuests : questLog.ActiveQuests;
            foreach (var quest in source)
            {
                if (quest != null)
                {
                    quests.Add(quest.questId);
                }
            }

            return quests;
        }

        private void LoadQuestList(List<string> questIds, bool completed)
        {
            if (questRegistry == null || gameState?.QuestLog == null)
            {
                return;
            }

            foreach (var questId in questIds)
            {
                var quest = questRegistry.FindById(questId);
                if (quest == null)
                {
                    continue;
                }

                if (completed)
                {
                    gameState.QuestLog.AddQuest(quest);
                    gameState.QuestLog.CompleteQuest(quest);
                }
                else
                {
                    gameState.QuestLog.AddQuest(quest);
                }
            }
        }

        private static string GetAssetGuid(Object asset)
        {
#if UNITY_EDITOR
            if (asset == null)
            {
                return string.Empty;
            }

            return AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));
#else
            return string.Empty;
#endif
        }
    }
}

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

        public void Save()
        {
            if (gameState == null)
            {
                return;
            }

            var data = new SaveData
            {
                gold = gameState.Wallet != null ? gameState.Wallet.Gold : 0,
                roster = SerializeRoster(gameState.PartyRoster)
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
                    assetGuid = GetAssetGuid(member)
                });
            }

            return entries;
        }

        private static List<CharacterStats> DeserializeRoster(List<SerializableRosterEntry> entries)
        {
            var members = new List<CharacterStats>();
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

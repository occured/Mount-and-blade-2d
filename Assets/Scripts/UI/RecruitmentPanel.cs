using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Character;
using MountAndBlade2D.Party;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.UI
{
    public class RecruitmentPanel : MonoBehaviour
    {
        [SerializeField] private PartyRoster roster;
        [SerializeField] private CurrencyWallet wallet;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button recruitButtonPrefab;
        [SerializeField] private List<RecruitOption> recruits = new();

        private void OnEnable()
        {
            RenderOptions();
        }

        private void RenderOptions()
        {
            if (contentRoot == null || recruitButtonPrefab == null)
            {
                return;
            }

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (var recruit in recruits)
            {
                var button = Instantiate(recruitButtonPrefab, contentRoot);
                var label = button.GetComponentInChildren<TMPro.TMP_Text>();
                var cost = recruit.cost;
                if (cost <= 0 && recruit.troopData != null)
                {
                    cost = recruit.troopData.recruitCost;
                }
                if (label != null)
                {
                    label.text = $"{recruit.stats.name} — Cost: {cost} — Wage: {recruit.stats.wage}";
                }

                var option = recruit;
                option.cost = cost;
                button.onClick.AddListener(() => AttemptRecruit(option));
            }
        }

        private void AttemptRecruit(RecruitOption recruit)
        {
            if (roster == null)
            {
                Debug.Log("Recruitment failed: missing roster reference.");
                return;
            }

            if (recruit.stats == null)
            {
                Debug.Log("Recruitment failed: missing recruit stats.");
                return;
            }

            if (wallet != null && !wallet.TrySpend(recruit.cost))
            {
                Debug.Log("Recruitment failed: not enough gold.");
                return;
            }

            if (roster.TryAdd(recruit.stats))
            {
                Debug.Log($"Recruited {recruit.stats.name}");
            }
            else
            {
                Debug.Log("Recruitment failed: roster full.");
                if (wallet != null)
                {
                    wallet.Add(recruit.cost);
                }
            }
        }

        [System.Serializable]
        private struct RecruitOption
        {
            public CharacterStats stats;
            public TroopData troopData;
            public int cost;
        }
    }
}

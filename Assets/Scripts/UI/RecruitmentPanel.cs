using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Character;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.UI
{
    public class RecruitmentPanel : MonoBehaviour
    {
        [SerializeField] private PartyRoster roster;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button recruitButtonPrefab;
        [SerializeField] private List<CharacterStats> recruits = new();

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
                if (label != null)
                {
                    label.text = $"{recruit.name} — Wage: {recruit.wage}";
                }

                button.onClick.AddListener(() => AttemptRecruit(recruit));
            }
        }

        private void AttemptRecruit(CharacterStats recruit)
        {
            if (roster != null && roster.TryAdd(recruit))
            {
                Debug.Log($"Recruited {recruit.name}");
            }
            else
            {
                Debug.Log("Recruitment failed: roster full or missing roster reference.");
            }
        }
    }
}

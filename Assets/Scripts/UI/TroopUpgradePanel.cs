using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Character;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.UI
{
    public class TroopUpgradePanel : MonoBehaviour
    {
        [SerializeField] private PartyRoster roster;
        [SerializeField] private TroopUpgradeSystem upgradeSystem;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button upgradeButtonPrefab;

        private void OnEnable()
        {
            Render();
        }

        private void Render()
        {
            if (roster == null || upgradeSystem == null || contentRoot == null || upgradeButtonPrefab == null)
            {
                return;
            }

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (var member in roster.Members)
            {
                var button = Instantiate(upgradeButtonPrefab, contentRoot);
                var label = button.GetComponentInChildren<TMPro.TMP_Text>();
                if (upgradeSystem.CanUpgrade(member, out var current, out var next))
                {
                    if (label != null)
                    {
                        label.text = $"Upgrade {member.name} → {next.baseStats.name} ({current.upgradeCost}g)";
                    }
                    button.interactable = true;
                    button.onClick.AddListener(() => upgradeSystem.TryUpgrade(member));
                }
                else
                {
                    if (label != null)
                    {
                        label.text = $"{member.name} (No upgrade)";
                    }
                    button.interactable = false;
                }
            }
        }
    }
}

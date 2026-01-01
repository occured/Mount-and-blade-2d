using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Core;

namespace MountAndBlade2D.UI
{
    public class SaveLoadPanel : MonoBehaviour
    {
        [SerializeField] private SaveSystem saveSystem;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;

        private void Awake()
        {
            if (saveButton != null)
            {
                saveButton.onClick.AddListener(HandleSave);
            }

            if (loadButton != null)
            {
                loadButton.onClick.AddListener(HandleLoad);
            }
        }

        private void HandleSave()
        {
            saveSystem?.Save();
        }

        private void HandleLoad()
        {
            saveSystem?.Load();
        }
    }
}

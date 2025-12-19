using System;
using UnityEngine;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Tracks spawned party lifetimes to decrement counts when destroyed.
    /// </summary>
    public class PartyLifetime : MonoBehaviour
    {
        private Action _onDestroyed;

        public void Init(Action onDestroyed)
        {
            _onDestroyed = onDestroyed;
        }

        private void OnDestroy()
        {
            _onDestroyed?.Invoke();
        }
    }
}

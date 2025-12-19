using System;
using UnityEngine;

namespace MountAndBlade2D.Core
{
    /// <summary>
    /// Discrete tick-based clock for overworld simulation. Emits events at dawn/dusk and every tick.
    /// </summary>
    public class GameClock : MonoBehaviour
    {
        [SerializeField] private float secondsPerTick = 1f;
        [SerializeField] private int ticksPerDay = 120;

        private float _accumulator;
        private int _currentTick;

        public event Action<int> OnTick;
        public event Action<int> OnNewDay;
        public int CurrentTick => _currentTick;
        public int CurrentDay => ticksPerDay > 0 ? _currentTick / ticksPerDay : 0;

        private void Update()
        {
            _accumulator += Time.deltaTime;
            while (_accumulator >= secondsPerTick)
            {
                _accumulator -= secondsPerTick;
                AdvanceTick();
            }
        }

        private void AdvanceTick()
        {
            _currentTick++;
            OnTick?.Invoke(_currentTick);

            if (_currentTick % ticksPerDay == 0)
            {
                OnNewDay?.Invoke(_currentTick / ticksPerDay);
            }
        }

        public void SetTick(int tick)
        {
            _currentTick = Mathf.Max(0, tick);
        }
    }
}

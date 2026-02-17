using System;
using UnityEngine;

namespace Modul_30_1_2
{
    public class Timer
    {
        public event Action<TimerActions> TimerAction;
        public event Action<int> TimerSecondsChanged;

        private Rfloat _currentTimerTime;

        public Timer(int timerTime)
        {
            SetTimerTime(timerTime);

            _currentTimerTime = new();
            _currentTimerTime.ChangedIntPart += (value) => TimerSecondsChanged?.Invoke(value);

            TimerSecondsChanged += x => Debug.Log(x.ToString());
            TimerAction += x => Debug.Log(x.ToString());
        }

        public bool IsRunning { get; private set; }
        public int TimerTime { get; private set; }

        public void Update(float time)
        {
            if (!IsRunning)
                return;

            UpdateTimer(time);
        }

        public void SetTimerTime(int time)
        {
            if (time > 0)
                TimerTime = time;
        }

        public void StartTimer()
        {
            _currentTimerTime.Value = TimerTime;
            IsRunning = true;
            TimerAction?.Invoke(TimerActions.Start);
        }

        public void StopTimer()
        {
            _currentTimerTime.Value = 0;
            IsRunning = false;
            TimerAction?.Invoke(TimerActions.Stop);
        }

        public void PauseTimer()
        {
            IsRunning = false;
            TimerAction?.Invoke(TimerActions.Pause);
        }

        public void ResumeTimer()
        {
            IsRunning = true;
            TimerAction?.Invoke(TimerActions.Resume);
        }

        public void ResetTimer()
        {
            _currentTimerTime.Value = TimerTime;
            TimerAction?.Invoke(TimerActions.Reset);
        }

        private void UpdateTimer(float time)
        {
            _currentTimerTime.Value -= time;

            if (_currentTimerTime.Value <= 0)
            {
                _currentTimerTime.Value = 0;
                IsRunning = false;
                TimerAction?.Invoke(TimerActions.Done);
            }
        }
    }
}
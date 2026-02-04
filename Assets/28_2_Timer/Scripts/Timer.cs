using System;
using UnityEngine;

namespace Modul_28_2
{
    public class Timer
    {
        public event Action<TimerActions> TimerAction;
        public event Action<int> TimerSecondsChanged;

        private float _currentTimerTime;

        public Timer(int timerTime)
        {
            SetTimerTime(timerTime);

            //TimerSecondsChanged += x => Debug.Log(x.ToString());
            //TimerAction += x => Debug.Log(x.ToString());
        }

        public bool IsRunning { get; private set; }
        public int TimerTime { get; private set; }

        public float CurrentTimerTime
        {
            get => _currentTimerTime;
            private set
            {
                if (Mathf.Ceil(_currentTimerTime) != Mathf.Ceil(value))
                {
                    TimerSecondsChanged?.Invoke((int)Mathf.Ceil(value));
                }

                _currentTimerTime = value;
            }
        }

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
            CurrentTimerTime = TimerTime;
            IsRunning = true;
            TimerAction?.Invoke(TimerActions.Start);
        }

        public void StopTimer()
        {
            CurrentTimerTime = 0;
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
            CurrentTimerTime = TimerTime;
            TimerAction?.Invoke(TimerActions.Reset);
        }

        private void UpdateTimer(float time)
        {
            CurrentTimerTime -= time;

            if (CurrentTimerTime <= 0)
            {
                CurrentTimerTime = 0;
                IsRunning = false;
                TimerAction?.Invoke(TimerActions.Done);
            }
        }
    }
}
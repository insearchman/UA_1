using UnityEngine;

namespace Modul_30_1_2
{
    public enum TimerActions
    {
        Start,
        Stop,
        Pause,
        Resume,
        Reset,
        Done
    }

    [RequireComponent(typeof(TimerView))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int _timerTime = 5;
        private TimerView _uiView;

        private Timer _timer;

        private void Awake()
        {
            _timer = new(_timerTime);

            _uiView = GetComponent<TimerView>();
            _uiView.Init(_timer);

            _timer.StartTimer();
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }
    }
}
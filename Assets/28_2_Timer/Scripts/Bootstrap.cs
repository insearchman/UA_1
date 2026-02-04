using UnityEngine;

namespace Modul_28_2
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

    [RequireComponent(typeof(UIView))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int _timerTime = 5;
        private UIView _uiView;

        private Timer _timer;

        private void Awake()
        {
            _uiView = GetComponent<UIView>();
            _timer = new(_timerTime);
            _timer.StartTimer();

            _uiView.Init(_timer);
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }
    }
}
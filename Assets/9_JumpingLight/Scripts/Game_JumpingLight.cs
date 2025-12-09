using TMPro;
using UnityEngine;

namespace Modul_9
{
    [RequireComponent(typeof(Border))]
    public class Game_JumpingLight : MonoBehaviour
    {
        private const KeyCode RestartKey = KeyCode.Space;
        private const KeyCode JumpUpKey = KeyCode.UpArrow;
        private const KeyCode JumpLeftKey = KeyCode.LeftArrow;
        private const KeyCode JumpRightKey = KeyCode.RightArrow;

        private const string WinText = "WIN";
        private const string DefeatText = "DEFEAT";

        [SerializeField] private Ball _ball;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _result;

        [SerializeField] private int _scoreToWin = 1000;
        [SerializeField] private int _sideJumpMultiplier = 10;

        private Border _borders;

        private bool _isRuning;

        private void Awake()
        {
            _borders = GetComponent<Border>();
        }

        void Start()
        {
            StartGame();
        }

        void Update()
        {
            if (Input.GetKeyDown(RestartKey))
                StartGame();

            if (_isRuning == false)
                return;

            JumpHandler();

            if (GetScore() >= _scoreToWin)
                WinGame();

            if (_borders.IsHitWith(_ball))
                StopGame();
        }

        private void StartGame()
        {
            _ball.ResetBallState();

            _result.text = "";
            _score.text = "";

            _isRuning = true;
        }

        private void StopGame()
        {
            _ball.gameObject.SetActive(false);

            _isRuning = false;

            _result.text = DefeatText;

            Debug.Log($"Вы проиграли со счётом: {GetScore()}");
        }

        private void WinGame()
        {
            _ball.SetKinematic(true);

            _isRuning = false;

            _result.text = WinText;

            Debug.Log("Вы победили!");
        }

        private int GetScore()
        {
            return _ball.UpJumpCount + _ball.SideJumpCount * _sideJumpMultiplier;
        }

        private void JumpHandler()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(JumpUpKey))
                {
                    Jump(JumpDirection.Up);
                }

                if (Input.GetKeyDown(JumpLeftKey))
                {
                    Jump(JumpDirection.Left);
                }

                if (Input.GetKeyDown(JumpRightKey))
                {
                    Jump(JumpDirection.Right);
                }

                _score.text = GetScore().ToString();
            }
        }

        private void Jump(JumpDirection jumpDirection)
        {
            if (_ball.IsKinematic)
                _ball.SetKinematic(false);

            _ball.Jump(jumpDirection);
        }
    }
}
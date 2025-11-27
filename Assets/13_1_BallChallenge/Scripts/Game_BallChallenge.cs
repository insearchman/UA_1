using TMPro;
using UnityEngine;

[RequireComponent (typeof(CoinsManager))]
public class Game_BallChallenge : MonoBehaviour
{
    private const string WinGameText = "You WIN";
    private const string DefeatGameText = "You DEFEAT";

    [SerializeField] private TextMeshProUGUI _gameTimerText;
    [SerializeField] private TextMeshProUGUI _gameResultText;
    [SerializeField] private Player _player;

    [SerializeField] private float GameTime = 60;

    private CoinsManager _coinsManager;

    private float _currentGameTime;
    private bool _isPlay;

    private void Awake()
    {
        _coinsManager = GetComponent<CoinsManager>();
        _coinsManager.OnAllCointsCollected += WinGame;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (_isPlay)
            UpdateGameTimer();
    }

    private void StartGame()
    {
        _isPlay = true;
        _currentGameTime = GameTime;
        _gameResultText.text = string.Empty;

        _coinsManager.GenerateCoins();
    }

    private void UpdateGameTimer()
    {
        _currentGameTime -= Time.deltaTime;
        _gameTimerText.text = _currentGameTime.ToString("0");

        if (_currentGameTime < 0)
        {
            DefeatGame();
        }
    }

    private void StopGame()
    {
        _isPlay = false;
        _player.enabled = false;
    }

    private void WinGame()
    {
        StopGame();
        _gameResultText.text = WinGameText;
    }

    private void DefeatGame()
    {
        StopGame();
        _gameResultText.text = DefeatGameText;
    }
}
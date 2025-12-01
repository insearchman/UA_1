using TMPro;
using UnityEngine;

[RequireComponent (typeof(CoinsSpawner))]
public class Game_BallChallenge : MonoBehaviour
{
    private const string WinGameText = "You WIN";
    private const string DefeatGameText = "You DEFEAT";

    private const float GameTime = 60;

    [SerializeField] private TextMeshProUGUI _gameTimerText;
    [SerializeField] private TextMeshProUGUI _gameResultText;
    [SerializeField] private Player_15 _player;

    private CoinsSpawner _coinsManager;

    private float _currentGameTime;
    private bool _isPlay;

    private void Awake()
    {
        _coinsManager = GetComponent<CoinsSpawner>();
        _player.OnCoinCollect += CoinCollectHandler;
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

        if (_currentGameTime < 0)
        {
            DefeatGame();
        }

        _gameTimerText.text = _currentGameTime.ToString("0");
    }

    private void StopGame()
    {
        _isPlay = false;
        _player.enabled = false;
    }

    private void CoinCollectHandler()
    {
        if(_player.CoinsWallet >= _coinsManager.AllCoins.Count)
        {
            WinGame();
        }
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
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePresenter : MonoBehaviour
{
    private GameState _currentGameState;
    private PlayerInput _input;
    [SerializeField] private PlayerPresenter _player;
    [SerializeField] private GameView _view;
    [SerializeField] private GameLevelsManager _levelsManager;
    [SerializeField] private EnemyPresenter[] _enemiesPool;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();

        _input.OnInputTap += InputHitHandler;

        _currentGameState = GameState.start;
        DisplayGameState();

        _levelsManager.OnAllLevelsCompleted += AllLevelsCompltedHandler;
        _levelsManager.OnLevelCompleted += LevelCompletedHandler;

        for (int i = 0; i < _enemiesPool.Length; i++)
        {
            _enemiesPool[i].OnEnemyDefetead += EnemyDefeatedHandler;
        }

        _levelsManager.StartGame();
    }

    private void OnDisable()
    {
        _input.OnInputTap -= InputHitHandler;

        for (int i = 0; i < _enemiesPool.Length; i++)
        {
            _enemiesPool[i].OnEnemyDefetead -= EnemyDefeatedHandler;
        }
    }
    // Enemy action handler
    private void EnemyDefeatedHandler()
    {
        _levelsManager.EnemyDead();
    }

    // Input action handlers
    private void InputHitHandler()
    {
        if (_currentGameState == GameState.start) 
        {
            NextGameState();
            DisplayGameState();
            _player.Move();
        }
        else if (_currentGameState == GameState.end)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            _player.Hit();
        }
        
    }

    // Game levels manager handlers
    private void AllLevelsCompltedHandler()
    {
        NextGameState();
        DisplayGameState();
    }

    private void LevelCompletedHandler()
    {
        _player.Move();
    }

    private void NextGameState()
    {
        switch (_currentGameState) 
        {
            case GameState.start:
                _currentGameState = GameState.game;
                break;
            case GameState.game:
                _currentGameState = GameState.end;
                break;
            case GameState.end:
                _currentGameState = GameState.start;
                break;
            default:
                Debug.LogError($"Missing case for {_currentGameState} game state");
                break;
        }

    }

    private void DisplayGameState()
    {
        switch (_currentGameState)
        {
            case GameState.start:
                _view.UpdateView("Tap to start game...");
                break;
            case GameState.game:
                _view.UpdateView("");
                break;
            case GameState.end:
                _view.UpdateView("You are awesome!! Tap to restart game...");
                break;
            default:
                Debug.LogError($"Missing case for {_currentGameState} game state");
                break;
        }
    }
}

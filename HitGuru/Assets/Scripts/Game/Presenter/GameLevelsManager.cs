using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelsManager : MonoBehaviour
{
    [SerializeField] private GameLevelEntity[] _levelsEntities;
    private GameLevelModel[] _levelsModels;
    private int _currentLevel;

    public event Action OnLevelCompleted;
    public event Action OnAllLevelsCompleted;

    public void StartGame()
    {
        _currentLevel = 0;
        SetupData();
    }

    private void SetupData()
    {
        _levelsModels = new GameLevelModel[_levelsEntities.Length];

        for (int i = 0; i < _levelsEntities.Length; i++)
        {
            _levelsModels[i] = new GameLevelModel(_levelsEntities[i]);
        }
    }

    public void EnemyDead()
    {
        GameLevelModel model = _levelsModels[_currentLevel];
        model.EnemyDefeated();

        if (model.EnemyCount < 1)
        {
            OnLevelCompleted?.Invoke();
            NextLevel();
        }

    }

    private void NextLevel()
    {
        if (_currentLevel == _levelsModels.Length - 1)
        {
            OnAllLevelsCompleted?.Invoke();
        }
        else
        {
            _currentLevel++;
        }
        
    }
}

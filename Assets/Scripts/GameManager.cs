using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Puzzle[] requiredPuzzles;

    private GameState _gameState;
    public GameState gameState
    {
        get { return _gameState; }
        set
        {
            GameState oldState = _gameState;
            switch (value)
            {
                default:
                    break;
            }
            _gameState = value;
        }
    }
    
    private void Awake()
    {
    }

    private void CheckIfFinished()
    {
        if (requiredPuzzles == null) { return; }
    }

    public enum GameState
    {
        OFF,
        PLAYING,
        END
    }
}

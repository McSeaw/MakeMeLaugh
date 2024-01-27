using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    private int _currentLevel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameState state)
    {
        State = state;
        switch (State)
        {
            case GameState.Lose:
                GameLose();
                break;
            case GameState.InGame:
                break;
            case GameState.Victory:
                break;
        }
    }

    public enum GameState
    {
        Lose,
        InGame,
        Victory,
    }

    void GameLose()
    {
        SceneManager.
    }
}

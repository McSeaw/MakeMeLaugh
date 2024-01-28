using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    private int _currentLevel;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _laughAudio;
    [SerializeField] AudioClip _angryAudio;

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
        Physics.IgnoreLayerCollision(6, 7, true);
        _audioSource = GetComponent<AudioSource>(); 
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
                LoadLevel();
                break;
            case GameState.Win:
                _currentLevel += 1;
                GameWin();
                break;
            case GameState.Quit:
                QuitGame();
                break;
        }
    }

    public enum GameState
    {
        Lose,
        InGame,
        Win,
        Quit,
    }

    void GameLose()
    {
        _audioSource.PlayOneShot(_angryAudio);
        SceneManager.LoadScene(1);
    }

    void GameWin()
    {
        _audioSource.PlayOneShot(_laughAudio);
        SceneManager.LoadScene(2);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(3 + _currentLevel);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

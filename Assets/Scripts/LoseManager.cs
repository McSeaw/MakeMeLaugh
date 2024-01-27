using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.InGame);
    }

    public void QuitGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Quit);
    }
}

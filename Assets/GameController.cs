using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Preparation, BeginWave, Waving ,Action, EndWave, GameOver }

public class GameController : MonoBehaviour {
    [HideInInspector]
    

    public static GameState gameState;
    bool game_over = false;

    public GameObject endWaveSound;

    public float preparationTime = 30.0f;
    float countDown;

    // Use this for initialization
    void Start () {
        gameState = GameState.Preparation;
        countDown = preparationTime;
       
	}

    public static void ChangeGameState(GameState newState)
    {
        Debug.Log(newState);
        if (gameState != newState)
            gameState = newState;
    }
	
	// Update is called once per frame
	void Update () {
		if(gameState == GameState.Preparation)
        {
            if(countDown <= 0)
            {
                countDown = preparationTime;
                ChangeGameState(GameState.BeginWave);
            }
            countDown -= Time.deltaTime;
        }
        else if(gameState == GameState.GameOver)
        {
            Time.timeScale = 1;

            if (!game_over) {
                Debug.Log("Game Over Man");
                game_over = true;
            }
        }
        else if(gameState == GameState.EndWave)
        {
            Time.timeScale = 1;
            if (endWaveSound != null)
            {
                Instantiate(endWaveSound);
            }
            ChangeGameState(GameState.Preparation);
        }


	}


}

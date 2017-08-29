using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [HideInInspector]
    public enum GameState { Preparation, BeginWave, Action ,EndWave, GameOver }

    public GameState gameState;

    public static GameController gc; 

    public float preparationTime = 30.0f;
    float countDown;

    // Use this for initialization
    void Start () {
        gc = this;
        gameState = GameState.Preparation;
        countDown = preparationTime;
        
	}

    public void ChangeGameState(GameState newState)
    {
        if (gameState != newState)
            gameState = newState;
    }
	
	// Update is called once per frame
	void Update () {
		if(gameState == GameState.Preparation)
        {
            if(countDown <= 0)
            {
                ChangeGameState(GameState.BeginWave);
            }
            countDown -= Time.deltaTime;
        }else if(gameState == GameState.GameOver)
        {
            Debug.Log("Game Over Man");
        }


	}
}

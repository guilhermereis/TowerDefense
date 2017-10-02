using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {GameActivate ,Preparation,BeginWave, Waving,Action, EndWave, GameOver }

public class GameController : MonoBehaviour {

    public delegate void GameStateChangedDelegate();
    public static GameStateChangedDelegate gamechangedDelegate;

    [HideInInspector]
    public static GameState gameState;
    public bool game_over = false;

    public GameObject endWaveSound;
    public GameObject gameStateUI;
    private Button startWaveButton;

    public float preparationTime = 30.0f;
    float countDown;
    public SteamStatsAndAchievements stats;


    // Use this for initialization
    void Start () {
        gameState = GameState.Preparation;
        countDown = preparationTime;
        startWaveButton = gameStateUI.transform.Find("StartWave").gameObject.GetComponent<Button>();
        stats = GameObject.FindObjectOfType<SteamStatsAndAchievements>();

    }

    private void OnGUI()
    {
        stats.Render();
    }

    public static void ChangeGameState(GameState newState)
    {
        Debug.Log(newState);
        if (gameState != newState)
        {
            gameState = newState;
            gamechangedDelegate();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameState == GameState.Preparation)
        {

            if (countDown > 0f && countDown <= 10f) {
                startWaveButton.transform.Find("Countdown").gameObject.SetActive(true);
                startWaveButton.transform.Find("CountdownShadow").gameObject.SetActive(true);
                startWaveButton.transform.Find("Countdown").GetComponent<Text>().text = "" + Mathf.CeilToInt(countDown);
                startWaveButton.transform.Find("CountdownShadow").GetComponent<Text>().text = "" + Mathf.CeilToInt(countDown);
            }
            else {
                startWaveButton.transform.Find("Countdown").gameObject.SetActive(false);
                startWaveButton.transform.Find("CountdownShadow").gameObject.SetActive(false);
            }

            if (countDown <= 0)
            {
                countDown = preparationTime;
                ChangeGameState(GameState.BeginWave);
                startWaveButton.GetComponent<Image>().fillAmount = 1;
            }
            else
            {
                startWaveButton.GetComponent<Image>().fillAmount = 1 - countDown / preparationTime;
            }

            countDown -= Time.deltaTime;
        }
        else if (gameState == GameState.BeginWave) {
            gameStateUI.GetComponent<Animator>().SetTrigger("ShowMap");
        }
        else if (gameState == GameState.GameOver)
        {
            GetComponent<FastForward>().NormalSpeedOnClick();

            if (!game_over)
            {
                if (WaveSpawner.gainSecondChanceCounter >= 0)
                {
                    Debug.Log("Game Over Man");
                    game_over = true;
                    GameObject sc_object = GameObject.Find("SecondChanceDialog");
                    if (sc_object != null)
                    {
                        sc_object.GetComponent<SecondChance>().Show();
                        Debug.Log("Want to try Again ?");
                    }
                }
            }
        }
        else if (gameState == GameState.EndWave)
        {
            GetComponent<FastForward>().NormalSpeedOnClick();
            if (endWaveSound != null)
            {
                Instantiate(endWaveSound);
            }
            gameStateUI.GetComponent<Animator>().SetTrigger("ShowPreparation");
            ChangeGameState(GameState.Preparation);
        }
	}

    public void ForceWaveStart() {
        if (gameState == GameState.Preparation) {
            ChangeGameState(GameState.BeginWave);
            countDown = preparationTime;
        }
    }


}

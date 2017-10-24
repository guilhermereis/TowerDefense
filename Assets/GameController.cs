using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {GameActivate,GamePaused ,Preparation,BeginWave, Waving,Action, EndWave, GameOver }

public class GameController : MonoBehaviour {

    public delegate void GameStateChangedDelegate();
    public static GameStateChangedDelegate gamechangedDelegate;

    public delegate void EndWaveDelegate();
    public static EndWaveDelegate endWaveDelegate;

    public delegate void ToggleGizmoDelegate();
    public static ToggleGizmoDelegate toggleGizmoDelegate;

    [HideInInspector]
    public static GameState gameState;
    public bool game_over = false;

    public GameObject endWaveSound;
    GameObject victorySound;

    public GameObject gameStateUI;
    private Button startWaveButton;

    [Header("UI")]
    public Sprite towerGizmosOnButtonSprite;
    public Sprite towerGizmosOffButtonSprite;
    public static bool towerGizmosOn = true;
    public GameObject toggleGizmoButton;

    [Header("Game State")]
    public float preparationTime = 30.0f;
    float countDown;
    public static SteamStatsAndAchievements stats;

    private void OnEnable()
    {
        stats = GameObject.FindObjectOfType<SteamStatsAndAchievements>();


    }

#region steamstats
    //add built towers during game
    public static void AddBuiltTower(BuildType towerType)
    {
        if (stats)
            stats.AddBuiltTower(towerType);


    }

    public static void TryAgain()
    {
        if (stats)
            stats.TryAgain();
    }

    public static void AddBuiltMine()
    {
        if (stats)
            stats.AddMine();
    }

    public static void UnlockLane(int lane)
    {
        if (stats)
        {
            if (lane == 2)
                stats.UnlockLane2();
            else if(lane == 3)
                stats.UnlockLane3();
            else if(lane == 4)
                stats.UnlockLane4();
        }
        
    }

    public static void Freeze()
    {
        if (stats)
         stats.FreezeGoblin();
        
    }

    public static void Repair()
    {
        if (stats)
            stats.Repair();
    }

    public static void AddMonsterKilled(PawnType monsterType, DamageType _damage)
    {
        if (stats)
            stats.AddMonstersKilled(monsterType,_damage);
    }

    public static void MoneyCollected(int gold, bool wasCollected)
    {
        if (stats)
            stats.AddMoneyCollected(gold, wasCollected);
    }

    public static void MoneySpent(int gold)
    {
        if (stats)
            stats.SpendMoney(gold);
    }

    //public static void Add

#endregion
    // Use this for initialization
    void Start () {
        gamechangedDelegate+= evaluateGameStateChanged;
        toggleGizmoDelegate += evaluateGizmoToggle;
        gameState = GameState.Preparation;
        countDown = preparationTime;
        startWaveButton = gameStateUI.transform.Find("StartWave").gameObject.GetComponent<Button>();
        
    }

    private void OnGUI()
    {
        //stats.Render();
    }

    public static void ChangeGameState(GameState newState)
    {
        if (gameState != newState)
        {
            gameState = newState;
            if (gamechangedDelegate != null)
                gamechangedDelegate();
        }
    }

    public void evaluateGameStateChanged() {
        switch (gameState) {
            case GameState.Preparation:
                gameStateUI.GetComponent<Animator>().SetTrigger("ShowPreparation");
                break;
            case GameState.BeginWave:
                gameStateUI.GetComponent<Animator>().SetTrigger("ShowMap");
                break;
        }
    }

    public void evaluateGizmoToggle() {

    }

    public void toggleGizmo() {
        towerGizmosOn = !towerGizmosOn;
        if (towerGizmosOn)
        {
            toggleGizmoButton.GetComponent<Image>().overrideSprite = towerGizmosOnButtonSprite;
            toggleGizmoButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            toggleGizmoButton.GetComponent<Image>().overrideSprite = towerGizmosOffButtonSprite;
            toggleGizmoButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        toggleGizmoDelegate();
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("k")) {
            TakeScreenshot();
        }

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
        else if (gameState == GameState.GameOver)
        {
            GetComponent<TopRightMenu>().NormalSpeedOnClick();
            //
            if (!game_over)
            {
                if (WaveSpawner.gainSecondChanceCounter >= WaveSpawner.secondChanceWaveCountTarget)
                {
                   
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
            if(endWaveDelegate != null)
                endWaveDelegate();

            GetComponent<TopRightMenu>().NormalSpeedOnClick();
            if (endWaveSound != null)
            {
                if (victorySound == null)
                    victorySound = Instantiate(endWaveSound);
                else
                    victorySound.GetComponent<AudioSource>().Play();
            }
            ChangeGameState(GameState.Preparation);
        }
	}

    public void ForceWaveStart() {
        if (gameState == GameState.Preparation) {
            ChangeGameState(GameState.BeginWave);
            countDown = preparationTime;
        }
    }

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("screen_{0}x{1}_{2}.png",
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public static void TakeScreenshot() {
        ScreenCapture.CaptureScreenshot(ScreenShotName(1920,1080), 2);
    }

}

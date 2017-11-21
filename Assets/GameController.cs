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
    public GameObject waveStartSound;
    public GameObject clock1Sound;
    public GameObject clock2Sound;

    public GameObject gameStateUI;
    public GameObject secondChanceUI;
    private Button startWaveButton;

    [Header("UI")]
    public Sprite towerGizmosOnButtonSprite;
    public Sprite towerGizmosOffButtonSprite;
    public static bool towerGizmosOn;
    public GameObject toggleGizmoButton;

    [Header("Game State")]
    public float preparationTime = 30.0f;
    float countDown;
    public static SteamStatsAndAchievements stats;
    int CurrentCountdown = 11;
#if !DISABLESTEAMWORKS
 

    private void OnEnable()
    {
        stats = GameObject.FindObjectOfType<SteamStatsAndAchievements>();
    }
#endif

    #region steamstats
    //add built towers during game
    public static void AddBuiltTower(BuildType towerType)
    {
        #if !DISABLESTEAMWORKS
        if (stats)
            stats.AddBuiltTower(towerType);
        #endif
    }

    public static void TryAgain()
    {
        #if !DISABLESTEAMWORKS
        if (stats)
            stats.TryAgain();
#endif
    }

    public static void AddBuiltMine()
    {
#if !DISABLESTEAMWORKS
        if (stats)
            stats.AddMine();
#endif
    }

    public static void UnlockLane(int lane)
    {
#if !DISABLESTEAMWORKS
        if (stats)
        {
            if (lane == 2)
                stats.UnlockLane2();
            else if(lane == 3)
                stats.UnlockLane3();
            else if(lane == 4)
                stats.UnlockLane4();
        }
#endif
    }

    public static void Freeze()
    {
#if !DISABLESTEAMWORKS
        if (stats)
         stats.FreezeGoblin();
#endif
    }

    public static void Repair()
    {
#if !DISABLESTEAMWORKS
        if (stats)
            stats.Repair();
#endif
    }

    public static void AddMonsterKilled(PawnType monsterType, DamageType _damage)
    {
#if !DISABLESTEAMWORKS
        if (stats)
            stats.AddMonstersKilled(monsterType,_damage);
#endif
    }
    public static void MoneyCollected(int gold, bool wasCollected)
    {
#if !DISABLESTEAMWORKS
        if (stats)
            stats.AddMoneyCollected(gold, wasCollected);
#endif
    }

    public static void MoneySpent(int gold)
    {
#if !DISABLESTEAMWORKS
        if (stats)
            stats.SpendMoney(gold);
#endif
    }

    //public static void Add

#endregion

    // Use this for initialization
    void Start () {
        gamechangedDelegate+= evaluateGameStateChanged;
        toggleGizmoDelegate += evaluateGizmoToggle;
        ChangeGameState(GameState.GameActivate);
        //gameState = GameState.GameActivate;
        countDown = preparationTime;
        startWaveButton = gameStateUI.transform.Find("StartWave").gameObject.GetComponent<Button>();
        towerGizmosOn = true;
    }

    public static void GameStart() {
        //gameState = GameState.Preparation;
        ChangeGameState(GameState.Preparation);
    }

    private void OnDestroy()
    {
        gamechangedDelegate -= evaluateGameStateChanged;
        endWaveDelegate = null;
        toggleGizmoDelegate = null;
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
                CurrentCountdown = 11;
                break;
            case GameState.BeginWave:
                SoundToPlay.PlaySfx(waveStartSound,0.2f);
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

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!TopRightMenu.isConfigOn && !TopRightMenu.isSecondChanceOn) {
                if (gameState == GameState.Preparation) {
                    ForceWaveStart();
                }
            }
        }

        if (gameState == GameState.Preparation)
        {
            if (countDown > 0f && countDown <= 10f) {
                int countdownValue = Mathf.CeilToInt(countDown);
                if (countdownValue < CurrentCountdown) {
                    if (countdownValue % 2 == 0)
                    {
                        SoundToPlay.PlaySfx(clock1Sound);
                    }
                    else {
                        SoundToPlay.PlaySfx(clock2Sound);
                    }
                    CurrentCountdown = countdownValue;
                }

                startWaveButton.transform.Find("Countdown").gameObject.SetActive(true);
                startWaveButton.transform.Find("CountdownShadow").gameObject.SetActive(true);
                startWaveButton.transform.Find("Countdown").GetComponent<Text>().text = "" + countdownValue;
                startWaveButton.transform.Find("CountdownShadow").GetComponent<Text>().text = "" + countdownValue;
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
                game_over = true;
                if (secondChanceUI != null)
                {
                    secondChanceUI.GetComponent<TryAgainController>().show();
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

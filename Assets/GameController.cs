using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {GameActivate,GamePaused ,Preparation,BeginWave, Waving,Action, EndWave, GameOver }

public class SoundToPlay {

    static GameObject soundObject;
    static AudioSource audioSource;
    static float sfx_volume = PlayerPrefs.GetFloat("sfx volume");
    static float music_volume = PlayerPrefs.GetFloat("music volume");
    static float master_volume = PlayerPrefs.GetFloat("master volume");
    public static void SetSoundToPlay(GameObject _soundObject)
    {
        soundObject = _soundObject;
        float master_volume = PlayerPrefs.GetFloat("master volume");                
        SetAllVolumes();
        SetGlobalVolume(master_volume);
    }
    public static void SetSoundToPlay(AudioSource _audioSource)
    {
        audioSource = _audioSource;
        float master_volume = PlayerPrefs.GetFloat("master volume");        
        SetAllVolumes();
        SetGlobalVolume(master_volume);
    }
    public static void SetAllVolumes()
    {
        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");
        master_volume = PlayerPrefs.GetFloat("master volume");
    }
    public static void SetGlobalVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
    public static void PlayMusic(GameObject _soundObject)
    {
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        src.volume = music_volume;
        MonoBehaviour.Instantiate(soundObject);
    }
    public static void PlayMusic(AudioSource _audioSource)
    {
        
        SetSoundToPlay(_audioSource);
        audioSource.volume = music_volume;
        audioSource.Play();
        
    }
    public static void PlaySfx(GameObject _soundObject)
    {
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        src.volume = sfx_volume;
        MonoBehaviour.Instantiate(soundObject);
    }
    public static void PlaySfx(AudioSource _audioSource)
    {
        SetSoundToPlay(_audioSource);
        audioSource.volume = sfx_volume;
        audioSource.Play();
        
    }
    public static void PlayAtLocation(GameObject soundObject, Vector3 position, Quaternion rotation)
    {
        MonoBehaviour.Instantiate(soundObject, position, rotation);
    }
    public static void PlayAtLocation(AudioSource audioSource, Vector3 position, Quaternion rotation )
    {
        audioSource.Play();
    }
}

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
        stats.AddBuiltTower(towerType);


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


}

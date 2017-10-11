﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {GameActivate,GamePaused ,Preparation,BeginWave, Waving,Action, EndWave, GameOver }

public class SoundToPlay {

    GameObject soundObject;
    AudioSource audioSource;
    public SoundToPlay(GameObject _soundObject)
    {
        soundObject = _soundObject;
        float master_volume = PlayerPrefs.GetFloat("master volume");
        SetMasterVolume(master_volume);
    }
    public SoundToPlay(AudioSource _audioSource)
    {
        audioSource = _audioSource;
        float master_volume = PlayerPrefs.GetFloat("master volume");
        SetMasterVolume(master_volume);
    }
    public void SetMasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
    public void PlayMusic()
    {
        if (audioSource)
        {
            audioSource.volume = GameController.music_volume;
            audioSource.Play();
        }
        else if (soundObject)
        {
            AudioSource src = soundObject.GetComponent<AudioSource>();
            src.volume = GameController.music_volume;
            MonoBehaviour.Instantiate(soundObject);
        } 
    }
    public void PlaySfx()
    {
        if (audioSource)
        {
            audioSource.volume = GameController.music_volume;
            audioSource.Play();
        }
        else if (soundObject)
        {
            AudioSource src = soundObject.GetComponent<AudioSource>();
            src.volume = GameController.sfx_volume;
            MonoBehaviour.Instantiate(soundObject);
        }
    }
    public void PlayAtLocation(Vector3 position, Quaternion rotation )
    {
        if (audioSource)
            audioSource.Play();
        else if (soundObject)
            MonoBehaviour.Instantiate(soundObject, position, rotation);
    }
}

public class GameController : MonoBehaviour {

    public delegate void GameStateChangedDelegate();
    public static GameStateChangedDelegate gamechangedDelegate;

    public delegate void EndWaveDelegate();
    public static EndWaveDelegate endWaveDelegate;

    [HideInInspector]
    public static GameState gameState;
    public bool game_over = false;

    public GameObject endWaveSound;
    GameObject victorySound;

    public GameObject gameStateUI;
    private Button startWaveButton;

    public float preparationTime = 30.0f;
    float countDown;
    public SteamStatsAndAchievements stats;
    public static float sfx_volume;
    public static float music_volume;
    public static float master_volume;

    // Use this for initialization
    void Start () {
        gamechangedDelegate+= evaluateGameStateChanged;
        gameState = GameState.Preparation;
        countDown = preparationTime;
        startWaveButton = gameStateUI.transform.Find("StartWave").gameObject.GetComponent<Button>();
        stats = GameObject.FindObjectOfType<SteamStatsAndAchievements>();

        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");
        master_volume = PlayerPrefs.GetFloat("master volume");


        AudioSource[] mySources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < mySources.Length; i++)
        {
            mySources[i].volume = sfx_volume;
            //Debug.Log("Found:  " + mySources[i].gameObject, mySources[i].gameObject);
        }

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
            
        }
        else if (gameState == GameState.Waving)
        {

        }
        else if (gameState == GameState.Action)
        {
        }
        else if (gameState == GameState.GameOver)
        {
            GetComponent<FastForward>().NormalSpeedOnClick();

            if (!game_over)
            {
                if (WaveSpawner.gainSecondChanceCounter >= 0)
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

            GetComponent<FastForward>().NormalSpeedOnClick();
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

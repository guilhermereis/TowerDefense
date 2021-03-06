﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class ConfigurationMenu : MonoBehaviour {

    public Slider[] volumeSliders;
    public Toggle fullscreenToggle;
    public Toggle tutorialToggle;
    public Dropdown resolutionDropDown;
    public Dropdown qualityDropDown;
    public GameObject quitConfirmationScreen;
    public GameObject resConfirmationScreen;
    public GameObject startOverConfirmationScreen;
    public GameObject creditsScreen;
    public AudioClip sound;

    private float sfx_volume;
    private float music_volume;
    private float master_volume;

    public static int previousResIndex;
    public static bool previousFullScreen;
    public static int activeScreenResIndex;
    public static int tutorialOn;
    public static bool activeFullScreen;

    /*Set the following to true everytime you want to force the resolution dropdown to not do
    anything when setting its value, and change only the Text and selected text:
    resForceValue = true;
    dropdown.value = newValue;*/
    private bool resForceValue = false;
    private bool blockQualitySettings = false;
    private bool blockFullScreenSound = false;
    private bool blockTutorialSound = false;

    CanvasGroup cg;

    private Resolution[] resolutions; //new Resolution[4];

    public delegate void SoundSliderChangedDelegate();
    public static SoundSliderChangedDelegate soundSliderDelegate;

    void Awake()
    {
        soundSliderDelegate += processSliderChange;
        ResolutionConfirmationScreenController.countDownFinished += onScreenResolutionCountDownFinished;
    }

    private void OnDestroy()
    {
        soundSliderDelegate -= processSliderChange;
        ResolutionConfirmationScreenController.countDownFinished -= onScreenResolutionCountDownFinished;
    }

    void Start()
    {

        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().playOnAwake = false;
        cg = GetComponent<CanvasGroup>();
        Hide();

        master_volume = PlayerPrefs.GetFloat("master volume");
        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");
        activeScreenResIndex = PlayerPrefs.GetInt("resolution index");
        tutorialOn = PlayerPrefs.GetInt("tutorial");

        volumeSliders[0].GetComponent<Slider>().value = master_volume; 
        volumeSliders[1].GetComponent<Slider>().value = sfx_volume; 
        volumeSliders[2].GetComponent<Slider>().value = music_volume;

        getScreenResolutions();
        //Any other settings you want to initialize...

        //toggle.onClick.AddListener(() => PlaySound());        
        fullscreenToggle.onValueChanged.AddListener((x) => Invoke("MyFunction", 0f));
    }
    public void MyFunction()
    {
        if(!blockFullScreenSound)
            SoundToPlay.PlaySfx(GetComponent<AudioSource>());

        blockFullScreenSound = false;
    }

    public void PlayTutoSound() {
        if (!blockTutorialSound)
            SoundToPlay.PlaySfx(GetComponent<AudioSource>());

        blockTutorialSound = false;
    }


    public void Hide()
    {
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
    }

    public void Show()
    {
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }

    public void Toggle()
    {
        if (cg.alpha == 1f)
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
        }
        else if (cg.alpha == 0f)
        {
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }
    }

    public void SaveAudioConf() {
        PlayerPrefs.SetFloat("master volume", volumeSliders[0].value);
        PlayerPrefs.SetFloat("sfx volume", volumeSliders[1].value);
        PlayerPrefs.SetFloat("music volume", volumeSliders[2].value);
        PlayerPrefs.Save();
    }

    public void Continue()
    {
        SaveAudioConf();
        IngameMenu igm = GameObject.Find("IngameMenu").GetComponent<IngameMenu>();
        igm.Hide();
    }
    
    public void OnSliderChanged()
    {
        SaveAudioConf();
        AudioListener.volume = volumeSliders[0].value;
        soundSliderDelegate();
    }

    public void processSliderChange() {
    }

    public void getScreenResolutions() {
        resolutions = Screen.resolutions;
        resolutionDropDown.options.Clear();

        string pattern = @" \d+Hz";
        string replacement = "";
        Regex rgx = new Regex(pattern);

        foreach (Resolution r in resolutions) {
            resolutionDropDown.options.Add(new Dropdown.OptionData(r.width +"x"+ r.height));
        }


        if (resolutions.Length > activeScreenResIndex) {
            resForceValue = true;
            resolutionDropDown.value = activeScreenResIndex;
            resForceValue = true;
            fullscreenToggle.isOn = Screen.fullScreen;
            if (tutorialToggle) {
                blockTutorialSound = true;
                tutorialToggle.isOn = tutorialOn == 1 ? true : false;
            }
                
            resolutionDropDown.transform.Find("Label").GetComponent<Text>().text = resolutions[activeScreenResIndex].width + "x" + resolutions[activeScreenResIndex].height;
        }
        blockQualitySettings = true;
        qualityDropDown.value = QualitySettings.GetQualityLevel() - 1;
        resForceValue = false;
    }

    public void OnScreenResSet(int index) {
        previousFullScreen = activeFullScreen;
        previousResIndex = activeScreenResIndex;
        activeScreenResIndex = index;
        activeFullScreen = fullscreenToggle.isOn;

        if (resolutions.Length > index) {
            Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullscreenToggle.isOn);
        }        
        if(!resForceValue)
            resConfirmationScreen.GetComponent<ResolutionConfirmationScreenController>().showAndStartCountDown();
        resForceValue = false;
    }

    public void onScreenResolutionCountDownFinished()
    {
        resConfirmationScreen.GetComponent<ResolutionConfirmationScreenController>().stopCountDown();
        resForceValue = true;
        Screen.SetResolution(resolutions[previousResIndex].width, resolutions[previousResIndex].height, previousFullScreen);
        resolutionDropDown.value = previousResIndex;
        fullscreenToggle.isOn = previousFullScreen;
    }

    public void screenResolutionSetConfirmed() {
        resConfirmationScreen.GetComponent<ResolutionConfirmationScreenController>().stopCountDown();
        PlayerPrefs.SetInt("Screenmanager Resolution Width", resolutions[activeScreenResIndex].width);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", resolutions[activeScreenResIndex].height);
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", (fullscreenToggle.isOn) ? 1 : 0);
        PlayerPrefs.SetInt("resolution index", activeScreenResIndex);
        PlayerPrefs.Save();
    }

    public void setQualitySettings(int index) {
        if (blockQualitySettings)
        {
            QualitySettings.SetQualityLevel(index + 1);
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        OnScreenResSet(resolutionDropDown.value);
    }

    public void SetTutorial(bool isTutorial)
    {
        PlayTutoSound();
        PlayerPrefs.SetInt("tutorial", isTutorial? 1 : 0);
        PlayerPrefs.Save();
    }

    public void QuitButtonPressed() {
        showQuitConfirmationScreen();
    }

    public void showQuitConfirmationScreen() {
        quitConfirmationScreen.GetComponent<CanvasGroup>().alpha = 1;
        quitConfirmationScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void hideQuitConfirmationScreen() {
        quitConfirmationScreen.GetComponent<CanvasGroup>().alpha = 0;
        quitConfirmationScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void showStartOverConfirmation() {
        startOverConfirmationScreen.GetComponent<CanvasGroup>().alpha = 1f;
        startOverConfirmationScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void confirmStartOver() {
        SceneManager.LoadScene("LoadingScene");
    }

    public void hideStartOver() {
        startOverConfirmationScreen.GetComponent<CanvasGroup>().alpha = 0f;
        startOverConfirmationScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void showCredits() {
        creditsScreen.GetComponent<CanvasGroup>().alpha = 1f;
        creditsScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void hideCredits() {
        creditsScreen.GetComponent<CanvasGroup>().alpha = 0f;
        creditsScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

}
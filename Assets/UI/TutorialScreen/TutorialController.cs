using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tutorialState {Welcome1,Castle2,CastleHealth3,Repair4,Gold5,
                            Landscape6,Shop7,WaveCounter8,GameOptions9,
                            Preparation10,Minimap11,CameraControls12,
                            SecondChance13,Finish14}

public class TutorialController : MonoBehaviour {

    public GameObject WelcomePanel;
    public GameObject CastlePanel;
    public Transform CastlePosition;
    public GameObject CastleHealthPanel;
    public GameObject RepairPanel;
    public GameObject GoldPanel;
    public GameObject ShopPanel;
    public GameObject WaveCounter;
    public GameObject GameOptionsPanel;
    public GameObject PreparationPanel;
    public Transform PreparationPanelLocation;
    public GameObject MinimapPanel;
    public GameObject CameraControlsPanel;
    public Transform CameraControlsPanelLocation;
    public GameObject SecondChancePanel;
    public GameObject FinishPanel;

    public GameObject SkipConfirmationPanel;
    public GameObject LandscapePanel;
    public Transform GrasslandsPosition;

    private GameObject currentShownStep;
    private tutorialState tutorialState;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            tutorialState = tutorialState.Welcome1;
            currentShownStep = WelcomePanel;
            showCurrentStepanel();
            GameObject HUD = GameObject.FindGameObjectWithTag("TutorialCanvas");
        }
        else {
            confirmSkip();
        }
    }


    public void showSkipConfirm() {
        SkipConfirmationPanel.GetComponent<CanvasGroup>().alpha = 1f;
        SkipConfirmationPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void confirmSkip() {
        PlayerPrefs.SetInt("tutorial", 0);
        PlayerPrefs.Save();
        GameController.GameStart();
        GameObject.Destroy(gameObject);
    }

    public void closeSkipConfirm()
    {
        SkipConfirmationPanel.GetComponent<CanvasGroup>().alpha = 0f;
        SkipConfirmationPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //FlowControl
    public void nextStep() {
        switch (tutorialState) {
            case tutorialState.Welcome1:
                switchTutorialState(tutorialState.Castle2);
                break;
            case tutorialState.Castle2:
                switchTutorialState(tutorialState.CastleHealth3);
                break;
            case tutorialState.CastleHealth3:
                switchTutorialState(tutorialState.Repair4);
                break;
            case tutorialState.Repair4:
                switchTutorialState(tutorialState.Gold5);
                break;
            case tutorialState.Gold5:
                switchTutorialState(tutorialState.Landscape6);
                break;
            case tutorialState.Landscape6:
                switchTutorialState(tutorialState.Shop7);
                break;
            case tutorialState.Shop7:
                switchTutorialState(tutorialState.WaveCounter8);
                break;
            case tutorialState.WaveCounter8:
                switchTutorialState(tutorialState.GameOptions9);
                break;
            case tutorialState.GameOptions9:
                switchTutorialState(tutorialState.Preparation10);
                break;
            case tutorialState.Preparation10:
                switchTutorialState(tutorialState.Minimap11);
                break;
            case tutorialState.Minimap11:
                switchTutorialState(tutorialState.CameraControls12);
                break;
            case tutorialState.CameraControls12:
                switchTutorialState(tutorialState.SecondChance13);
                break;
            case tutorialState.SecondChance13:
                switchTutorialState(tutorialState.Finish14);
                break;
            case tutorialState.Finish14:
                confirmSkip();
                break;
        }
    }
    public void previousStep() {
        switch (tutorialState)
        {
            case tutorialState.Welcome1:
                break;
            case tutorialState.Castle2:
                switchTutorialState(tutorialState.Welcome1);
                break;
            case tutorialState.CastleHealth3:
                switchTutorialState(tutorialState.Castle2);
                break;
            case tutorialState.Repair4:
                switchTutorialState(tutorialState.CastleHealth3);
                break;
            case tutorialState.Gold5:
                switchTutorialState(tutorialState.Repair4);
                break;
            case tutorialState.Landscape6:
                switchTutorialState(tutorialState.Gold5);
                break;
            case tutorialState.Shop7:
                switchTutorialState(tutorialState.Landscape6);
                break;
            case tutorialState.WaveCounter8:
                switchTutorialState(tutorialState.Shop7);
                break;
            case tutorialState.GameOptions9:
                switchTutorialState(tutorialState.WaveCounter8);
                break;
            case tutorialState.Preparation10:
                switchTutorialState(tutorialState.GameOptions9);
                break;
            case tutorialState.Minimap11:
                switchTutorialState(tutorialState.Preparation10);
                break;
            case tutorialState.CameraControls12:
                switchTutorialState(tutorialState.Minimap11);
                break;
            case tutorialState.SecondChance13:
                switchTutorialState(tutorialState.CameraControls12);
                break;
            case tutorialState.Finish14:
                switchTutorialState(tutorialState.SecondChance13);
                break;
        }
    }

    private void switchTutorialState(tutorialState newState) {
        currentShownStep.GetComponent<CanvasGroup>().alpha = 0;
        currentShownStep.GetComponent<CanvasGroup>().blocksRaycasts = false;
        tutorialState = newState;

        switch (newState) {
            case tutorialState.Welcome1:
                currentShownStep = WelcomePanel;
                break;
            case tutorialState.Castle2:
                currentShownStep = CastlePanel;
                break;
            case tutorialState.CastleHealth3:
                currentShownStep = CastleHealthPanel;
                break;
            case tutorialState.Repair4:
                currentShownStep = RepairPanel;
                break;
            case tutorialState.Gold5:
                currentShownStep = GoldPanel;
                break;
            case tutorialState.Landscape6:
                currentShownStep = LandscapePanel;
                break;
            case tutorialState.Shop7:
                currentShownStep = ShopPanel;
                break;
            case tutorialState.WaveCounter8:
                currentShownStep = WaveCounter;
                break;
            case tutorialState.GameOptions9:
                currentShownStep = GameOptionsPanel;
                break;
            case tutorialState.Preparation10:
                currentShownStep = PreparationPanel;
                break;
            case tutorialState.Minimap11:
                currentShownStep = MinimapPanel;
                break;
            case tutorialState.CameraControls12:
                currentShownStep = CameraControlsPanel;
                break;
            case tutorialState.SecondChance13 :
                currentShownStep = SecondChancePanel;
                break;
            case tutorialState.Finish14:
                currentShownStep = FinishPanel;
                break;
        }

        showCurrentStepanel();
    }

    private void showCurrentStepanel() {
        currentShownStep.GetComponent<CanvasGroup>().alpha = 1;
        currentShownStep.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (tutorialState == tutorialState.Castle2) {
            currentShownStep.transform.Find("Panel").transform.position = Camera.main.WorldToScreenPoint(CastlePosition.position);
        }
        if (tutorialState == tutorialState.Landscape6) {
            currentShownStep.transform.Find("Panel").transform.position = Camera.main.WorldToScreenPoint(GrasslandsPosition.position);
        }
        if (tutorialState == tutorialState.Preparation10 || tutorialState == tutorialState.Minimap11) {
            currentShownStep.transform.Find("Panel").transform.position = Camera.main.WorldToScreenPoint(PreparationPanelLocation.position);
        }
        if (tutorialState == tutorialState.CameraControls12)
        {
            currentShownStep.transform.Find("Panel").transform.position = Camera.main.WorldToScreenPoint(CameraControlsPanelLocation.position);
        }
    }
}

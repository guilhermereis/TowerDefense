using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tutorialState {Welcome1,Castle2,CastleHealth3,Repair4,Gold5,
                            Landscape6,Shop7,Mine8, WaveCounter9,GameOptions10,
                            Preparation11,Minimap12,CameraControls13,
                            SecondChance14,Finish15}

public class TutorialController : MonoBehaviour {

    public GameObject WelcomePanel;
    public GameObject CastlePanel;
    public Transform CastlePosition;
    public GameObject CastleHealthPanel;
    public GameObject RepairPanel;
    public GameObject GoldPanel;
    public GameObject ShopPanel;
    public GameObject MinesPanel;
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
                switchTutorialState(tutorialState.Mine8);
                break;
            case tutorialState.Mine8:
                switchTutorialState(tutorialState.WaveCounter9);
                break;
            case tutorialState.WaveCounter9:
                switchTutorialState(tutorialState.GameOptions10);
                break;
            case tutorialState.GameOptions10:
                switchTutorialState(tutorialState.Preparation11);
                break;
            case tutorialState.Preparation11:
                switchTutorialState(tutorialState.Minimap12);
                break;
            case tutorialState.Minimap12:
                switchTutorialState(tutorialState.CameraControls13);
                break;
            case tutorialState.CameraControls13:
                switchTutorialState(tutorialState.SecondChance14);
                break;
            case tutorialState.SecondChance14:
                switchTutorialState(tutorialState.Finish15);
                break;
            case tutorialState.Finish15:
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
            case tutorialState.Mine8:
                switchTutorialState(tutorialState.Shop7);
                break;
            case tutorialState.WaveCounter9:
                switchTutorialState(tutorialState.Mine8);
                break;
            case tutorialState.GameOptions10:
                switchTutorialState(tutorialState.WaveCounter9);
                break;
            case tutorialState.Preparation11:
                switchTutorialState(tutorialState.GameOptions10);
                break;
            case tutorialState.Minimap12:
                switchTutorialState(tutorialState.Preparation11);
                break;
            case tutorialState.CameraControls13:
                switchTutorialState(tutorialState.Minimap12);
                break;
            case tutorialState.SecondChance14:
                switchTutorialState(tutorialState.CameraControls13);
                break;
            case tutorialState.Finish15:
                switchTutorialState(tutorialState.SecondChance14);
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
            case tutorialState.Mine8:
                currentShownStep = MinesPanel;
                break;
            case tutorialState.WaveCounter9:
                currentShownStep = WaveCounter;
                break;
            case tutorialState.GameOptions10:
                currentShownStep = GameOptionsPanel;
                break;
            case tutorialState.Preparation11:
                currentShownStep = PreparationPanel;
                break;
            case tutorialState.Minimap12:
                currentShownStep = MinimapPanel;
                break;
            case tutorialState.CameraControls13:
                currentShownStep = CameraControlsPanel;
                break;
            case tutorialState.SecondChance14 :
                currentShownStep = SecondChancePanel;
                break;
            case tutorialState.Finish15:
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
        if (tutorialState == tutorialState.Preparation11 || tutorialState == tutorialState.Minimap12) {
            currentShownStep.transform.Find("Panel").transform.position = Camera.main.WorldToScreenPoint(PreparationPanelLocation.position);
        }
        if (tutorialState == tutorialState.CameraControls13)
        {
            currentShownStep.transform.Find("Panel").transform.position = Camera.main.WorldToScreenPoint(CameraControlsPanelLocation.position);
        }
    }
}

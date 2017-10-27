using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tutorialState {Welcome1,Castle2,CastleHealth3,Repair4,Gold5,
                            Landscape6,Shop7,WaveCounter8,GameOptions9,
                            Preparation10,Minimap11,CameraControls12,
                            SecondChance13,Finish14}

public class TutorialController : MonoBehaviour {

    public GameObject WelcomePanel;
    public GameObject SkipConfirmationPanel;

    private GameObject currentShownStep;
    private tutorialState tutorialState;

    public void showSkipConfirm() {
        SkipConfirmationPanel.GetComponent<CanvasGroup>().alpha = 1f;
        SkipConfirmationPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void confirmSkip() {
        GameController.GameStart();
        gameObject.SetActive(false);
    }

    public void closeSkipConfirm()
    {
        SkipConfirmationPanel.GetComponent<CanvasGroup>().alpha = 0f;
        SkipConfirmationPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //FlowControl
    void nextStep() {
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
                showEndDialog();
                break;
        }
    }
    void previousStep() {
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

    void showEndDialog() {

    }

    private void switchTutorialState(tutorialState newState) {
        currentShownStep.GetComponent<CanvasGroup>().alpha = 0;
        currentShownStep.GetComponent<CanvasGroup>().blocksRaycasts = false;

        switch (newState) {
            case tutorialState.Castle2:

                break;
        }

        showCurrentStepanel();
    }

    private void showCurrentStepanel() {
        currentShownStep.GetComponent<CanvasGroup>().alpha = 1;
        currentShownStep.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

	// Use this for initialization
	void Start () {
        tutorialState = tutorialState.Welcome1;
        currentShownStep = WelcomePanel;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

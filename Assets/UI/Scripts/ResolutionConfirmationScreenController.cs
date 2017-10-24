using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionConfirmationScreenController : MonoBehaviour {

    public Text shadow;
    public Text label;

    public delegate void CountDownFinishedDelegate();
    public static CountDownFinishedDelegate countDownFinished;

    private string baseText = "Do you want to keep your\nresolution settings?\nReverting in ";
    private CanvasGroup cg;
    private float counter = 5f;
    private float defaultTime = 5f;

    private bool isCountingDown = false;

    // Use this for initialization
    void Start() {
        countDownFinished += evaluateCountDownFinished;
        cg = GetComponent<CanvasGroup>();
    }

    public void stopCountDown()
    {
        isCountingDown = false;
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        counter = defaultTime;
    }

    public void showAndStartCountDown() {   
        isCountingDown = true;
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        counter = defaultTime;
    }

    void Update() {
        if (isCountingDown) {
            
            counter -= Time.unscaledDeltaTime;
            int timeRemaining = (int)Mathf.Ceil(counter);

            if (shadow && label)
            {
                shadow.text = baseText + timeRemaining + "...";
                label.text = baseText + timeRemaining + "...";
            }

            if (timeRemaining < 0f)
            {
                countDownFinished();
            }
        }
    }

    public void revertResolutionOnButtonClick() {
        countDownFinished();
    }

    public void evaluateCountDownFinished() {
        stopCountDown();
    }
}

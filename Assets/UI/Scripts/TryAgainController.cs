using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TryAgainController : MonoBehaviour {

    public GameObject hasSecondChanceScreen;
    public GameObject noSecondChanceScreen;
    public GameObject quitConfirmation;

    //HasSecondChance


    //NoSecondChance
    public Image AnkFillImage;
    public Text percentText;
    public Text percentTextShadow;

    private int targetPercent = 0;
    private float targetFill = 0f;

    // Use this for initialization

    public void show() {
        targetFill = 0.8f;
        //targetFill = (float)WaveSpawner.gainSecondChanceCounter / (float)WaveSpawner.secondChanceWaveCountTarget;
        targetPercent = (int)(targetFill * 100);

        if (targetFill >= 1f)
        {
            noSecondChanceScreen.SetActive(false);
            hasSecondChanceScreen.SetActive(true);
        }
        else {
            noSecondChanceScreen.SetActive(true);
            hasSecondChanceScreen.SetActive(false);
        }

        CanvasGroup cv = GetComponent<CanvasGroup>();
        cv.alpha = 1f;
        cv.blocksRaycasts = true;
    }

    public void hide() {
        CanvasGroup cv = GetComponent<CanvasGroup>();
        cv.alpha = 0f;
        cv.blocksRaycasts = false;
    }

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        if (noSecondChanceScreen.activeInHierarchy) {
            percentText.text = Mathf.CeilToInt(AnkFillImage.fillAmount * 100) + "%";
            percentTextShadow.text = percentText.text;
            AnkFillImage.fillAmount = Mathf.Lerp(AnkFillImage.fillAmount, targetFill, 0.1f);
        }
    }

    public void startOver() {
        SceneManager.LoadScene("LoadingScene");
    }

    public void quitToMainMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void useSecondChance() {

    }

    public void dontUseSecondChance() {

    }

}

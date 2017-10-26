using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainController : MonoBehaviour {

    public GameObject hasSecondChanceScreen;
    public GameObject noSecondChanceScreen;

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
            if (targetFill > AnkFillImage.fillAmount) {
                string currentText = percentText.text.Substring(0, percentText.text.Length);
                percentText.text = (Mathf.FloorToInt(Mathf.Lerp(float.Parse(currentText), targetPercent, 0.1f)) * 100) + "%";
                percentTextShadow.text = percentText.text;
                AnkFillImage.fillAmount = Mathf.Lerp(AnkFillImage.fillAmount, targetFill, 0.1f);
            }
        }
    }
}

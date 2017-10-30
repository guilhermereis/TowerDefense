using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningBoardController : MonoBehaviour {

    public WaveSpawner waveSpawner;
    private Animator anim;
    private int openedAtWave = 999999999;
    public GameObject attentionWarningSound;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void setWarningText(string text) {
        transform.Find("TextShadow").GetComponent<Text>().text = text;
        transform.Find("Text").GetComponent<Text>().text = text;
    }

    public void openWarningBoard() {
        SoundToPlay.PlaySfx(attentionWarningSound);
        openedAtWave = waveSpawner.waveNumber;
        anim.SetTrigger("Open");
    }

    public void closeWarningBoard() {
        openedAtWave = 999999999;
        anim.SetTrigger("Close");
    }

	// Stays Open for 1 wave
	void Update () {
        if (waveSpawner.waveNumber > openedAtWave)
        {
            closeWarningBoard();
        }
    }
}

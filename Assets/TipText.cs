using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipText : MonoBehaviour {

    private string[] tips = { "Don't mind taking some hits to save you some gold!","Don't forget to collect money from your mines", "Space your defenses", "Use Ice Towers to slow your enemies", "One mine takes 4 waves to pay for itself"};
    private Text tipText;
	// Use this for initialization
	void Start () {
        tipText = GetComponent<Text>();
        tipText.text += tips[Random.Range(0, tips.Length)];
	}
}

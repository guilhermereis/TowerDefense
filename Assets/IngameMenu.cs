using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenu : MonoBehaviour {
    
	void Start () {
        Hide();
    }

    public void Hide()
    {
        CanvasGroup cg = GameObject.Find("IngameMenu").GetComponent<CanvasGroup>();
        cg.alpha = 0f; //this makes everything transparent
        cg.blocksRaycasts = false; //this prevents the UI element to receive input events
    }
    public void Show()
    {
        CanvasGroup cg = GameObject.Find("IngameMenu").GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }
}

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
        gameObject.SetActive(false);
    }
    public void Show()
    {
        CanvasGroup cg = GameObject.Find("IngameMenu").GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
        gameObject.SetActive(true);
    }
    public void Toggle()
    {
        CanvasGroup cg = GameObject.Find("IngameMenu").GetComponent<CanvasGroup>();
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
}

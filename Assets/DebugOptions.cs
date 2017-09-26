using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOptions : MonoBehaviour
{

    WaveSpawner waveSpawner;
   

    void Start()
    {
        GameObject wsObject = GameObject.Find("WaveSpawner");
        if (wsObject != null)
        {
            waveSpawner = wsObject.GetComponent<WaveSpawner>();
        }
        
        Hide();
    }

    public void doDestroyAll()
    {
        waveSpawner.doDestroyAll();

    }
    public void doSaveAll()
    {
        waveSpawner.doSaveAll();
    }
    public void doLoadAll()
    {
        waveSpawner.doLoadAll();
        Hide();
    }
    public void Hide()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 0f; //this makes everything transparent
        cg.blocksRaycasts = false; //this prevents the UI element to receive input events
    }
    public void Show()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }
}

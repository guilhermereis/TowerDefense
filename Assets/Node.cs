﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject unit;


    public GameObject preview;

    private Renderer rend;
    private Renderer previewRenderer;
    private Color startColor;

    BuildManager buildManager;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseExit()
    {
        rend.material.color = startColor;
        Destroy(this.preview);
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
        {
            return;
        }

        rend.material.color = hoverColor;

        if (this.preview == null && this.unit == null)
        {
            buildManager.BuildPreviewOn(this);
            previewRenderer = preview.GetComponent<Renderer>();
            previewRenderer.material.color = Color.green;
        }
            
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (unit != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }
        Debug.Log("Gonna call !");
        buildManager.BuildUnitOn(this);

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    

    
}

using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject unit;


    private Renderer rend;
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

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
        {
            return;
        }

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

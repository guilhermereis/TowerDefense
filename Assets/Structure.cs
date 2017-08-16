using UnityEngine;
using UnityEngine.EventSystems;

public class Structure : MonoBehaviour
{

    private BuildManager buildManager;
    private Node nodeThatSitsOn;
    private Renderer rend;
    private Color startColor;
    public Color hoverColor;

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        buildManager.SelectStructure(this);

        return;

    }
    public void SetNodeThatSitsOn(Node _nodeThatSitsOn)
    {
        nodeThatSitsOn = _nodeThatSitsOn;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

}

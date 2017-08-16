using UnityEngine;

public class StructureUI : MonoBehaviour
{

    public GameObject ui;
    private Structure target;

    public void SetTarget(Structure _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}

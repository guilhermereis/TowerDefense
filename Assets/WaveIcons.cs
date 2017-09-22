using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveIcons : MonoBehaviour {

    public GameObject icon;
    public static List<GameObject> allIcons;

    public void addNewIcon()
    {
        //Transform newSelectionCube = 
        //allIcons.Add(Instantiate(icon, Vector2.zero, Quaternion.identity));
        //allIcons[allIcons.Count - 1].transform.parent = transform;
        GameObject _icon = Instantiate(icon, Vector2.zero, Quaternion.identity);
        _icon.transform.parent = transform;
        allIcons.Add(_icon);

    }
    public void removeIcon(int index)
    {
        allIcons[index].SetActive(false);
    }
    /*
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            addNewIcon();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            removeIcon(2);
        }
    }
    */
}

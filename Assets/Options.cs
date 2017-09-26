using UnityEngine;

public class Options : MonoBehaviour {

    protected BuildManager buildManager;
    protected GridMouse gridMouse;
    protected Shop shop;

    // Use this for initialization
    void Start () {
        buildManager = BuildManager.instance;
        gridMouse = GridMouse.instance;
        shop = Shop.instance;
    }
    
	
}

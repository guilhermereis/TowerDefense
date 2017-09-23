using UnityEngine;

[System.Serializable]
public class UnitBlueprint{

    public string name;
    public GameObject prefab;
    public int cost;
    public int sell_cost;
    public int upgrade_cost;
    public Vector2 position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int getRegularSellCost()
    {
        return sell_cost;
    }
}

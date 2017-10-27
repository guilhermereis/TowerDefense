using UnityEngine;

[System.Serializable]
public class UnitBlueprint{

    public string name;
    public GameObject prefab;
    public int cost;
    public int sell_cost;
    public int withInterest_sellcost;
    public int upgrade_cost;
    public Vector2 position;
    public int stored_x;
    public int stored_z;

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

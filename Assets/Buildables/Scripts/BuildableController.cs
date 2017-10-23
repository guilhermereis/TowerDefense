using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildType { tower1, tower2, tower3, towerIce, towerFire, mine, none }

public class BuildableController : MonoBehaviour {

	


	private float tileSize;
	private float defense;
	private float health { get; set; }
	private float maxHealth = 100;
	private bool isUpgradable;
    private int arrayListPosition;
    protected UnitBlueprint unitBlueprint;
    public BuildType buildType = BuildType.none;
    public GameObject buildSoundPrefab;

    public virtual int GetSellCostWithInterest()
    {
        return -1;
    }

	protected virtual void Awake()
	{
		health = maxHealth;
        arrayListPosition = -1;
       
    }

    private void Start()
    {
       
    }

    public void setArrayListPosition(int position) {
        arrayListPosition = position;
    }
    public int getArrayListPosition()
    {
        return arrayListPosition;
    }
    public void setUnitBlueprint(UnitBlueprint _unitBlueprint)
    {
        unitBlueprint = _unitBlueprint;
    }
    public UnitBlueprint getUnitBlueprint()
    {
        return unitBlueprint;
    }
    public float Defense
	{
		get
		{
			return defense;
		}

		set
		{
			defense = value;
		}
	}

	public float TileSize
	{
		get
		{
			return tileSize;
		}

		set
		{
			tileSize = value;
		}
	}

	public float Health
	{
		get
		{
			return health;
		}

		set
		{
			health = value;
		}
	}

	public bool IsUpgradable
	{
		get
		{
			return isUpgradable;
		}

		set
		{
			isUpgradable = value;
		}
	}

	
    public virtual void OnTriggerEnter(Collider other)
    {

    }
    public virtual void OnTriggerExit(Collider other)
    {

    }
}

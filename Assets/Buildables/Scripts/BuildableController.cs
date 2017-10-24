using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildType { tower1, tower2, tower3, towerIce, towerFire, mine, none }

public class BuildableController : MonoBehaviour {



    [Header("Build Effect")]
    public ParticleSystem buildSmokeEffectPrefab;
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

    public void BuildEffect()
    {
        SoundToPlay.PlaySfx(buildSoundPrefab);
        //Instantiate(buildSoundPrefab);
        buildSmokeEffectPrefab = GetComponentInChildren<ParticleSystem>();
        buildSmokeEffectPrefab.Play();
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
    public void setUnitType()
    {

    }
    public void setUnitBlueprintAndType(UnitBlueprint _unitBlueprint)
    {
        unitBlueprint = _unitBlueprint;

        string unitName = unitBlueprint.name;

        if (unitName == Shop.instance.towerLevel1.name)
        {
            buildType = BuildType.tower1;
        }
        else if (unitName == Shop.instance.towerLevel2.name)
        {
            buildType = BuildType.tower2;
        }
        else if (unitName == Shop.instance.towerLevel3.name)
        {
            buildType = BuildType.tower3;
        }
        else if (unitName == Shop.instance.towerSlow.name)
        {
            buildType = BuildType.towerIce;
        }
        else if (unitName == Shop.instance.towerTesla.name)
        {
            buildType = BuildType.towerFire;
        }
        else if (unitName == Shop.instance.miningCamp.name)
        {
            buildType = BuildType.mine;
        }
        GameController.AddBuiltTower(buildType);
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

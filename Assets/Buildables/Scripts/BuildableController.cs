﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableController : MonoBehaviour {

	public enum HealthState { good, damaged, destroyed}

	private float tileSize;
	private float defense;
	private float health { get; set; }
	private float maxHealth = 100;
	private bool isUpgradable;
    private int arrayListPosition;
    private UnitBlueprint unitBlueprint;
	public HealthState currentHealthState = HealthState.good;
    public GameObject buildSoundPrefab;


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

	public bool ChangeState(HealthState newState)
	{
		if (currentHealthState != newState)
		{
			currentHealthState = newState;
			OnChangeHealthState();
			return true;
		}
		else
			return false;
	}

	protected void OnChangeHealthState()
	{
		if(currentHealthState == HealthState.good)
		{

		}else if(currentHealthState == HealthState.damaged)
		{
			Debug.Log("Damaged");
		}else if(currentHealthState == HealthState.destroyed)
		{
			Debug.Log("Destroyed");
		}

	}

	public virtual bool Damage(float _damage)
	{
		health -= _damage;
		float fillAmount = health / maxHealth;
		if (fillAmount >= 0.2f && fillAmount <= 0.75f)
		{
			ChangeState(HealthState.damaged);
		}else if(fillAmount <= 0)
		{
			ChangeState(HealthState.destroyed);
			return true;
		}
		
		return false;
	}
    public virtual void OnTriggerEnter(Collider other)
    {

    }
    public virtual void OnTriggerExit(Collider other)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableController : MonoBehaviour {

	public enum HealthState { good, damaged, destroyed}

	private float tileSize;
	private float defense;
	private float health;
	private bool isUpgradable;
	public HealthState currentHealthState = HealthState.good;

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

		}else if(currentHealthState == HealthState.damaged)
		{

		}

	}
}

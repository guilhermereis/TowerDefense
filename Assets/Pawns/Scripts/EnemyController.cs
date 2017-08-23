using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PawnController {

	public int weight;

	// Use this for initialization
	void Start () {
		
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Update()
	{
		base.Update();
	}

	public override void OnBattle()
	{
		base.OnBattle();
	}

	public override void OnIdle()
	{
		base.OnIdle();
	}

	public override void OnMoving()
	{
		base.OnMoving();
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSoldierCharacter : PawnCharacter {



	public override void OnDying()
	{
		GetComponentInParent<SoldierCampController>().soldiersCount--;
		GetComponentInParent<SoldierCampController>().soldiersController.Remove(gameObject.GetComponent<SimpleSoldierController>());
		base.OnDying();
	}

}

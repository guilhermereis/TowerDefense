using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {

	public GameObject hpBar;
	public Canvas canvas;

	public GameObject SetMyHPBar(GameObject owner)
	{
		GameObject h = Instantiate<GameObject>(hpBar);

		canvas.transform.SetParent(canvas.transform, false);

		return h;
	}

}

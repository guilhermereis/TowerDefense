﻿using UnityEngine;
using UnityEngine.UI;

public class PawnHealthBarGUI : MonoBehaviour {

	public Texture2D tex = null;
	public GameObject hpBarPrefab;
	public Canvas canvas;
	private GameObject healthbar;

	public GameObject Healthbar
	{
		get
		{
			return healthbar;
		}

		set
		{
			healthbar = value;
		}
	}

	private void Start()
	{

		canvas = FindObjectOfType<Canvas>();

		Healthbar = Instantiate<GameObject>(hpBarPrefab);
		if(Healthbar != null)
		{
			Healthbar.transform.SetParent(canvas.transform, false);
			
			
			Healthbar.SetActive(false);
		}

		
	}

	private void Update()
	{
		
		Healthbar.transform.position = (Vector3.up * 2) + transform.position;// Camera.main.WorldToScreenPoint((Vector3.up * 3) + transform.position);
	}

	public void UpdateHealthBar(float health,float maxHealth)
	{
		Healthbar.SetActive(true);
		float updatedHealth = health / maxHealth ;
		
		Healthbar.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = updatedHealth;
		//Debug.Log(Healthbar.transform.GetChild(0).GetComponentInChildren<Image>().name);
		if (health <= 0)
		{
			Destroy(Healthbar);
		}
	}


	//private void OnGUI()
	//{
	//	Vector3 fixedTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	//	Vector3 scrPos = Camera.main.WorldToScreenPoint(fixedTransform);



	//	GUI.DrawTexture(
	//		new Rect(scrPos.x - 100/ 2.0f,
	//	Screen.height - scrPos.y/ 2.0f - 100, 30, 10),
	//	tex,
	//	ScaleMode.StretchToFill);

	//}

}
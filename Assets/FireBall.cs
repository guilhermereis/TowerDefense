using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	public float range;
	private Rigidbody rig;
	public float angle;
	public float speed;
	public GameObject explosionParticlePrefab;
	private Vector3 dir;
	private List<Rigidbody> targets;
	// Use this for initialization
	void Start () {
		range = 20f;
		rig = GetComponent<Rigidbody>();
		dir = ((transform.position + transform.forward * range) - transform.position);
		//Quaternion lookRotation = Quaternion.LookRotation(dir);
		targets = new List<Rigidbody>();
		//Vector3 rotation = lookRotation.eulerAngles;
		//Debug.Log(transform.rotation.eulerAngles);
		//transform.rotation = Quaternion.Euler(0f, 270, 0f);
	}

	// Update is called once per frame
	void Update()
	{
	

		rig.MovePosition(transform.position + dir.normalized * speed * Time.deltaTime);



	}

	private void OnCollisionEnter(Collision collision)
	{
		
		Instantiate(explosionParticlePrefab,collision.contacts[0].point, Quaternion.identity);
		Destroy(gameObject);

	}

	private void OnTriggerEnter(Collider other)
	{
		if(!other.isTrigger && other.gameObject.tag == "Enemy")
		{
			targets.Add(other.GetComponent<Rigidbody>());
		}
	}
}

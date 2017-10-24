using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrow : Arrow {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
        if (target != null)
        {
            Vector3 dir = target.GetComponent<CapsuleCollider>().center + target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //rig.MovePosition(dir.normalized * speed * Time.deltaTime);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        }
        if (target == null || target.GetComponent<PawnController>().currentState == PawnController.PawnState.Dead)
        {
            Destroy(gameObject);
            return;
        }
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            
            gameObject.GetComponent<AudioSource>().Play();
            Instantiate(bloodPrefabParticle, target.transform.position + target.GetComponent<CapsuleCollider>().center, Quaternion.Euler(new Vector3(-90, 0, 0)));
            GameObject ice = Instantiate(damagePrefabParticle, target.transform.position + target.GetComponent<CapsuleCollider>().center, Quaternion.Euler(new Vector3(-90, 0, 0)));
            target.GetComponent<PawnCharacter>().StartCoroutine("SlowTime", transform.parent.gameObject.GetComponent<TowerSlowController>().SlowAmount);
            ice.GetComponent<CrystalIce>().target = target;
            ice.transform.parent = target.transform;
            GameController.Freeze();

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();

            Destroy(gameObject,2);
            return;

        }
    }
}

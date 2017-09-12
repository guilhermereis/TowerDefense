using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamArrow : Arrow {

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
            if (other.gameObject.GetComponent<PawnCharacter>().Damage(attackPower))
            {
                GetComponentInParent<TowerController>().enemies.Remove(other.gameObject);
                other.gameObject.GetComponent<PawnCharacter>().OnDying();
            }

            Destroy(gameObject);
            return;
        }
    }
}

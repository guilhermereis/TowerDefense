﻿using System.Collections;
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
            speed = target.GetComponent<PawnController>().speed + 0.5f;
            Vector3 dir = target.GetComponent<CapsuleCollider>().center + target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //rig.MovePosition(dir.normalized * speed * Time.deltaTime);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        }
        if (target == null || target.GetComponent<PawnController>().currentState == PawnController.PawnState.Dead)
        {
            StopParticles();
            Destroy(gameObject,2f);
            return;
        }
    }

    public void StopParticles()
    {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.name == "fireball")
            {
                Destroy(ps);
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            else
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            bool hitted;
            bool killed;
            killed = other.gameObject.GetComponent  <PawnCharacter>().Damage(attackPower, out hitted,DamageType.Fire);
            if (hitted)
            {
                Instantiate(damagePrefabParticle, target.transform.position + target.GetComponent<CapsuleCollider>().center, Quaternion.Euler(new Vector3(-90, 0, 0)));
                StopParticles();
                GetComponentInChildren<SphereCollider>().enabled = false;

                if ( killed)
                    GetComponentInParent<TowerController>().enemies.Remove(other.gameObject);
            }

            Destroy(gameObject,2);
            return;
        }
    }
}

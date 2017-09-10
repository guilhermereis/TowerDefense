using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float attackPower = 1000;
    public int maxPiercing = 3;
    private int totalPierced = 0;
    public float speed = 2f;
    Vector3 initialPosition;
    public float maxDistance = 25;
	// Use this for initialization
	void Start() {
        initialPosition = transform.position;
        //Debug.Break();
        Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.GetType() == typeof(BoxCollider) && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PawnCharacter>().Damage(attackPower);
            totalPierced++;
        }
    }

}

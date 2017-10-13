using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalIce : MonoBehaviour {

    public GameObject brokenIce;
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            if (!target.GetComponent<PawnCharacter>().isSlow)
            {
                if(brokenIce!= null)
                {
                    //GameObject ice = Instantiate(brokenIce, transform.position, transform.rotation);
                    //ice.transform.parent = gameObject.transform.parent;
                    //ice.GetComponent<ExplodeIceEffect>().ExplodeIce();
                    Destroy(gameObject);

                }
            }

        }
    }
}

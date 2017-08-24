using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlacer : MonoBehaviour {

    public GameObject attachSocket;
    public GameObject characterWhoWields;

    private void Start(){
        if (attachSocket != null)
        {
            //transform.localScale = characterWhoWields.transform.localScale;
            transform.position = attachSocket.transform.position;
            transform.rotation = attachSocket.transform.rotation;
        }
    }
    // Update is called once per frame
    void Update () {
        if (attachSocket != null){
            transform.position = attachSocket.transform.position;
            transform.rotation = attachSocket.transform.rotation;
        }
        else Destroy(gameObject);
    }
}

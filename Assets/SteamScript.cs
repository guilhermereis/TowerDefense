using System.Collections;
using Steamworks;
using UnityEngine;

public class SteamScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
           
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log("name = " +name);
        }else
            Debug.Log("steamManager not initialized");

       // Debug.Break();
    }
	
	
}

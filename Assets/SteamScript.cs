using System.Collections;
#if DISABLESTEAMWORKS
using Steamworks;
using UnityEngine;

public class SteamScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
           
       

       // Debug.Break();
    }

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log("name = " + name);
            gameObject.AddComponent<SteamStatsAndAchievements>();
        }
        else
            Debug.Log("steamManager not initialized");
    }

}
#endif
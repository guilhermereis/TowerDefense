using UnityEngine.UI;
using UnityEngine;

public class SetMoney : MonoBehaviour {
    public Text scoreText;
    static int gscore = 100;
    void OnGui()
    {
        GUI.Label(new Rect(10,10,100,20), "Score: "+gscore);
    }

}

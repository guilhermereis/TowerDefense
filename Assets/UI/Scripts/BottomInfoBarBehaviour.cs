using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomInfoBarBehaviour : MonoBehaviour {

    public float slideSpeed = 5f;
    public float closedPosition = -50f;
    public float openPosition = 0f;
    private int selectionState = 0; //0 = Nothing selected, 1 = Has selection;

    public void setSelectionState(int SelectionState) {
        this.selectionState = SelectionState % 2;
    }

    public int getSelectionState() {
        return this.selectionState;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        Vector3 newPos;
        switch (selectionState) {
            
            case 0:
                newPos = new Vector3(pos.x,
                    Mathf.Lerp(pos.y, closedPosition, slideSpeed),
                    pos.z);
                transform.position = newPos;
                break;
            case 1:
                
                newPos = new Vector3(pos.x,
                    Mathf.Lerp(pos.y, openPosition, slideSpeed),
                    pos.z);
                transform.position = newPos;
                break;
            default:
                 newPos = new Vector3(pos.x,
                    Mathf.Lerp(pos.y, openPosition, slideSpeed),
                    pos.z);
                transform.position = newPos;
                break;
        }
	}
}

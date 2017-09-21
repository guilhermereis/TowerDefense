using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomInfoBarBehaviour : MonoBehaviour {

    public float slideSpeed = 5f;
    public float closedPosition = -50f;
    public float openPosition = 0f;
    private GameObject selectedObject;
    private int selectionState = 0; //0 = Nothing selected, 1 = Has selection;
    public GameObject AttackSpeedText;
    public GameObject AttackSpeedTextShadow;
    public GameObject DamageText;
    public GameObject DamageTextShadow;
    public GameObject SelectionName;
    public GameObject SelectionNameShadow;

    public void setSelectedUnit(GameObject selectedObject) {
        TowerController controller = selectedObject.GetComponent<TowerController>();
        if (controller)
        {
            AttackSpeedText.GetComponent<Text>().text = "" + controller.getFireRate();
            AttackSpeedTextShadow.GetComponent<Text>().text = "" + controller.getFireRate();

            DamageText.GetComponent<Text>().text = "" + controller.getAttackPower();
            DamageTextShadow.GetComponent<Text>().text = "" + controller.getAttackPower();

            switch (controller.name) {
                case "PrefabArcherTower1(Clone)":
                    SelectionName.GetComponent<Text>().text = "Tower Lvl1: ";
                    SelectionNameShadow.GetComponent<Text>().text = "Tower Lvl1: ";
                    break;
                case "PrefabArcherTower2(Clone)":
                    SelectionName.GetComponent<Text>().text = "Tower Lvl2: ";
                    SelectionNameShadow.GetComponent<Text>().text = "Tower Lvl2: ";
                    break;
                case "PrefabArcherTower3(Clone)":
                    SelectionName.GetComponent<Text>().text = "Tower Lvl3: ";
                    SelectionNameShadow.GetComponent<Text>().text = "Tower Lvl3: ";
                    break;
                case "PrefabArcherTower2Slow(Clone)":
                    SelectionName.GetComponent<Text>().text = "Icer Tower: ";
                    SelectionNameShadow.GetComponent<Text>().text = "Icer Tower: ";
                    break;
                case "PrefabArcherTower2Tesla(Clone)":
                    SelectionName.GetComponent<Text>().text = "Tesla Tower: ";
                    SelectionNameShadow.GetComponent<Text>().text = "Tesla Tower: ";
                    break;
            }
        }
        else {
            //Get Camp Controller
        }

    }

    public GameObject getSelectedUnit() {
        return selectedObject;
    }

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
                    Mathf.Lerp(pos.y, closedPosition, slideSpeed * Time.deltaTime) ,
                    pos.z);
                transform.position = newPos;
                break;
            case 1:
                
                newPos = new Vector3(pos.x,
                    Mathf.Lerp(pos.y, openPosition, slideSpeed * Time.deltaTime),
                    pos.z);
                transform.position = newPos;
                break;
            default:
                 newPos = new Vector3(pos.x,
                    Mathf.Lerp(pos.y, openPosition, slideSpeed * Time.deltaTime),
                    pos.z);
                transform.position = newPos;
                break;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomInfoBarBehaviour : MonoBehaviour {

    public float slideSpeed = 5f;
    public float closedPosition = -50f;
    public Transform closedPositionWaypoint;
    public float openPosition = 0f;
    private GameObject selectedObject;
    private int selectionState = 0; //0 = Nothing selected, 1 = Has selection;
    public GameObject AttackSpeedText;
    public GameObject AttackSpeedTextShadow;
    public GameObject DamageText;
    public GameObject DamageTextShadow;
    public GameObject SelectionName;
    public GameObject SelectionNameShadow;
    public GameObject AttadkDmgIcon;
    public Sprite IceIconVariation;
    public Sprite CurrentGoldVariation;
    public Color iceTextColorVariation;
    public Color mineColorVariation;
    public Color defaultTextColor;

    private Sprite defaultAttackDmgIcon;
    private Color defaultAttackDmgColor;

    public void setSelectedUnit(GameObject selectedObject) {
        TowerController controller = selectedObject.GetComponent<TowerController>();

        if (controller)
        {
            AttackSpeedText.GetComponent<Text>().text = "" + controller.getFireRate();
            AttackSpeedTextShadow.GetComponent<Text>().text = "" + controller.getFireRate();
            AttadkDmgIcon.GetComponent<Image>().overrideSprite = defaultAttackDmgIcon;
            DamageText.GetComponent<Text>().color = defaultAttackDmgColor;
            AttackSpeedText.GetComponent<Text>().color = defaultTextColor;
            SelectionName.transform.Find("AttackDamage").gameObject.SetActive(true);
            SelectionName.transform.Find("AttackSpeed").GetComponent<Image>().overrideSprite = null;

            string unitName = controller.getUnitBlueprint().name;


            if (unitName == Shop.instance.towerLevel1.name) {
                SelectionName.GetComponent<Text>().text = " Tower Lvl1: ";
                SelectionNameShadow.GetComponent<Text>().text = " Tower Lvl1: ";
                DamageText.GetComponent<Text>().text = "" + controller.getAttackPower();
                DamageTextShadow.GetComponent<Text>().text = "" + controller.getAttackPower();
                SelectionName.transform.Find("AttackSpeed").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S FIRERATE\nATTACKS / SECOND";
                SelectionName.transform.Find("AttackDamage").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S ATTACK DAMAGE";
            }
            if (unitName == Shop.instance.towerLevel2.name)
            {
                SelectionName.GetComponent<Text>().text = " Tower Lvl2: ";
                SelectionNameShadow.GetComponent<Text>().text = " Tower Lvl2: ";
                DamageText.GetComponent<Text>().text = "" + controller.getAttackPower();
                DamageTextShadow.GetComponent<Text>().text = "" + controller.getAttackPower();
                SelectionName.transform.Find("AttackSpeed").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S FIRERATE\nATTACKS / SECOND";
                SelectionName.transform.Find("AttackDamage").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S ATTACK DAMAGE";
            }
            if (unitName == Shop.instance.towerLevel3.name)
            {
                SelectionName.GetComponent<Text>().text = " Tower Lvl3: ";
                SelectionNameShadow.GetComponent<Text>().text = " Tower Lvl3: ";
                DamageText.GetComponent<Text>().text = "" + controller.getAttackPower();
                DamageTextShadow.GetComponent<Text>().text = "" + controller.getAttackPower();
                SelectionName.transform.Find("AttackSpeed").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S FIRERATE\nATTACKS / SECOND";
                SelectionName.transform.Find("AttackDamage").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S ATTACK DAMAGE";
            }
            if (unitName == Shop.instance.towerSlow.name)
            {
                SelectionName.GetComponent<Text>().text = " Icy Tower: ";
                SelectionNameShadow.GetComponent<Text>().text = " Icy Tower: ";
                TowerSlowController slowController = selectedObject.GetComponent<TowerSlowController>();
                DamageText.GetComponent<Text>().text = "" + slowController.SlowAmount + " s";
                DamageTextShadow.GetComponent<Text>().text = "" + slowController.SlowAmount + " s";
                AttadkDmgIcon.GetComponent<Image>().overrideSprite = IceIconVariation;
                DamageText.GetComponent<Text>().color = iceTextColorVariation;
                SelectionName.transform.Find("AttackSpeed").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S FIRERATE\nATTACKS / SECOND";
                SelectionName.transform.Find("AttackDamage").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S SLOW DURATION";

            }
            if (unitName == Shop.instance.towerTesla.name)
            {
                SelectionName.GetComponent<Text>().text = " Fire Tower: ";
                SelectionNameShadow.GetComponent<Text>().text = " Fire Tower: ";
                DamageText.GetComponent<Text>().text = "" + controller.getAttackPower();
                DamageTextShadow.GetComponent<Text>().text = "" + controller.getAttackPower();
                SelectionName.transform.Find("AttackSpeed").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S FIRERATE\nATTACKS / SECOND";
                SelectionName.transform.Find("AttackDamage").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "TOWER'S ATTACK DAMAGE";
            }
            
        }
        else {
            //Get Camp Controller
            MiningCampController mcontroller = selectedObject.GetComponent<MiningCampController>();
            SelectionName.GetComponent<Text>().text = " Mining Camp: ";
            SelectionNameShadow.GetComponent<Text>().text = " Mining Camp: ";
            SelectionName.transform.Find("AttackSpeed").transform.Find("TooltipPrefab").gameObject.GetComponentInChildren<TooltipController>().tooltipText = "Mine's gold per turn";
            SelectionName.transform.Find("AttackDamage").gameObject.SetActive(false);
            SelectionName.transform.Find("AttackSpeed").GetComponent<Image>().overrideSprite = CurrentGoldVariation;

            AttackSpeedText.GetComponent<Text>().text = "" + mcontroller.maxCapacity;
            AttackSpeedTextShadow.GetComponent<Text>().text = "" + mcontroller.maxCapacity;
            
            DamageText.GetComponent<Text>().text = "" + mcontroller.currentGold;
            DamageTextShadow.GetComponent<Text>().text = "" + mcontroller.currentGold;

            DamageText.GetComponent<Text>().color = mineColorVariation;
            AttackSpeedText.GetComponent<Text>().color = mineColorVariation;
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
        defaultAttackDmgIcon = AttadkDmgIcon.GetComponent<Image>().sprite;
        defaultAttackDmgColor = DamageText.GetComponent<Text>().color;
        defaultTextColor = AttackSpeedText.GetComponent<Text>().color;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        Vector3 newPos;
        switch (selectionState) {
            
            case 0:
                newPos = new Vector3(pos.x,
                    Mathf.Lerp(pos.y, closedPositionWaypoint.position.y, slideSpeed * Time.deltaTime) ,
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

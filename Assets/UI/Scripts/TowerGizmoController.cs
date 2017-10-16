using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerGizmoController : MonoBehaviour {
    private GameObject attackSpeedImage;
    private GameObject attackDamageImage;

    public Sprite AS1Sprite;
    public Sprite AS2Sprite;
    public Sprite AS3Sprite;

    public Sprite AD1Sprite;
    public Sprite AD2Sprite;
    public Sprite AD3Sprite;

    // Use this for initialization
    void Start() {
        attackSpeedImage = transform.Find("AS").gameObject;
        attackDamageImage = transform.Find("AD").gameObject;
    }

    public void forceStart() {
        attackSpeedImage = transform.Find("AS").gameObject;
        attackDamageImage = transform.Find("AD").gameObject;
    }

    public void setVisibility(bool isVisible) {
        gameObject.SetActive(isVisible);
    }

    public void setAttackSpeedLvl(int asLvl){
        Image img = attackSpeedImage.GetComponent<Image>();

        switch (asLvl) {
            case 0:
                img.transform.localScale = new Vector3(0f,0f,0f);
                break;
            case 1:
                img.overrideSprite = AS1Sprite;
                img.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 2:
                img.overrideSprite = AS2Sprite;
                img.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 3:
                img.overrideSprite = AS3Sprite;
                img.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            default:
                break;
        }
    }

    public void setAttackDamageLvl(int adLvl)
    {
        Image adImg = attackDamageImage.GetComponent<Image>();
        RectTransform rectTrans = adImg.rectTransform;

        switch (adLvl)
        {
            case 0:
                adImg.transform.localScale = new Vector3(0f, 0f, 0f);
                break;
            case 1:
                adImg.overrideSprite = AD1Sprite;
                adImg.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 2:
                adImg.overrideSprite = AD2Sprite;
                adImg.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 3:
                adImg.overrideSprite = AD3Sprite;
                adImg.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            default:
                break;
        }
    }
}

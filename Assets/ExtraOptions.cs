using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraOptions : MonoBehaviour {
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void upgradeAP1()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        tower.setAttackPower(150);

    }
    public void upgradeAP2()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        tower.setAttackPower(200);
    }
    public void upgradeAP3()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        tower.setAttackPower(250);
    }
    //----------------------------------------------------------
    public void upgradeFR1()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        tower.setFireRate(1.1f);
    }
    public void upgradeFR2()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        tower.setFireRate(1.5f);
    }
    public void upgradeFR3()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        tower.setFireRate(1.9f);
    }
}

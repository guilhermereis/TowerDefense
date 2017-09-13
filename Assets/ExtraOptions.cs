using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraOptions : MonoBehaviour {
    BuildManager buildManager;
    bool[] enabledAP;
    bool[] enabledFR;
    void Start()
    {
        buildManager = BuildManager.instance;
        enabledAP = new bool[3];
        enabledFR = new bool[3];
    }

    public void upgradeAP1()
    {
        if (!enabledAP[0] && !enabledAP[1] && !enabledAP[2])
        {
            TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
            tower.setAttackPower(150);
            enabledAP[0] = true;
        }
    }
    public void upgradeAP2()
    {
        if (!enabledAP[1] && !enabledAP[2])
        {
            TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
            tower.setAttackPower(200);
            enabledAP[1] = true;
        }
    }
    public void upgradeAP3()
    {
        if (!enabledAP[2])
        {
            TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
            tower.setAttackPower(250);
            enabledAP[2] = true;
        }
    }
    //----------------------------------------------------------
    public void upgradeFR1()
    {
        if (!enabledFR[0] && !enabledFR[1] && !enabledFR[2])
        {
            TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
            tower.setFireRate(1.1f);
            enabledFR[0] = true;
        }
    }
    public void upgradeFR2()
    {
        if (!enabledFR[1] && !enabledFR[2])
        {
            TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
            tower.setFireRate(1.5f);
            enabledFR[1] = true;
        }
    }
    public void upgradeFR3()
    {
        if (!enabledFR[2])
        {
            TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
            tower.setFireRate(1.9f);
            enabledFR[2] = true;
        }
    }
}

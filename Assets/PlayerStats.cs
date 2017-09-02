using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public int StartMoney;

    void Start()
    {
        AddMoney(StartMoney);
        Debug.Log("Set money to " + StartMoney);
    }
    public static void AddMoney(int amount)
    {
        Money += amount;
        GameObject.Find("MoneyText").GetComponent<Text>().text = "Money: "+ Money;
    }

}

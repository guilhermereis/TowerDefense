using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public int StartMoney = 1000;

    void Start()
    {
        AddMoney(StartMoney);
    }
    public static void AddMoney(int amount)
    {
        Money += amount;
        GameObject.Find("MoneyText").GetComponent<Text>().text = "Money: "+ Money;
    }

}

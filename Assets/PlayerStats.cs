using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    public static int Money;
    public int StartMoney;
    public Animator secondChanceAnimator;
    private GameObject secondChanceMeter;
    private GameObject hudCanvas;

    private float secondChanceFillTarget = 0f;

    void Start()
    {
        AddMoney(StartMoney);        

        hudCanvas = GameObject.FindGameObjectWithTag("HUD");
        secondChanceMeter = hudCanvas.transform.Find("Player Info").Find("GoldBarUnfilled").Find("GoldBarFilled").gameObject;
        secondChanceAnimator = hudCanvas.transform.Find("Player Info").Find("GoldBarUnfilled").Find("GoldBarFilled").GetComponent<Animator>();
    }

    private void Update()
    {
        secondChanceFillTarget = (float)WaveSpawner.gainSecondChanceCounter / (float)WaveSpawner.secondChanceWaveCountTarget;
        secondChanceMeter.GetComponent<Image>().fillAmount = Mathf.Lerp(secondChanceMeter.GetComponent<Image>().fillAmount, secondChanceFillTarget, 0.5f);

        if (secondChanceMeter.GetComponent<Image>().fillAmount >= 1)
        {
            if (secondChanceAnimator)
            {
                secondChanceAnimator.SetBool("IsFilled", true);
            }
        }
        else {
            if (secondChanceAnimator)
                secondChanceAnimator.SetBool("IsFilled", false);
        }
        
    }

    public static int AddMoney(int amount)
    {
        Money += amount;
        GameObject.Find("MoneyText").GetComponent<Text>().text = ""+Money;
        GameObject.Find("MoneyTextShadow").GetComponent<Text>().text = "" + Money;
        return amount;
    }
    public static void SetMoney(int amount)
    {
        Money = amount;
        GameObject.Find("MoneyText").GetComponent<Text>().text = "" + Money;
        GameObject.Find("MoneyTextShadow").GetComponent<Text>().text = "" + Money;
    }


}

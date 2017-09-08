using UnityEngine;
using UnityEngine.UI;

public class PawnHealthBarGUI : MonoBehaviour {

	public Texture2D tex = null;
	public GameObject hpBarPrefab;
	public Canvas canvas;
	private GameObject healthbar;

	public GameObject Healthbar
	{
		get
		{
			return healthbar;
		}

		set
		{
			healthbar = value;
		}
	}

    public Vector3 heightOffset;


    private void Start()
	{

        //getting the height 
       
        Canvas[] canvasArray = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvasArray.Length; i++)
        {
            if (canvasArray[i].tag.Equals("canvas"))
            {
               
                canvas = canvasArray[i];
                break;
            }
        }

        Healthbar = Instantiate<GameObject>(hpBarPrefab);
        if (Healthbar != null)
        {
            Healthbar.transform.SetParent(canvas.transform, false);
            Healthbar.SetActive(false);
        }
        else
            Debug.Log("f");

		
	}

	private void Update()
	{
		
        if(Healthbar != null)
        {
		    Healthbar.transform.position = heightOffset + transform.position;// Camera.main.WorldToScreenPoint((Vector3.up * 3) + transform.position);
            Healthbar.transform.rotation = Camera.main.transform.rotation;
        }
	}

	public void UpdateHealthBar(float health,float maxHealth)
	{

       
        if (gameObject != null)
        {
		    Healthbar.SetActive(true);
		    float updatedHealth = health / maxHealth;
            if (updatedHealth >= 0)
                Healthbar.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = updatedHealth;
            else
            {
                Healthbar.SetActive(false);
                this.enabled = false;
            }
            //Debug.Log(Healthbar.transform.GetChild(0).GetComponentInChildren<Image>().name);


        }

    

	}


	//private void OnGUI()
	//{
	//	Vector3 fixedTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	//	Vector3 scrPos = Camera.main.WorldToScreenPoint(fixedTransform);



	//	GUI.DrawTexture(
	//		new Rect(scrPos.x - 100/ 2.0f,
	//	Screen.height - scrPos.y/ 2.0f - 100, 30, 10),
	//	tex,
	//	ScaleMode.StretchToFill);

	//}

}

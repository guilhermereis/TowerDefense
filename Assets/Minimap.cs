using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public GameObject square;
    public static List<GameObject> allSquares;
    public float scale;
    public static List<GameObject> monsterBatch;

    // Use this for initialization
    void Start () {
       // UpdateMap();
        allSquares = new List<GameObject>();
        scale = GetComponent<RectTransform>().rect.width / 2 / 33f;
    }
    public void addNewSquare()
    {
        allSquares.Add(Instantiate(square, transform));
        allSquares[allSquares.Count-1].transform.parent = transform;
        allSquares[allSquares.Count - 1].SetActive(false);
    }
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
       Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }

    public void UpdateMonsterBatch()
    {
        monsterBatch = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().monsterBatch;
        addNewSquare();
    }

    public void ClearMonsterBatch() {
        allSquares.Clear();
    }

	// Update is called once per frame
	void Update () {
        if (monsterBatch != null)
        {
            for (int i = 0; i < monsterBatch.Count; i++)
            {

                if (monsterBatch != null && monsterBatch[i] != null)
                {
                    if (monsterBatch[i].GetComponent<PawnCharacter>().isDead)
                    {
                        allSquares[i].SetActive(false);
                    }
                    else
                    {
                        allSquares[i].SetActive(true);
                        Vector2 monsterWorldPosition = new Vector2(monsterBatch[i].transform.position.x, monsterBatch[i].transform.position.z);
                        Vector2 monsterPosition = monsterWorldPosition * scale;
                        allSquares[i].GetComponent<RectTransform>().localPosition = monsterPosition;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour {

	public static int Wanderer = 1;
	public static int Warrior = 2;
	public static int Destroyer = 3;

	private void Start()
	{
		int[] wave = getCombinaton(5,10);
		for (int i = 0; i < wave.Length; i++)
		{
			Debug.Log(wave[i]);
		}
	}

	int[] getCombinaton(int monsters, int score)
	{
		int i = 0;
		int sum = 0;
		int[] monstersWave = new int[monsters];
		while(i < monsters)
		{
			int m = Mathf.RoundToInt( Random.Range(1f, 3f));


			if( sum + m <= score)
			{
				monstersWave[i] = m;
				sum += m;
				i++;
			}
			


		}

		return monstersWave;
	}
	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave  {

    //wanderer 1
    //warrior 2
    //bomber 3
    //4 great wanderer
    //5 great warrior
    //6 great bomber

	public int totalScore;
	public int numberOfMonsters;
	public int[] combination;

    public Wave(int _totalScore, int _numberOfMonsters)
    {
        totalScore = _totalScore;
        numberOfMonsters = _numberOfMonsters;
    }

    public int[] BomberWave(int monstersWave)
    {
        int[] bombers = new int[monstersWave];
        for (int i = 0; i < bombers.Length; i++)
        {
            bombers[i] = 3;
        }

        return bombers;
    }

    public int[] GetCombinaton()
    {
        int i = 0;
        int sum = 0;
        int[] monstersWave = new int[numberOfMonsters];
        

        while (i < numberOfMonsters)
        {
            int m = Mathf.RoundToInt(Random.Range(1f, 6f));

            if (sum + m <= totalScore)
            {
                monstersWave[i] = m;
                sum += m;
                i++;
            }
            else
            {
                if (i < numberOfMonsters)
                {
                    sum = 0;
                    i = 0;
                    //for (int j = i; j < numberOfMonsters; j++)
                    //{
                    //    monstersWave[i] = 1;
                    //    i++;
                    //}


                }
            }

        }

        return monstersWave;
    }


}

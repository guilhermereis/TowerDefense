using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave  {


	public int totalScore;
	public int numberOfMonsters;
	public int[] combination;

    public Wave(int _totalScore, int _numberOfMonsters)
    {
        totalScore = _totalScore;
        numberOfMonsters = _numberOfMonsters;
    }


    public int[] GetCombinaton()
    {
        int i = 0;
        int sum = 0;
        int[] monstersWave = new int[numberOfMonsters];
        

        while (i < numberOfMonsters)
        {
            int m = Mathf.RoundToInt(Random.Range(1f, 3f));

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

                }
            }

        }

        return monstersWave;
    }


}

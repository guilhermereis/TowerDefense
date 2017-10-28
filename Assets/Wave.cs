using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Wave  {

    //1 wanderer 1
    //2 warrior 2
    //3 bomber 3
    //4 great wanderer
    //5 great warrior
    //6 great bomber
    //7 king
    //8 chief wanderer
    //9 chief warrior
    //10 chief bomber
    //11 chief king
    //12 lord wanderer
    //13 lord warrior
    //14 lord bomber
    //15 lord king 
    //16 master wanderer
    //17 master warrior
    //18 master bomber
    //19 master king
    //20 great king
    public int[] mForce = { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6 };
	public int totalScore;
	public int numberOfMonsters;
	public int[] combination;
    public float alpha;

    public int currentMilestone;
    
    public Wave(float _alpha, int milestone, int _numberOfMonsters)
    {
        currentMilestone = milestone;
        numberOfMonsters = _numberOfMonsters;
        alpha = _alpha;
    }


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

    public float EvaluateCurveFunction(float wave)
    {
        float y = Mathf.Log(5 * wave + 1, 10) * 2.55f;
        return y;
    }

    public int[] GetCombinaton()
    {
       
        int currentMilestoneLenght = 0;
        int[] monstersWave;
        float percentageOfMonstersNextMilestone = EvaluateCurveFunction(alpha) /2;
        int totalMonstersNextMilestone = Mathf.RoundToInt( percentageOfMonstersNextMilestone);

        Debug.Log(currentMilestone + "cm");
        
           

       
        //int thisMilestone = c;

        if(percentageOfMonstersNextMilestone == 0.0f)
        {
            currentMilestoneLenght = WaveSpawner.combinations[currentMilestone ].combination.Length;
            if(WaveSpawner.combinations[currentMilestone].special!= null)
            {
                int j = 0;
                monstersWave = new int[currentMilestoneLenght + WaveSpawner.combinations[currentMilestone].special.Length];
                for (int i = 0; i < WaveSpawner.combinations[currentMilestone].combination.Length; i++)
                {
                    monstersWave[i] = WaveSpawner.combinations[currentMilestone].combination[i];
                    j = i;
                }

                for (int i = j+1 , x =0 ; i < monstersWave.Length; i++,x++)
                {
                    monstersWave[i] = WaveSpawner.combinations[currentMilestone].special[x];
                }

            }else
                monstersWave = WaveSpawner.combinations[currentMilestone].combination;
        }
        else
        {

            int count = 0;
            currentMilestoneLenght = WaveSpawner.combinations[currentMilestone].combination.Length;

            if (currentMilestone + 1 < WaveSpawner.combinations.Length)
            {
                //% de 1 e % do outro
                int nextMilestonCombination = WaveSpawner.combinations[currentMilestone+1].combination.Length;
                
                //int monstersOfCurrentMS = Mathf.RoundToInt((1 - c) * (lenght + lenght2));

                int monstersOfCurrentMS = Mathf.RoundToInt((1 - percentageOfMonstersNextMilestone) * currentMilestoneLenght);
                int monstersOfNextMS = Mathf.RoundToInt(percentageOfMonstersNextMilestone * nextMilestonCombination);
                int totalMonsters = monstersOfCurrentMS + monstersOfNextMS;
                monstersWave = new int[totalMonsters];
                while (count < monstersOfCurrentMS)
                {
                    monstersWave[count] = WaveSpawner.combinations[currentMilestone].combination[Random.Range(0, currentMilestoneLenght - 1)];
                    count++;
                }
                //count = 0;

                while (count < totalMonsters)
                {
                    monstersWave[count] = WaveSpawner.combinations[currentMilestone + 1].combination[Random.Range(0, nextMilestonCombination - 1)];
                    count++;
                }

            }
            else
            {
                monstersWave = new int[currentMilestoneLenght + WaveSpawner.repetition++];


                for (int k = 0; k < monstersWave.Length; k++)
                {
                    if (k < WaveSpawner.combinations[currentMilestone].combination.Length)
                        monstersWave[k] = WaveSpawner.combinations[currentMilestone].combination[k];
                    else
                        monstersWave[k] = 6;
                }
            }
        }

        
        

        return monstersWave;
    }


}

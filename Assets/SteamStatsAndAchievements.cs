using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Steamworks;
using UnityEngine;

public class SteamStatsAndAchievements : MonoBehaviour {

    private enum Achievement: int
    {
        ACH_WIN_10_WAVES,
        ACH_WIN_50_WAVES,
        ACH_WIN_100_WAVES,
        ACH_BUILD_10_TOWERS,
        ACH_BUILD_50_TOWERS,
        ACH_BUILD_100_TOWERS,
        ACH_BUILD_TOWER_3,

    };

    

    private Achievement_t[] achievements = new Achievement_t[] {
        new Achievement_t(Achievement.ACH_WIN_10_WAVES,"Defeated 10 waves",""),
        new Achievement_t(Achievement.ACH_WIN_50_WAVES,"Defeated 50 waves",""),
        new Achievement_t(Achievement.ACH_WIN_100_WAVES,"Defeated 100 waves",""),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWERS,"Build 10 towers",""),
        new Achievement_t(Achievement.ACH_BUILD_50_TOWERS,"Build 50 towers",""),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWERS,"Build 100 towers",""),
        new Achievement_t(Achievement.ACH_BUILD_TOWER_3,"Build tower level 3",""),
    };

    //our gameID
    private CGameID gameID;

    bool onPlayScene;
    //storestats this frame
    private bool isStoreStats;

    //variables responsable for get info from steam
    private bool isRequestedStats;
    private bool isStatsvalid;

    //-----------------------------------------------------------------------------
    // Current Data
    //-----------------------------------------------------------------------------
    private int c_numberOfWave;
    private int c_numberOfTowers;
    private int c_numberOftower1;
    private int c_numberOftower2;
    private int c_numberOftower3;
    private int c_numberOfIceTower;
    private int c_numberOfFireTower;
    private int c_numberOfMine;
    private int c_numberOfMonstersKilled;
    private int c_numberOfKingKilled;
    private int c_numberOfWandererKilled;
    private int c_numberOfWarriorKilled;
    private int c_numberOfBomberKilled;
    private int c_numberOfFrozen;
    private int c_numberOfBurnt;
    private int c_numberOfDamage;
    private int c_moneyCollected;
    private int c_moneyRaised = 0;
    private int c_moneySpent = 0;
    private int c_unlockedLane2;
    private int c_unlockedLane3;
    private int c_unlockedLane4;


    private bool isLevel3Built;

    //-----------------------------------------------------------------------------
    // Persistent Data
    //-----------------------------------------------------------------------------
    private int p_totalWaves;
    private int p_totalDefeats;
    private int p_totalTowerBuilt;
    private int p_totalKingKilled;
    private int p_totalMineBuilt;
    private int p_totalTryAgain;
    private int p_unlockedLane2;
    private int p_unlockedLane3;
    private int p_unlockedLane4;
    private int p_totalDamage;
    private int p_totalEnemyFrozen;
    private int p_totalEnemyBurnt;
    private int p_moneyCollected;
    private int p_totalMonsterKilled;

    protected string leaderboardName = "Defeated Waves";

    //callbacks
    protected Callback<UserAchievementStored_t> userAchievementsStored;
    protected Callback<UserStatsReceived_t> userStatsReceived;
    protected Callback<UserStatsStored_t> userStatsStored;
    //callresults
    protected CallResult<LeaderboardScoreUploaded_t> leaderboardScoreUploaded;
    protected CallResult<LeaderboardFindResult_t> findLeaderBoard;


    //-----------------------------------------------------------------------------
    // Set total number of builds built
    //-----------------------------------------------------------------------------

    #region Builds
    public void AddBuiltTower(BuildType tower)
    {
        if (!SteamManager.Initialized)
            return;
        
        c_numberOfTowers ++;

        switch (tower)
        {
            case BuildType.tower1:
                AddTower();
                break;
            case BuildType.tower2:
                AddTower2();
                break;
            case BuildType.tower3:
                AddTower3();
                break;
            case BuildType.towerIce:
                AddIceTowers();
                break;
            case BuildType.towerFire:
                AddFireTowers();
                break;
            default:
                break;
        }


    }

    public void AddIceTowers()
    {
        c_numberOfIceTower++;
    }

    public void AddFireTowers()
    {
        c_numberOfFireTower++;
    }

    public void AddTower()
    {
        c_numberOftower1++;
    }

    public void AddTower2()
    {
        c_numberOftower2++;
    }

    public void AddTower3()
    {
        c_numberOftower3++;
    }

    public void AddMine()
    {
        c_numberOfMine++;
    }
    #endregion


    #region Monsters
    public void AddMonstersKilled(PawnType monster)
    {
        if (!SteamManager.Initialized)
            return;

        c_numberOfMonstersKilled++;
        switch (monster)
        {
            case PawnType.Bomber:
                AddBomber();
                break;
            case PawnType.King:
                AddKing();
                break;
            case PawnType.Wanderer:
                AddWanderer();
                break;
            case PawnType.Warrior:
                AddWarrior();
                break;
            default:
                break;

        }
    }

    private void AddBomber()
    {
        c_numberOfBomberKilled++;
    }

    private void AddWarrior()
    {
        c_numberOfWarriorKilled++;
    }
    private void AddKing()
    {
        c_numberOfKingKilled++;
    }
    private void AddWanderer()
    {
        c_numberOfWandererKilled++;
    }

    #endregion

    public void AddMoneyCollected(int gold, bool wasCollected)
    {
        c_moneyRaised += gold;
        if (wasCollected)
            c_moneyCollected += gold;
    }

    public void SpendMoney(int gold)
    {
        c_moneySpent += gold;
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnChangedLevel;

        if (!SteamManager.Initialized)
            return;

        //GameController.gamechangedDelegate += OnGameChanged;
        gameID = new CGameID(SteamUtils.GetAppID());

        userAchievementsStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
        userStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        userStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);

        findLeaderBoard = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFound);
        leaderboardScoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUpdated);

        isRequestedStats = false;
        isStatsvalid = false;


    }

    public void OnChangedLevel(Scene scene, LoadSceneMode mode)
    {
       
        if (scene.name == "MainScene")
        {
            Debug.Log(scene.name);
            onPlayScene = true;
           
        }
    }

   
    private void OnLeaderboardScoreUpdated(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bSuccess == 1 && !bIOFailure)
        {
            if(pCallback.m_bScoreChanged == 1)
            {
                Debug.Log("NEW RECORD");
            }
        }
    }

    private void OnLeaderboardFound(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        int[] scoreDetails;
        if(pCallback.m_bLeaderboardFound == 1 && !bIOFailure)
        {
           
            scoreDetails = new int[1];
            scoreDetails[0] = 1;
            SteamAPICall_t uHandle = SteamUserStats.UploadLeaderboardScore(pCallback.m_hSteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, p_totalWaves, scoreDetails, 1);
            leaderboardScoreUploaded.Set(uHandle);

        }
    }

    private void OnUserStatsStored(UserStatsStored_t pCallback)
    {

        if (!SteamManager.Initialized)
            return;

        if ((ulong)gameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("StoreStats sucess");
            }else if (pCallback.m_eResult == EResult.k_EResultInvalidParam)
            {
                Debug.Log("Some Failed to validade");

                UserStatsReceived_t callback = new UserStatsReceived_t();
                callback.m_eResult = EResult.k_EResultOK;
                callback.m_nGameID = (ulong)gameID;
                OnUserStatsReceived(callback);
            }
        }
        else
        {
            Debug.Log("StoreStats failed");
        }
    }

    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if (!SteamManager.Initialized)
            return;

        if((ulong)gameID == pCallback.m_nGameID)
        {
            if(EResult.k_EResultOK == pCallback.m_eResult)
            {
                isStatsvalid = true;

                foreach(Achievement_t achievement in achievements)
                {
                    bool result = SteamUserStats.GetAchievement(achievement.achievementID.ToString(), out achievement.isAchieved);
                    if (result)
                    {
                        achievement.name = SteamUserStats.GetAchievementDisplayAttribute(achievement.achievementID.ToString(), "name");
                        achievement.desc = SteamUserStats.GetAchievementDisplayAttribute(achievement.achievementID.ToString(), "desc");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                }

                //-------------------------------
                // Getting stats from steam
                //-------------------------------
                SteamUserStats.GetStat("totalNumWaves", out p_totalWaves);
                SteamUserStats.GetStat("totalNumDefeats", out p_totalDefeats);
                SteamUserStats.GetStat("totalNumTower", out p_totalTowerBuilt);
                SteamUserStats.GetStat("totalNumKingKilled", out p_totalKingKilled);
                SteamUserStats.GetStat("totalNumMineBuilt", out p_totalMineBuilt);
                SteamUserStats.GetStat("totalNumTryAgain", out p_totalTryAgain);
                SteamUserStats.GetStat("totalNumDamage", out p_totalDamage);
                SteamUserStats.GetStat("totalNumEnemyFrozen", out p_totalEnemyFrozen);
                SteamUserStats.GetStat("totalNumEnemyBurnt", out p_totalEnemyBurnt);
                SteamUserStats.GetStat("totalNumMonsterKilled", out p_totalMonsterKilled);
                SteamUserStats.GetStat("unlockedLane2", out p_unlockedLane2 );
                SteamUserStats.GetStat("unlockedLane3", out p_unlockedLane3);
                SteamUserStats.GetStat("unlockedLane4", out p_unlockedLane4);
                SteamUserStats.GetStat("totalNumMoneyCollected", out p_moneyCollected);


            }
            else
            {
                Debug.Log("Request Failed");
            }
        }
    }

    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        if (!SteamManager.Initialized)
            return;

        if ((ulong)gameID == pCallback.m_nGameID)
        {
            if( pCallback.m_nMaxProgress == 0)
            {
                Debug.Log("Achievement Unlocked");
            }
            else
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress +","+pCallback.m_nMaxProgress+ ")");
            }
        }
    }
    public void Render()
    {
        if (!SteamManager.Initialized)
        {
            GUILayout.Label("Steamworks not Initialized");
            return;
        }

        
        GUILayout.Label("NumWaves: " + p_totalWaves);
        GUILayout.Label("NumDefeats: " + p_totalDefeats);
        

        GUILayout.BeginArea(new Rect(Screen.width - 300, 0, 300, 800));
        foreach (Achievement_t ach in achievements)
        {
            GUILayout.Label(ach.achievementID.ToString());
            GUILayout.Label(ach.name + " - " + ach.desc);
            GUILayout.Label("Achieved: " + ach.isAchieved);
            GUILayout.Space(20);
        }

        // FOR TESTING PURPOSES ONLY!
        if (GUILayout.Button("RESET STATS AND ACHIEVEMENTS"))
        {
            SteamUserStats.ResetAllStats(true);
            SteamUserStats.RequestCurrentStats();
            //OnGameStateChange(EClientGameState.k_EClientGameActive);
        }
        GUILayout.EndArea();
    }

    	
    public void OnGameChanged()
    {
        Debug.Log("GameChanged");
        if (GameController.gameState == GameState.GameActivate)
        {
            c_numberOfTowers = 0;
            c_numberOfWave = 0;
        }else if (GameController.gameState == GameState.EndWave)
        {
            p_totalWaves++;
            Debug.Log("total waves " +p_totalWaves);
            isStoreStats = true;
            
        }else if(GameController.gameState == GameState.GameOver)
        {
            p_totalDefeats++;
            isStoreStats = true;
            //getting leaderboard;
            SteamAPICall_t fHandle =  SteamUserStats.FindLeaderboard(leaderboardName);
            findLeaderBoard.Set(fHandle);

        }
    }

	// Update is called once per frame
	void Update () {
        if (!SteamManager.Initialized)
            return;

        if (onPlayScene)
        {
            if (GameController.gamechangedDelegate != null)
            {
                GameController.gamechangedDelegate += OnGameChanged;
                Debug.Log("got delegate");
            }
            onPlayScene = false;

        }

        if (!isRequestedStats)
        {

            if (!SteamManager.Initialized)
            {
                isRequestedStats = true;
                return;
            }

            bool isSuccess = SteamUserStats.RequestCurrentStats();
            isRequestedStats = isSuccess;
        }


        if (!isStatsvalid)
            return;


        foreach(Achievement_t achievement in achievements)
        {
            if (achievement.isAchieved)
                return;

            switch (achievement.achievementID)
            {
                case Achievement.ACH_BUILD_10_TOWERS:
                    
                    if (c_numberOfTowers >= 10)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
            }
        }

        //store stats in the steam database if necessary.
        SteamUserStats.SetStat("totalNumWaves", p_totalWaves);
        SteamUserStats.SetStat("totalNumDefeats", p_totalDefeats);
        //SteamUserStats.UploadLeaderboardScore()
    }

    private void UnlockAchievement(Achievement_t achievement)
    {
        achievement.isAchieved = true;

        SteamUserStats.SetAchievement(achievement.achievementID.ToString());
        

        isStoreStats = true;
        
    }

    private class Achievement_t
    {

        public Achievement achievementID;
        public string name;
        public string desc;
        public bool isAchieved;

        public Achievement_t(Achievement id, string name, string desc)
        {
            achievementID = id;
            this.name = name;
            this.desc = desc;
            isAchieved = false;
        }
    }
}

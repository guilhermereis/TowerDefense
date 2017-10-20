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

    //currentStats
    private int numberOfWave;
    private int numberOfTowers;
    private bool isLevel3Built;

    //persistentStats
    private int totalWaves = 1;
    private int totalDefeats;

    protected string leaderboardName = "Defeated Waves";

    //callbacks
    protected Callback<UserAchievementStored_t> userAchievementsStored;
    protected Callback<UserStatsReceived_t> userStatsReceived;
    protected Callback<UserStatsStored_t> userStatsStored;
    //callresults
    protected CallResult<LeaderboardScoreUploaded_t> leaderboardScoreUploaded;
    protected CallResult<LeaderboardFindResult_t> findLeaderBoard;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnChangedLevel;

        
    }

    public void OnChangedLevel(Scene scene, LoadSceneMode mode)
    {
       
        if (scene.name == "MainScene")
        {
            Debug.Log(scene.name);
            onPlayScene = true;
           
        }
    }

    void Start()
    {
        
        if (!SteamManager.Initialized)
            return;

       


        //GameController.gamechangedDelegate += OnGameChanged;
        gameID = new CGameID(SteamUtils.GetAppID());

        userAchievementsStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
        userStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        userStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);

        findLeaderBoard = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFound);
        leaderboardScoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUpdated);
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
            SteamAPICall_t uHandle = SteamUserStats.UploadLeaderboardScore(pCallback.m_hSteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, totalWaves, scoreDetails, 1);
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

                SteamUserStats.GetStat("NumWaves", out totalWaves);
                SteamUserStats.GetStat("NumDefeats", out totalDefeats);

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

        
        GUILayout.Label("NumWaves: " + totalWaves);
        GUILayout.Label("NumDefeats: " + totalDefeats);
        

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
            numberOfTowers = 0;
            numberOfWave = 0;
        }else if (GameController.gameState == GameState.EndWave)
        {
            totalWaves++;
            Debug.Log("total waves " +totalWaves);
            isStoreStats = true;
            
        }else if(GameController.gameState == GameState.GameOver)
        {
            totalDefeats++;
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
                    if(numberOfTowers >= 10)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
            }
        }

        //store stats in the steam database if necessary.
        SteamUserStats.SetStat("NumWaves", totalWaves);
        SteamUserStats.SetStat("NumDefeats", totalDefeats);
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

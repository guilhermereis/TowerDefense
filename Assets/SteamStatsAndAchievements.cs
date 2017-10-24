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
        ACH_BUILD_100_TOWERS,
        ACH_BUILD_1000_TOWERS,

        ACH_BUILD_10_TOWER_1,
        ACH_BUILD_100_TOWER_1,
        ACH_BUILD_1000_TOWER_1,

        ACH_BUILD_10_TOWER_2,
        ACH_BUILD_100_TOWER_2,
        ACH_BUILD_1000_TOWER_2,

        ACH_BUILD_10_TOWER_3,
        ACH_BUILD_100_TOWER_3,
        ACH_BUILD_1000_TOWER_3,

        ACH_BUILD_10_TOWER_FIRE,
        ACH_BUILD_100_TOWER_FIRE,
        ACH_BUILD_1000_TOWER_FIRE,

        ACH_BUILD_10_TOWER_ICE,
        ACH_BUILD_100_TOWER_ICE,
        ACH_BUILD_1000_TOWER_ICE,

        ACH_BUILD_10_MINE,
        ACH_BUILD_100_MINE,
        ACH_BUILD_1000_MINE,

        ACH_KILL_10_MONSTERS,
        ACH_KILL_100_MONSTERS,
        ACH_KILL_1000_MONSTERS,

        ACH_KILL_10_WANDERER,
        ACH_KILL_100_WANDERER,
        ACH_KILL_1000_WANDERER,

        ACH_KILL_10_WARRIOR,
        ACH_KILL_100_WARRIOR,
        ACH_KILL_1000_WARRIOR,

        ACH_KILL_10_BOMBER,
        ACH_KILL_100_BOMBER,
        ACH_KILL_1000_BOMBER,

        ACH_KILL_10_KING,
        ACH_KILL_100_KING,
        ACH_KILL_1000_KING,

        ACH_UNLOCK_LANE2,
        ACH_UNLOCK_LANE3,
        ACH_UNLOCK_LANE4,

        ACH_FREEZE_10_ENEMY,
        ACH_FREEZE_100_ENEMY,
        ACH_FREEZE_1000_ENEMY,

        ACH_BURN_10_ENEMY,
        ACH_BURN_100_ENEMY,
        ACH_BURN_1000_ENEMY,

        ACH_SURVIVE_10_WAVES,
        ACH_SURVIVE_50_WAVES,
        ACH_SURVIVE_100_WAVES,

        ACH_REPAIR_10,
        ACH_REPAIR_50,
        ACH_REPAIR_100,

        ACH_INVICIBILITY_10_WAVES,
        ACH_INVICIBILITY_30_WAVES,
        ACH_INVICIBILITY_50_WAVES,
        ACH_INVICIBILITY_80_WAVES,

        ACH_CHEAP,
        ACH_MONEY_SAVER,
        ACH_MONEY_SPENDER,
        ACH_MONEY_COLLECTED_1000,
        ACH_MONEY_COLLECTED_10000,
        ACH_MONEY_RAISED_1000,
        ACH_MONEY_RAISED_10000,

        ACH_TRYAGAIN,



    };

    

    private List<Achievement_t> achievements = new List<Achievement_t> {
        new Achievement_t(Achievement.ACH_WIN_10_WAVES,"Gettin' good !","Defeated 10 waves"),
        new Achievement_t(Achievement.ACH_WIN_50_WAVES,"","Defeated 50 waves"),
        new Achievement_t(Achievement.ACH_WIN_100_WAVES,"","Defeated 100 waves"),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWERS,"Jack the Builder","Build 10 towers"),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWERS,"","Build 50 towers"),
        new Achievement_t(Achievement.ACH_BUILD_1000_TOWERS,"Empire","Build 100 towers"),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWER_1,"","Build 10 towers level 1"),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWER_1,"","Build 100 towers level 1"),
        new Achievement_t(Achievement.ACH_BUILD_1000_TOWER_1,"Ancient Warfare","Build 1000 towers level 1"),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWER_2,"Industrious","Build 10 towers level 2"),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWER_2,"","Build 100 towers level 2"),
        new Achievement_t(Achievement.ACH_BUILD_1000_TOWER_2,"","Build 1000 towers level 2"),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWER_3,"Architect","Build 10 towers level 3"),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWER_3,"","Build 100 towers level 3"),
        new Achievement_t(Achievement.ACH_BUILD_1000_TOWER_3,"","Build 1000 towers level 3"),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWER_FIRE,"","Build 10 fire towers"),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWER_FIRE,"","Build 100 fire towers"),
        new Achievement_t(Achievement.ACH_BUILD_1000_TOWER_FIRE,"","Build 1000 fire towers"),
        new Achievement_t(Achievement.ACH_BUILD_10_TOWER_ICE,"","Build 10 ice towers"),
        new Achievement_t(Achievement.ACH_BUILD_100_TOWER_ICE,"","Build 100 ice towers"),
        new Achievement_t(Achievement.ACH_BUILD_1000_TOWER_ICE,"","Build 1000 ice towers"),

        new Achievement_t(Achievement.ACH_BUILD_10_MINE,"Gold Digger","Build 10 mines"),
        new Achievement_t(Achievement.ACH_BUILD_100_MINE,"Entrepeneur","Build 100 mines"),
        new Achievement_t(Achievement.ACH_BUILD_1000_MINE,"","Build 1000 mines"),
        new Achievement_t(Achievement.ACH_KILL_10_MONSTERS,"","kill 10 goblins"),
        new Achievement_t(Achievement.ACH_KILL_100_MONSTERS,"Massacre","kill 100 goblins"),
        new Achievement_t(Achievement.ACH_KILL_1000_MONSTERS,"","kill 1000 goblins"),
        new Achievement_t(Achievement.ACH_KILL_10_WARRIOR,"","kill 10 warrior goblins"),
        new Achievement_t(Achievement.ACH_KILL_100_WARRIOR,"","kill 100 warrior goblins"),
        new Achievement_t(Achievement.ACH_KILL_1000_WARRIOR,"","kill 1000 warrior goblins"),
        new Achievement_t(Achievement.ACH_KILL_10_WANDERER,"","kill 10 wanderer goblins"),
        new Achievement_t(Achievement.ACH_KILL_100_WANDERER,"","kill 100 wanderer goblins"),
        new Achievement_t(Achievement.ACH_KILL_1000_WANDERER,"","kill 1000 wanderer goblins"),
        new Achievement_t(Achievement.ACH_KILL_10_BOMBER,"Counter-Measures","kill 10 bomber goblins"),
        new Achievement_t(Achievement.ACH_KILL_100_BOMBER,"Anti-Terrorist","kill 100 bomber goblins"),
        new Achievement_t(Achievement.ACH_KILL_1000_BOMBER,"","kill 1000 bomber goblins"),
        new Achievement_t(Achievement.ACH_KILL_10_KING,"Kingslayer","kill 10 king goblins"),
        new Achievement_t(Achievement.ACH_KILL_100_KING,"Funeral of the Greats","kill 100 king goblins"),
        new Achievement_t(Achievement.ACH_KILL_1000_KING,"Ruler of the Seven Kingdoms","kill 1000 king goblins"),
        new Achievement_t(Achievement.ACH_UNLOCK_LANE2,"Winter is coming","unlock snow field"),
        new Achievement_t(Achievement.ACH_UNLOCK_LANE3,"Mad Max","unlock desert field"),
        new Achievement_t(Achievement.ACH_UNLOCK_LANE2,"Rock 'n' Roll","unlock volcano field"),
        new Achievement_t(Achievement.ACH_FREEZE_10_ENEMY,"Chilly","freeze 10 enemies"),
        new Achievement_t(Achievement.ACH_FREEZE_100_ENEMY,"Mr. Freeze","freeze 100 enemies"),
        new Achievement_t(Achievement.ACH_FREEZE_1000_ENEMY,"Ice Age","freeze 1000 enemies"),
        new Achievement_t(Achievement.ACH_BURN_10_ENEMY,"Burn !","burn 10 enemies"),
        new Achievement_t(Achievement.ACH_BURN_100_ENEMY,"Kill it with Fire !","burn 100 enemies"),
        new Achievement_t(Achievement.ACH_BURN_1000_ENEMY,"Flamethrower","burn 1000 enemies"),
        new Achievement_t(Achievement.ACH_SURVIVE_10_WAVES,"What doesn't kill you","defeat 10 waves in a row"),
        new Achievement_t(Achievement.ACH_SURVIVE_50_WAVES,"","defeat 50 waves in a row"),
        new Achievement_t(Achievement.ACH_SURVIVE_100_WAVES,"","defeat 100 waves in a row"),
        new Achievement_t(Achievement.ACH_INVICIBILITY_10_WAVES,"Bulletproof","defeat 10 waves without taking a hit"),
        new Achievement_t(Achievement.ACH_INVICIBILITY_30_WAVES,"Nothing to lose","defeat 30 waves without taking a hit"),
        new Achievement_t(Achievement.ACH_INVICIBILITY_50_WAVES,"","defeat 50 waves without taking a hit"),
        new Achievement_t(Achievement.ACH_INVICIBILITY_80_WAVES,"Eternal","defeat 80 waves without taking a hit"),
        new Achievement_t(Achievement.ACH_CHEAP,"","Reach wave 10 using less than 500 gold"),
        new Achievement_t(Achievement.ACH_MONEY_SAVER,"Put it in the bank !","Have 10000 golds"),
        new Achievement_t(Achievement.ACH_MONEY_SPENDER,"","Spend 50.000 golds on building structures"),
        new Achievement_t(Achievement.ACH_MONEY_COLLECTED_1000,"Scrooge McDuck","collected 1000 golds"),
        new Achievement_t(Achievement.ACH_MONEY_COLLECTED_10000,"Rockefeller","collected 10000 golds"),
        new Achievement_t(Achievement.ACH_TRYAGAIN,"Die Hard","Use try again mode"),
        new Achievement_t(Achievement.ACH_REPAIR_10,"Fixed !","Repaired Castle 10 times"),
        new Achievement_t(Achievement.ACH_REPAIR_100,"Brand New !","Repaired Castle 100 times"),
        new Achievement_t(Achievement.ACH_REPAIR_50,"Not A Single Scratch","Repaired Castle 50 times"),
    };

    //our gameID
    private CGameID gameID;

    bool onPlayScene;
    private CastleHealth castleHealth;

    //storestats this frame
    private bool isStoreStats;

    //variables responsable for get info from steam
    private bool isRequestedStats;
    private bool isStatsvalid;

    //-----------------------------------------------------------------------------
    // Current Data
    //-----------------------------------------------------------------------------
    private int c_numberOfWave = 1;
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
    private int c_moneySaved;
    private int c_moneySpent = 0;
    private int c_unlockedLane2;
    private int c_unlockedLane3;
    private int c_unlockedLane4;
    private int c_repair;


    private bool isLevel3Built;

    //-----------------------------------------------------------------------------
    // Persistent Data
    //-----------------------------------------------------------------------------
    private int p_totalWaves;
    private int p_totalDefeats;
    private int p_totalTowerBuilt;
    private int p_totalTowerlevel1Built;
    private int p_totalTowerlevel2Built;
    private int p_totalTowerlevel3Built;
    private int p_totalTowerIceBuilt;
    private int p_totalTowerFireBuilt;
    private int p_totalMineBuilt;
    
    private int p_totalKingKilled;
    private int p_totalRepair;
 

    private int p_totalWandererKilled;
    private int p_totalWarriorKilled;
    private int p_totalBomberKilled;
    private int p_totalTryAgain;
    private int p_unlockedLane2;
    private int p_unlockedLane3;
    private int p_unlockedLane4;
    private int p_totalDamage;
    private int p_totalEnemyFrozen;
    private int p_totalEnemyBurnt;
    private int p_totalmoneyCollected;
    private int p_totalmoneySpent;
    private int p_totalmoneyRaised;
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
    public void CheckForTotalTowerBuilt()
    {
        if (c_numberOfTowers + p_totalTowerBuilt == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_TOWERS);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerBuilt += c_numberOfTowers;
                SteamUserStats.SetStat("totalNumTower", p_totalTowerBuilt);

            }


        }
        else if (c_numberOfTowers + p_totalTowerBuilt == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_TOWERS);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerBuilt += c_numberOfTowers;
                SteamUserStats.SetStat("totalNumTower", p_totalTowerBuilt);

            }
        }else  if(c_numberOfTowers + p_totalTowerBuilt == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_TOWERS);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerBuilt += c_numberOfTowers;
                SteamUserStats.SetStat("totalNumTower", p_totalTowerBuilt);

            }
        }
    }

    public void CheckForTotalTower1Built()
    {
        if (c_numberOftower1 + p_totalTowerlevel1Built == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_TOWER_1);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel1Built += c_numberOftower1;
                SteamUserStats.SetStat("totalNumTower1", p_totalTowerlevel1Built);

            }


        }
        else if (c_numberOftower1 + p_totalTowerlevel1Built == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_TOWER_1);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel1Built += c_numberOftower1;
                SteamUserStats.SetStat("totalNumTower1", p_totalTowerlevel1Built);

            }
        }
        else if (c_numberOftower1 + p_totalTowerlevel1Built == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_TOWER_1);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel1Built += c_numberOftower1;
                SteamUserStats.SetStat("totalNumTower1", p_totalTowerlevel1Built);

            }
        }
    }

    public void CheckForTotalTower2Built()
    {
        if (c_numberOftower2 + p_totalTowerlevel2Built == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_TOWER_2);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel2Built += c_numberOftower2;
                SteamUserStats.SetStat("totalNumTower2", p_totalTowerlevel2Built);

            }


        }
        else if (c_numberOftower2 + p_totalTowerlevel2Built == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_TOWER_2);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel2Built += c_numberOftower2;
                SteamUserStats.SetStat("totalNumTower2", p_totalTowerlevel2Built);

            }
        }
        else if (c_numberOftower2 + p_totalTowerlevel2Built == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_TOWER_2);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel2Built += c_numberOftower2;
                SteamUserStats.SetStat("totalNumTower2", p_totalTowerlevel2Built);

            }
        }
    }

    public void CheckForTotalTower3Built()
    {
        if (c_numberOftower3 + p_totalTowerlevel3Built == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_TOWER_3);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel3Built += c_numberOftower3;
                SteamUserStats.SetStat("totalNumTower3", p_totalTowerlevel3Built);

            }


        }
        else if (c_numberOftower3 + p_totalTowerlevel3Built == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_TOWER_3);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel3Built += c_numberOftower3;
                SteamUserStats.SetStat("totalNumTower3", p_totalTowerlevel3Built);

            }
        }
        else if (c_numberOftower3 + p_totalTowerlevel3Built == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_TOWER_3);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerlevel3Built += c_numberOftower3;
                SteamUserStats.SetStat("totalNumTower3", p_totalTowerlevel3Built);

            }
        }
    }

    public void CheckForTotalTowerIceBuilt()
    {
        if (c_numberOfIceTower + p_totalTowerIceBuilt == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_TOWER_ICE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerIceBuilt += c_numberOfIceTower;
                SteamUserStats.SetStat("totalNumTowerIce", p_totalTowerIceBuilt);

            }


        }
        else if (c_numberOfIceTower + p_totalTowerIceBuilt == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_TOWER_ICE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerIceBuilt += c_numberOfIceTower;
                SteamUserStats.SetStat("totalNumTowerIce", p_totalTowerIceBuilt);

            }
        }
        else if (c_numberOfIceTower + p_totalTowerIceBuilt == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_TOWER_ICE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerIceBuilt += c_numberOfIceTower;
                SteamUserStats.SetStat("totalNumTowerIce", p_totalTowerIceBuilt);

            }
        }
    }

    public void CheckForTotalTowerFireBuilt()
    {
        if (c_numberOfFireTower + p_totalTowerFireBuilt == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_TOWER_FIRE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerFireBuilt += c_numberOfFireTower;
                SteamUserStats.SetStat("totalNumTowerFire", p_totalTowerFireBuilt);

            }


        }
        else if (c_numberOfIceTower + p_totalTowerIceBuilt == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_TOWER_FIRE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerFireBuilt += c_numberOfFireTower;
                SteamUserStats.SetStat("totalNumTowerFire", p_totalTowerFireBuilt);

            }
        }
        else if (c_numberOfIceTower + p_totalTowerIceBuilt == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_TOWER_FIRE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalTowerFireBuilt += c_numberOfFireTower;
                SteamUserStats.SetStat("totalNumTowerFire", p_totalTowerFireBuilt);

            }
        }
    }

    public void CheckForTotalMineBuilt()
    {
        if (c_numberOfMine + p_totalMineBuilt == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_10_MINE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalMineBuilt += c_numberOfMine;
                SteamUserStats.SetStat("totalNumMineBuilt", p_totalMineBuilt);

            }


        }else if (c_numberOfMine + p_totalMineBuilt == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_100_MINE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalMineBuilt += c_numberOfMine;
                SteamUserStats.SetStat("totalNumMineBuilt", p_totalMineBuilt);

            }


        }else if (c_numberOfMine + p_totalMineBuilt == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_BUILD_1000_MINE);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalMineBuilt += c_numberOfMine;
                SteamUserStats.SetStat("totalNumMineBuilt", p_totalMineBuilt);

            }


        }
    }

    public void AddBuiltTower(BuildType tower)
    {
        if (!SteamManager.Initialized)
            return;
        
        c_numberOfTowers ++;
        CheckForTotalTowerBuilt();



        //if(c_numberOfTowers + p_totalTowerBuilt > )

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
        CheckForTotalTower1Built();
    }

    public void AddTower2()
    {
        c_numberOftower2++;
        CheckForTotalTower2Built();
    }

    public void AddTower3()
    {
        c_numberOftower3++;
        CheckForTotalTower2Built();
    }

    public void AddMine()
    {
        c_numberOfMine++;
        CheckForTotalMineBuilt();
    }
    #endregion


    #region Monsters

    public void CheckForTotalGoblinsKilled()
    {
        if (c_numberOfMonstersKilled + p_totalMonsterKilled == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_10_MONSTERS);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalMonsterKilled += c_numberOfMonstersKilled;
                SteamUserStats.SetStat("totalNumGoblinKilled", p_totalMonsterKilled);

            }


        }else if (c_numberOfMonstersKilled + p_totalMonsterKilled == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_100_MONSTERS);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalMonsterKilled += c_numberOfMonstersKilled;
                SteamUserStats.SetStat("totalNumGoblinKilled", p_totalMonsterKilled);

            }


        }else if (c_numberOfMonstersKilled + p_totalMonsterKilled == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_1000_MONSTERS);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalMonsterKilled += c_numberOfMonstersKilled;
                SteamUserStats.SetStat("totalNumGoblinKilled", p_totalMonsterKilled);

            }


        }
    }

    public void CheckForTotalWandererKilled()
    {
        if (c_numberOfWandererKilled + p_totalWandererKilled == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_10_WANDERER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalWandererKilled += c_numberOfWandererKilled;
                SteamUserStats.SetStat("totalNumGoblinKilled", p_totalWandererKilled);

            }


        }else if (c_numberOfWandererKilled + p_totalWandererKilled == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_100_WANDERER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalWandererKilled += c_numberOfWandererKilled;
                SteamUserStats.SetStat("totalNumGoblinKilled", p_totalWandererKilled);

            }


        }else if (c_numberOfWandererKilled + p_totalWandererKilled == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_1000_WANDERER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalWandererKilled += c_numberOfWandererKilled;
                SteamUserStats.SetStat("totalNumGoblinKilled", p_totalWandererKilled);

            }


        }
    }

    public void CheckForTotalWarriorKilled()
    {
        if (c_numberOfWarriorKilled + p_totalWarriorKilled == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_10_WARRIOR);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalWarriorKilled += c_numberOfWarriorKilled;
                SteamUserStats.SetStat("totalNumWarriorKilled", p_totalWarriorKilled);

            }


        }
        else if (c_numberOfWarriorKilled + p_totalWarriorKilled == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_100_WARRIOR);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalWarriorKilled += c_numberOfWarriorKilled;
                SteamUserStats.SetStat("totalNumWarriorKilled", p_totalWarriorKilled);

            }


        }
        else if (c_numberOfWarriorKilled + p_totalWarriorKilled == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_1000_WARRIOR);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalWarriorKilled += c_numberOfWarriorKilled;
                SteamUserStats.SetStat("totalNumWarriorKilled", p_totalWarriorKilled);

            }


        }
    }

    public void CheckForTotalBomberKilled()
    {
        if (c_numberOfBomberKilled + p_totalBomberKilled == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_100_BOMBER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalBomberKilled += c_numberOfBomberKilled;
                SteamUserStats.SetStat("totalNumBomberKilled", p_totalBomberKilled);

            }


        }else if (c_numberOfBomberKilled + p_totalBomberKilled == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_1000_BOMBER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalBomberKilled += c_numberOfBomberKilled;
                SteamUserStats.SetStat("totalNumBomberKilled", p_totalBomberKilled);

            }


        }else if (c_numberOfBomberKilled + p_totalBomberKilled == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_1000_BOMBER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalBomberKilled += c_numberOfBomberKilled;
                SteamUserStats.SetStat("totalNumBomberKilled", p_totalBomberKilled);

            }


        }
    }

    public void CheckForTotalKingKilled()
    {
        if (c_numberOfKingKilled + p_totalKingKilled == 10)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_10_KING);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalKingKilled += c_numberOfKingKilled;
                SteamUserStats.SetStat("totalNumKingKilled", p_totalKingKilled);

            }


        }
        else if (c_numberOfKingKilled + p_totalKingKilled == 100)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_100_KING);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalKingKilled += c_numberOfKingKilled;
                SteamUserStats.SetStat("totalNumKingKilled", p_totalKingKilled);

            }


        }
        else if (c_numberOfKingKilled + p_totalKingKilled == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_KILL_1000_KING);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalKingKilled += c_numberOfKingKilled;
                SteamUserStats.SetStat("totalNumKingKilled", p_totalKingKilled);

            }


        }
    }

    public void AddMonstersKilled(PawnType monster,DamageType _damage)
    {
        if (!SteamManager.Initialized)
            return;

        c_numberOfMonstersKilled++;
        CheckForTotalGoblinsKilled();
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
        if (_damage == DamageType.Fire)
            BurnGoblin();
       
    }

    private void AddBomber()
    {
        c_numberOfBomberKilled++;
        CheckForTotalBomberKilled();
    }

    private void AddWarrior()
    {
        c_numberOfWarriorKilled++;
        CheckForTotalWarriorKilled();
    }
    private void AddKing()
    {
        c_numberOfKingKilled++;
        CheckForTotalKingKilled();
    }
    private void AddWanderer()
    {
        c_numberOfWandererKilled++;
        CheckForTotalWandererKilled();
    }

    #endregion

    public void CheckForTotalMoneyCollected()
    {
        if (c_moneyCollected + p_totalmoneyCollected == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_MONEY_COLLECTED_1000);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalmoneyCollected += c_moneyCollected;
                SteamUserStats.SetStat("totalNumMoneyCollected", p_totalmoneyCollected);

            }


        }
        else if (c_moneyCollected + p_totalmoneyCollected == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_MONEY_COLLECTED_1000);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalmoneyCollected += c_moneyCollected;
                SteamUserStats.SetStat("totalNumMoneyCollected", p_totalmoneyCollected);

            }


        }
    }

    public void CheckForTotalMoneyRaised()
    {
        if (c_moneyRaised + p_totalmoneyRaised == 1000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_MONEY_RAISED_1000);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalmoneyRaised += c_moneyRaised;
                SteamUserStats.SetStat("totalNumMoneyRaised", p_totalmoneyRaised);

            }


        }
        else if (c_moneyRaised + p_totalmoneyRaised == 10000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_MONEY_RAISED_10000);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                p_totalmoneyRaised += c_moneyRaised;
                SteamUserStats.SetStat("totalNumMoneyRaised", p_totalmoneyRaised);

            }


        }
    }

    public void CheckForMoneySaved()
    {
        if (c_moneyRaised == 10000)
        {
            Achievement_t achv = achievements.Find(achID => achID.achievementID == Achievement.ACH_MONEY_SAVER);
            if (!achv.isAchieved)
            {
                //unlock achievement and store the stats value
                UnlockAchievement(achv);
                //p_totalmoneyRaised += c_moneyRaised;
                //SteamUserStats.SetStat("totalNumMoneyRaised", p_totalmoneyRaised);

            }


        }
    }

    public void AddMoneyCollected(int gold, bool wasCollected)
    {
        if (!SteamManager.Initialized)
            return;

        c_moneyRaised += gold;
        CheckForTotalMoneyRaised();
        CheckForMoneySaved();

        if (wasCollected)
        {
            c_moneyCollected += gold;
            CheckForTotalMoneyCollected();
        }
    }

    public void CheckForTotalSpentMoney()
    {
        if(c_moneySpent + p_totalmoneySpent == 50000)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_MONEY_SPENDER);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
            }
        }
    }

    public void SpendMoney(int gold)
    {
        if (!SteamManager.Initialized)
            return;

        c_moneySpent += gold;
        CheckForTotalSpentMoney();
    }

    public void CheckForBurnt()
    {
        if (c_numberOfBurnt + p_totalEnemyBurnt == 10)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_BURN_10_ENEMY);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
                p_totalEnemyBurnt += c_numberOfBurnt;
                SteamUserStats.SetStat("totalNumEnemyBurnt", p_totalEnemyBurnt);
            }
        }else if (c_numberOfBurnt + p_totalEnemyBurnt == 100)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_BURN_10_ENEMY);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
                p_totalEnemyBurnt += c_numberOfBurnt;
                SteamUserStats.SetStat("totalNumEnemyBurnt", p_totalEnemyBurnt);
            }
        }else if (c_numberOfBurnt + p_totalEnemyBurnt == 1000)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_BURN_1000_ENEMY);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
                p_totalEnemyBurnt += c_numberOfBurnt;
                SteamUserStats.SetStat("totalNumEnemyBurnt", p_totalEnemyBurnt);
            }
        }
    }

    public void BurnGoblin()
    {

        if (!SteamManager.Initialized)
            return;

        c_numberOfBurnt++;
        CheckForBurnt();
    }

    public void CheckForFrozen()
    {
        if (c_numberOfFrozen + p_totalEnemyFrozen == 10)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_FREEZE_10_ENEMY);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
                p_totalEnemyFrozen += c_numberOfFrozen;
                SteamUserStats.SetStat("totalNumEnemyFrozen", p_totalEnemyFrozen);
            }
        }
        else if (c_numberOfFrozen + p_totalEnemyFrozen == 100)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_FREEZE_10_ENEMY);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
                p_totalEnemyFrozen += c_numberOfFrozen;
                SteamUserStats.SetStat("totalNumEnemyFrozen", p_totalEnemyFrozen);
            }
        }
        else if (c_numberOfFrozen + p_totalEnemyFrozen == 1000)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_FREEZE_1000_ENEMY);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
                p_totalEnemyFrozen += c_numberOfFrozen;
                SteamUserStats.SetStat("totalNumEnemyFrozen", p_totalEnemyFrozen);
            }
        }
    }

    public void FreezeGoblin()
    {
        if (!SteamManager.Initialized)
            return;

        c_numberOfFrozen++;
        CheckForFrozen();


    }

    public void UnlockLane2()
    {
        if (!SteamManager.Initialized)
            return;

        if (p_unlockedLane2 > 0)
            return;

        c_unlockedLane2 = 1;


        Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_UNLOCK_LANE2);
        if (!achv.isAchieved)
        {
            p_unlockedLane2 = 1;
            SteamUserStats.SetStat("unlockedLane2", p_unlockedLane2);
            UnlockAchievement(achv);
        }
    }
    public void UnlockLane3()
    {
        if (!SteamManager.Initialized)
            return;

        if (p_unlockedLane3 > 0)
            return;

        c_unlockedLane3 = 1;

        Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_UNLOCK_LANE3);
        if (!achv.isAchieved)
        {
            p_unlockedLane3 = 1;
            SteamUserStats.SetStat("unlockedLane3", p_unlockedLane3);
            UnlockAchievement(achv);
        }
    }
    public void UnlockLane4()
    {
        if (!SteamManager.Initialized)
            return;

        if (p_unlockedLane4 > 0)
            return;

        c_unlockedLane4 = 1;

        Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_UNLOCK_LANE4);
        if (!achv.isAchieved)
        {
            p_unlockedLane4 = 1;
            SteamUserStats.SetStat("unlockedLane4", p_unlockedLane4);
            UnlockAchievement(achv);
        }
    }

    public void CheckForRepair()
    {
        if(c_repair + p_totalRepair == 10)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_REPAIR_10);

            if (!achv.isAchieved)
            {
                p_totalRepair += c_repair;
                SteamUserStats.SetStat("totalNumRepair", p_totalRepair);
                UnlockAchievement(achv);
            }

        }else if (c_repair + p_totalRepair == 50)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_REPAIR_50);

            if (!achv.isAchieved)
            {
                p_totalRepair += c_repair;
                SteamUserStats.SetStat("totalNumRepair", p_totalRepair);
                UnlockAchievement(achv);
            }

        }else if (c_repair + p_totalRepair == 100)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_REPAIR_100);

            if (!achv.isAchieved)
            {
                p_totalRepair += c_repair;
                SteamUserStats.SetStat("totalNumRepair", p_totalRepair);
                UnlockAchievement(achv);
            }

        }
    }

    public void Repair()
    {
        if (!SteamManager.Initialized)
            return;

        c_repair++;
        CheckForRepair();
    }

    public void CheckForWaves()
    {
        if (c_numberOfWave + p_totalWaves == 10)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_WIN_10_WAVES);
            
            if (!achv.isAchieved)
            {
                p_totalWaves += c_numberOfWave;
                SteamUserStats.SetStat("totalNumWaves", p_totalWaves);
                UnlockAchievement(achv);
            }

            

        }

        if(c_numberOfWave == 10)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_SURVIVE_10_WAVES);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
            }

            if (!castleHealth.wasDamaged)
            {
                Achievement_t achv1 = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_INVICIBILITY_10_WAVES);
                if (!achv1.isAchieved)
                {
                    UnlockAchievement(achv1);
                }
            }

            if(c_moneySpent < 500)
            {
                Achievement_t achv2 = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_CHEAP);
                if (!achv2.isAchieved)
                {
                    UnlockAchievement(achv2);
                }
            }
        }
        else if(c_numberOfWave == 30)
        {
            if (!castleHealth.wasDamaged)
            {
                Achievement_t achv1 = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_INVICIBILITY_30_WAVES);
                if (!achv1.isAchieved)
                {
                    UnlockAchievement(achv1);
                }
            }
        }
        else if (c_numberOfWave == 50)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_SURVIVE_50_WAVES);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
            }

            if (!castleHealth.wasDamaged)
            {
                Achievement_t achv1 = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_INVICIBILITY_50_WAVES);
                if (!achv1.isAchieved)
                {
                    UnlockAchievement(achv1);
                }
            }
        }else if(c_numberOfWave == 80)
        {
            if (!castleHealth.wasDamaged)
            {
                Achievement_t achv1 = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_INVICIBILITY_80_WAVES);
                if (!achv1.isAchieved)
                {
                    UnlockAchievement(achv1);
                }
            }
        }
        else if (c_numberOfWave == 100)
        {
            Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_SURVIVE_100_WAVES);
            if (!achv.isAchieved)
            {
                UnlockAchievement(achv);
            }
        }
    }

    public void AddWaves()
    {
        if (!SteamManager.Initialized)
            return;

        c_numberOfWave++;
        CheckForWaves();
    }

    public void TryAgain()
    {
        if (!SteamManager.Initialized)
            return;
        
        p_totalTryAgain++;

        Achievement_t achv = achievements.Find(achvID => achvID.achievementID == Achievement.ACH_TRYAGAIN);
        if (!achv.isAchieved)
        {
            UnlockAchievement(achv);
        }

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

    public void UpdateStats()
    {
        p_totalWaves+= c_numberOfWave;
        p_totalTowerBuilt += c_numberOfTowers;
        p_totalTowerlevel1Built += c_numberOftower1;
        p_totalTowerlevel2Built += c_numberOftower2;
        p_totalTowerlevel3Built += c_numberOftower3;
        p_totalTowerIceBuilt += c_numberOfIceTower;
        p_totalTowerFireBuilt += c_numberOfFireTower;
        p_totalMineBuilt+= c_numberOfMine;
        p_totalKingKilled += c_numberOfKingKilled;
        p_totalRepair += c_repair;
        p_totalWandererKilled+= c_numberOfWandererKilled;
        p_totalWarriorKilled+= c_numberOfWarriorKilled;
        p_totalBomberKilled+= c_numberOfBomberKilled;    
        p_totalEnemyFrozen+= c_numberOfFrozen;
        p_totalEnemyBurnt += c_numberOfBurnt;
        p_totalmoneyCollected += c_moneyCollected;
        p_totalmoneySpent += c_moneySpent;
        p_totalmoneyRaised += c_moneyRaised;
        p_totalMonsterKilled += c_numberOfMonstersKilled;


        //store stats in the steam database if necessary.
        SteamUserStats.SetStat("totalNumWaves", p_totalWaves);
        SteamUserStats.SetStat("totalNumDefeats", p_totalDefeats);

        SteamUserStats.SetStat("totalNumTower", p_totalTowerBuilt);
        SteamUserStats.SetStat("totalNumTower1", p_totalTowerlevel1Built);
        SteamUserStats.SetStat("totalNumTower2", p_totalTowerlevel2Built);
        SteamUserStats.SetStat("totalNumTower3", p_totalTowerlevel3Built);
        SteamUserStats.SetStat("totalNumTowerIce", p_totalTowerIceBuilt);
        SteamUserStats.SetStat("totalNumTowerFire", p_totalTowerFireBuilt);
        SteamUserStats.SetStat("totalNumMineBuilt", p_totalMineBuilt);

        SteamUserStats.SetStat("totalNumKingKilled", p_totalKingKilled);
        SteamUserStats.SetStat("totalNumWandererKilled", p_totalWandererKilled);
        SteamUserStats.SetStat("totalNumWarriorKilled", p_totalWarriorKilled);
        SteamUserStats.SetStat("totalNumBomberKilled", p_totalBomberKilled);
        SteamUserStats.SetStat("totalNumGoblinKilled", p_totalMonsterKilled);

        SteamUserStats.SetStat("totalNumTryAgain", p_totalTryAgain);
        SteamUserStats.SetStat("totalNumDamage", p_totalDamage);
        SteamUserStats.SetStat("totalNumEnemyFrozen", p_totalEnemyFrozen);
        SteamUserStats.SetStat("totalNumEnemyBurnt", p_totalEnemyBurnt);
        SteamUserStats.SetStat("totalNumMonsterKilled", p_totalMonsterKilled);
        SteamUserStats.SetStat("unlockedLane2", p_unlockedLane2);
        SteamUserStats.SetStat("unlockedLane3", p_unlockedLane3);
        SteamUserStats.SetStat("unlockedLane4", p_unlockedLane4);
        SteamUserStats.SetStat("totalNumMoneyCollected", p_totalmoneyCollected);
        SteamUserStats.SetStat("totalNumMoneySpent", p_totalmoneySpent);
        SteamUserStats.SetStat("totalNumMoneyRaised", p_totalmoneyRaised);
        SteamUserStats.SetStat("totalNumRepair", p_totalRepair);


        bool succes = SteamUserStats.StoreStats();

        isStoreStats = !succes;
    }

    public void UpdateAchievements()
    {
        bool succes = SteamUserStats.StoreStats();
        if (succes)
        {
            Debug.Log("Achieved Stored With Success");
        }else
            Debug.Log("Achieved not stored");
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
                SteamUserStats.GetStat("totalNumTower1", out p_totalTowerlevel1Built);
                SteamUserStats.GetStat("totalNumTower2", out p_totalTowerlevel2Built);
                SteamUserStats.GetStat("totalNumTower3", out p_totalTowerlevel3Built);
                SteamUserStats.GetStat("totalNumTowerIce", out p_totalTowerIceBuilt);
                SteamUserStats.GetStat("totalNumTowerFire", out p_totalTowerFireBuilt);
                SteamUserStats.GetStat("totalNumMineBuilt", out p_totalMineBuilt);

                SteamUserStats.GetStat("totalNumKingKilled", out p_totalKingKilled);
                SteamUserStats.GetStat("totalNumWandererKilled", out p_totalWandererKilled);
                SteamUserStats.GetStat("totalNumWarriorKilled", out p_totalWarriorKilled);
                SteamUserStats.GetStat("totalNumBomberKilled", out p_totalBomberKilled);
                SteamUserStats.GetStat("totalNumGoblinKilled", out p_totalMonsterKilled);

                SteamUserStats.GetStat("totalNumTryAgain", out p_totalTryAgain);
                SteamUserStats.GetStat("totalNumDamage", out p_totalDamage);
                SteamUserStats.GetStat("totalNumEnemyFrozen", out p_totalEnemyFrozen);
                SteamUserStats.GetStat("totalNumEnemyBurnt", out p_totalEnemyBurnt);
                SteamUserStats.GetStat("totalNumMonsterKilled", out p_totalMonsterKilled);
                SteamUserStats.GetStat("unlockedLane2", out p_unlockedLane2 );
                SteamUserStats.GetStat("unlockedLane3", out p_unlockedLane3);
                SteamUserStats.GetStat("unlockedLane4", out p_unlockedLane4);
                SteamUserStats.GetStat("totalNumMoneyCollected", out p_totalmoneyCollected);
                SteamUserStats.GetStat("totalNumMoneySpent", out p_totalmoneySpent);
                SteamUserStats.GetStat("totalNumMoneyRaised", out p_totalmoneyRaised);
                SteamUserStats.GetStat("totalNumRepair", out p_totalRepair);



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

    public void ResetAllCurrentStats()
    {
        c_numberOfTowers = 0;
        c_numberOfWave = 0;
        c_numberOfTowers = 0;
        c_numberOftower1 = 0;
        c_numberOftower2 = 0;
        c_numberOftower3 = 0;
        c_numberOfIceTower = 0;
        c_numberOfFireTower = 0;
        c_numberOfMine = 0;
        c_numberOfMonstersKilled = 0;
        c_numberOfKingKilled = 0;
        c_numberOfWandererKilled = 0;
        c_numberOfWarriorKilled = 0;
        c_numberOfBomberKilled = 0;
        c_numberOfFrozen = 0;
        c_numberOfBurnt = 0;
        c_numberOfDamage = 0;
        c_moneyCollected = 0;
        c_moneyRaised = 0;
        c_moneySaved = 0;
        c_moneySpent = 0;
        c_unlockedLane2 = 0;
        c_unlockedLane3 = 0;
        c_unlockedLane4 = 0;
        c_repair = 0;

}
    	
    public void OnGameChanged()
    {
        Debug.Log("GameChanged");
        if (GameController.gameState == GameState.GameActivate)
        {
            ResetAllCurrentStats();
        }else if (GameController.gameState == GameState.EndWave)
        {
            AddWaves();
            Debug.Log("total waves " +p_totalWaves);
            UpdateStats();
            
        }else if(GameController.gameState == GameState.GameOver)
        {
            p_totalDefeats++;
            UpdateStats();
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
            castleHealth = GameObject.FindObjectOfType<CastleHealth>();
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

        if (isStoreStats)
        {
           
            //SteamUserStats.UploadLeaderboardScore()

        }
        

    }

    private void UnlockAchievement(Achievement_t achievement)
    {
        achievement.isAchieved = true;

        SteamUserStats.SetAchievement(achievement.achievementID.ToString());

        UpdateAchievements();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerLevel[] playerLevels;
    private Dictionary<PlayerLevel, int> PLDataToLevel = new Dictionary<PlayerLevel, int>();
    private Dictionary<int, PlayerLevel> LevelToPLD = new Dictionary<int, PlayerLevel>();
    private PlayerLevel currentLevel, maxLevel; 

    public void Initialize()
    {
        for(int i = 0; i < playerLevels.Length;  i++)
        {
            PLDataToLevel.Add(playerLevels[i], i);
            LevelToPLD.Add(i, playerLevels[i]); 
            maxLevel = playerLevels[i];
        }
        currentLevel = LevelToPLD[0]; 
    }

    public int GetLevelFromPLD(PlayerLevel playerLevel)
    {
        return PLDataToLevel[playerLevel]; 
    }

    public PlayerLevel GetCurrentPlayerLevel()
    {
        return currentLevel; 
    }

    public void CheckForLevelUp()
    {
        if (currentLevel == maxLevel) return;
        if (GameManager.Instance.GetTotalVoters() >= GetNextLevel().voterRequirement)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel = GetNextLevel();
        UIManager.Instance.SetPlayerLevelUI(currentLevel);
    }
    private PlayerLevel GetNextLevel()
    {
        if (currentLevel == maxLevel) return maxLevel;
        int lvl = PLDataToLevel[currentLevel]+1;
        return LevelToPLD[lvl];

    }
}

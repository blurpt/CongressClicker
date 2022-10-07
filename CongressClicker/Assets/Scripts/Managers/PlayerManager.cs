using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerLevel[] playerLevels;
    private Dictionary<PlayerLevel, int> PLDataToLevel = new Dictionary<PlayerLevel, int>();
    private Dictionary<int, PlayerLevel> LevelToPLD = new Dictionary<int, PlayerLevel>();
    private PlayerLevel currentLevel, maxLevel;
    private int merchIndex;

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

    public float PromotionProgressPercent()
    {
        int minVoters = currentLevel.voterRequirement;
        int maxVoters = GetNextLevel().voterRequirement;
        return Mathf.InverseLerp(minVoters, maxVoters, GameManager.Instance.GetTotalVoters()); 
    }

    public void CheckForLevelUp()
    {
        if (currentLevel == maxLevel) return;
        if (GameManager.Instance.GetTotalVoters() >= GetNextLevel().voterRequirement)
        {
            GameManager.Instance.StartQuiz(GetNextLevel());
            SoundManager.Instance.PlayQuizMusic();
        }

    }

    public void Promote()
    {
        currentLevel = GetNextLevel();
        UIManager.Instance.SetPlayerLevelUI(currentLevel);
    }

    public void SetMerchIndex(int index)
    {
        merchIndex = index;
    }

    public int GetMerchIndex()
    {
        return merchIndex; 
    }

    public bool IsMaxLevel()
    {
        Debug.Log(currentLevel + " | " + maxLevel);
        return currentLevel == maxLevel;
    }

    private PlayerLevel GetNextLevel()
    {
        if (currentLevel == maxLevel) return maxLevel;
        int lvl = PLDataToLevel[currentLevel]+1;
        return LevelToPLD[lvl];

    }
}

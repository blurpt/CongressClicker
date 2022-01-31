using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : Singleton<UIManager>
{
    public Text voterLable, levelLable;
    public Transform investmentContainer; 

    public void UpdateVoterCount(int num)
    {
        voterLable.text = GetVotersLableText(num); 
    }
    public void PopulateInvestments()
    {
        InvestmentsManager.Instance.SpawnInvestments(investmentContainer);
    }

    public void SetPlayerLevelUI(PlayerLevel playerLevel)
    {
        levelLable.text = "LvL " + PlayerManager.Instance.GetLevelFromPLD(playerLevel) + ": " + playerLevel.displayName; 
    }

    private string GetVotersLableText(int voters)
    {
        return "Voters: " + voters.ToString(); 
    }
}

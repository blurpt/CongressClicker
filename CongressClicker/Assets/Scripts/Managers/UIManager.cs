using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : Singleton<UIManager>
{
    public Text voterLable;
    public Transform investmentContainer; 

    public void UpdateVoterCount(int num)
    {
        voterLable.text = GetVotersLableText(num); 
    }
    public void PopulateInvestments()
    {
        InvestmentsManager.Instance.SpawnInvestments(investmentContainer);
    }

    private string GetVotersLableText(int voters)
    {
        return "Voters: " + voters.ToString(); 
    }
}

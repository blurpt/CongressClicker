using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeRequirement
{
    public string displayName;
    public int ID; 
    public int quantityRequirement;
    public int cost;
    public string description; 
    public Sprite icon;

    public bool CanAfford(Investment Investment)
    {
        return GameManager.Instance.GetVoters() >= cost && InvestmentsManager.Instance.GetInvestmentQuantity(Investment) >= quantityRequirement; 
    }
}

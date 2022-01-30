using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int totalVoters;
    private float timer; 
    public int GetVoters()
    {
        return totalVoters;
    }
    public bool CanPurchaseInvestment(int cost)
    {
        return totalVoters > cost;
    }

    private void Start()
    {
        PopulateContent();
    }
    private void Update()
    {
        foreach(InvestmentButton button in InvestmentsManager.Instance.GetInvestmentButtons())
        {
            button.CacheTimer();
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 1;
            InvestmentsManager.Instance.ActivateAvalibleUpgrades();
        }
    }

    private void PopulateContent()
    {
        UIManager.Instance.PopulateInvestments();
        InvestmentsManager.Instance.PopulateUpgradeRequirements();
    }

    public void AddVoters(int voters)
    {
        totalVoters += voters;
        UIManager.Instance.UpdateVoterCount(totalVoters);
    }

    public void PurchaseInvestment(int cost)
    {
        if (totalVoters < cost) return; 

        totalVoters -= cost;
        UIManager.Instance.UpdateVoterCount(totalVoters);
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool pauseLeveling, unlimitedVoters;
    private int voterCurrency, totalVoters;
    private float timer;
    private bool paused; 
    public int GetVoterCurrency()
    {
        return voterCurrency;
    }
    public int GetTotalVoters()
    {
        return totalVoters;
    }
    public bool CanPurchaseInvestment(int cost)
    {
        if (unlimitedVoters== false)
        {
            return voterCurrency >= cost;
        }
        else
            return true;
    }

    private void Start()
    {
        UIManager.Instance.PopulateInvestments();
        InvestmentsManager.Instance.PopulateUpgradeRequirements();
        InvestmentsManager.Instance.PopulateMerchSelection();
        PlayerManager.Instance.Initialize();
        UIManager.Instance.SetPlayerLevelUI(PlayerManager.Instance.GetCurrentPlayerLevel());
        UIManager.Instance.UpdateVoterCount(voterCurrency);
    }
    private void Update()
    {
        if (paused) return; 

        foreach(InvestmentButton button in InvestmentsManager.Instance.GetInvestmentButtons())
        {
            button.CacheTimer();
        }

        UIManager.Instance.SetPromotionProgressBar(PlayerManager.Instance.PromotionProgressPercent());

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 1;
            InvestmentsManager.Instance.ActivateAvalibleUpgrades();
            if(!pauseLeveling)
            {
                PlayerManager.Instance.CheckForLevelUp();
            }
        }
    }

    public void AddVoters(int voters)
    {
        voterCurrency += voters;
        totalVoters += voters; 
        UIManager.Instance.UpdateVoterCount(voterCurrency);
    }

    public void PurchaseInvestment(int cost)
    {

        if (voterCurrency <= cost && unlimitedVoters ==false) return;
        voterCurrency -= cost;
        UIManager.Instance.UpdateVoterCount(voterCurrency);
    }

    public void StartQuiz(PlayerLevel level)
    {
        paused = true;
        UIManager.Instance.StartQuiz(level);
    }

    public void EndQuiz(bool passed)
    {
        paused = false;
        int voterPercent = Mathf.RoundToInt(voterCurrency * .25f);
        if(passed)
        {
            totalVoters += voterPercent;
            voterCurrency += voterPercent;
            PlayerManager.Instance.Promote(); 
        }
        else
        {
            totalVoters -= voterPercent;
            voterCurrency -= voterPercent;
        }

        UIManager.Instance.UpdateVoterCount(voterCurrency);

    }




}

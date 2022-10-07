using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvestmentButton : MonoBehaviour
{
    public Text displayName, quantity, cost;
    public AudioClip cantPurchase;
    public Button button;
    private Investment investment;
    private float timer;

    public void CacheTimer()
    {
        if (InvestmentsManager.Instance.GetInvestmentQuantity(investment) == 0) return;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Cache();
            timer = investment.secondsToCache; 
        }
    }

    public void SetButtonInteractive()
    {
        if (GameManager.Instance.CanPurchaseInvestment(InvestmentsManager.Instance.GetCurrentInvestmentCost(investment)) == false)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void Populate(string DisplayName, int Quantity, Investment Investment)
    {
        displayName.text = DisplayName;
        quantity.text = Quantity.ToString();
        investment = Investment;
        timer = investment.secondsToCache;
        cost.text = investment.startingCost.ToString();
    }

    public void IncreaseInvestment()
    {
        if (GameManager.Instance.CanPurchaseInvestment(InvestmentsManager.Instance.GetCurrentInvestmentCost(investment)) == false)
        {
            SoundManager.Instance.PlayUISound(cantPurchase);
            return;
        }

        GameManager.Instance.PurchaseInvestment(InvestmentsManager.Instance.GetCurrentInvestmentCost(investment));
        InvestmentsManager.Instance.IncreaseInvestment(investment);
        InvestmentsManager.Instance.AddInvestmentToPanel(investment); 
        quantity.text = InvestmentsManager.Instance.GetInvestmentQuantity(investment).ToString();
        cost.text = InvestmentsManager.Instance.GetCurrentInvestmentCost(investment).ToString();
        SoundManager.Instance.PlayUISound(investment.purchaseSound);
        InvestmentsManager.Instance.FocusInvestment(investment);
    }

    public Investment GetInvestment()
    {
        return investment; 
    }

    private void Cache()
    {
        GameManager.Instance.AddVoters(VoterCalculation());
    }

    private int VoterCalculation()
    {
        int calculation = investment.votesPerCache * InvestmentsManager.Instance.GetInvestmentQuantity(investment);
        for (int i = 0; i < InvestmentsManager.Instance.InvestmentUpgradesAquired(investment); i++)
        {
            calculation = calculation * 2; 
        }

        return calculation; 
    }



}

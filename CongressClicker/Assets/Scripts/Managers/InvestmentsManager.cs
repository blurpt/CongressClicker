using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class InvestmentsManager : Singleton<InvestmentsManager>
{
    public Investment[] investments;
    public MerchSet[] merch; 
    public GameObject investmentPrefab, upgradePrefab, investmentPanelPrefab;
    public Transform upgradeContainer, displayContainer;
    public Scrollbar scrollbar;
    private List<InvestmentButton> investmentButtons = new List<InvestmentButton>();
    private Dictionary<string, int> investmentQuantities = new Dictionary<string, int>();
    private Dictionary<int, UpgradeRequirement> InvestmentUpgradeRequirments = new Dictionary<int, UpgradeRequirement>();
    private Dictionary<int, Investment> UpgradeInvestment = new Dictionary<int, Investment>();
    private Dictionary<Investment, InvestmentPanel> InvestmentPanels = new Dictionary<Investment, InvestmentPanel>();
    private Dictionary<int, MerchSet> merchSets = new Dictionary<int, MerchSet>();

    private List<int> purchasedUpgradeIDs = new List<int>();
    private List<int> spawnedUpgradeIDs = new List<int>();


    public int GetInvestmentQuantity(Investment investment)
    {
        return investmentQuantities[investment.displayName]; 
    }

    public int GetCurrentInvestmentCost(Investment investment)
    {
        return Mathf.RoundToInt(investment.startingCost + ((investment.startingCost * InvestmentsManager.Instance.GetInvestmentQuantity(investment)) * .25f));
    }

    public List<InvestmentButton> GetInvestmentButtons()
    {
        return investmentButtons; 
    }

    public int InvestmentUpgradesAquired(Investment investment)
    {
        int x = 0; 
        foreach(UpgradeRequirement upgrade in investment.upgradeRequirements)
        {
            if(purchasedUpgradeIDs.Contains(upgrade.ID))
            {
                x++; 
            }
        }

        return x; 
    }

    public void SpawnInvestments(Transform container)
    {
        foreach (Investment investment in investments)
        {
            InvestmentButton button = Instantiate(investmentPrefab, container).GetComponent<InvestmentButton>();
            button.Populate(investment.displayName, 0, investment);
            investmentQuantities.Add(investment.displayName, 0);
            investmentButtons.Add(button);
        }

        scrollbar.value = 1;
    }

    public void PopulateUpgradeRequirements()
    {
        foreach (InvestmentButton button in investmentButtons)
        {
            foreach (UpgradeRequirement upgradRequirment in button.GetInvestment().upgradeRequirements)
            {
                InvestmentUpgradeRequirments.Add(upgradRequirment.ID, upgradRequirment);
                UpgradeInvestment.Add(upgradRequirment.ID, button.GetInvestment());
            }
        }
    }

    public void PopulateMerchSelection()
    {
        for(int i = 0; i < merch.Length; i++)
        {
            merchSets.Add(i, merch[i]); 
        }

        UIManager.Instance.PopulateMerch();
        UpdateMerchSelection(0);
    }

    public Sprite GetMerchInvestmentIcon(int index)
    {
        return merchSets[index].investmentIcon;
    }

    public void UpdateMerchSelection(int index)
    {
        UIManager.Instance.UpdatePortrait(merchSets[index].portriatIcon);
        PlayerManager.Instance.SetMerchIndex(index);
    }

    public void IncreaseInvestment(Investment investment)
    {
        investmentQuantities[investment.displayName]++;
    }

    public void UpdateInvestmentPanel(Investment investment)
    {
        if(!InvestmentPanels.ContainsKey(investment))
        {
            InvestmentPanel investmentPanel = Instantiate(investmentPanelPrefab, displayContainer).GetComponent<InvestmentPanel>();
            investmentPanel.PopulateInvestmentPanel(investment);
            InvestmentPanels.Add(investment, investmentPanel);
            print("adding the thing");
        }

        InvestmentPanels[investment].AddInvestment(); 
    }

    public void ActivateAvalibleUpgrades()
    {
        foreach (KeyValuePair<int, UpgradeRequirement> key in InvestmentUpgradeRequirments)
        {
            if (!spawnedUpgradeIDs.Contains(key.Key))
            {
                if (key.Value.quantityRequirement <= GetInvestmentQuantity(UpgradeInvestment[key.Key]))
                {
                    UpgradeButton button = Instantiate(upgradePrefab, upgradeContainer).GetComponent<UpgradeButton>();
                    button.Populate(key.Value, UpgradeInvestment[key.Key]);
                    spawnedUpgradeIDs.Add(key.Key);
                }
            }
        }
    }
    
    public void AddPuchasedUpgradeID(int ID)
    {
        purchasedUpgradeIDs.Add(ID);
    }

}

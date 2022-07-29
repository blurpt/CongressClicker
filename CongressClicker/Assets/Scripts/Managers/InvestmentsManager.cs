using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class InvestmentsManager : Singleton<InvestmentsManager>
{
    public Investment[] investments;
    public MerchSet[] merch; 
    public GameObject investmentPrefab, upgradePrefab, investmentPanelPrefab, scrollbarCover;
    public Transform upgradeContainer, displayContainer;
    public Scrollbar investmentButtonsScrollbar, investmentFeedbackScrollbar;
    private List<InvestmentButton> investmentButtons = new List<InvestmentButton>();
    public List<InvestmentPanel> investmentPanelList = new List<InvestmentPanel>();
    private Dictionary<string, int> investmentQuantities = new Dictionary<string, int>();
    private Dictionary<int, UpgradeRequirement> InvestmentUpgradeRequirments = new Dictionary<int, UpgradeRequirement>();
    private Dictionary<int, Investment> UpgradeInvestment = new Dictionary<int, Investment>();
    public Dictionary<Investment, InvestmentPanel> InvestmentPanels = new Dictionary<Investment, InvestmentPanel>();
    private Dictionary<int, MerchSet> merchSets = new Dictionary<int, MerchSet>();
    private Dictionary<Investment, float> ScrollBarMap = new Dictionary<Investment, float>();


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

    public void PopulateInvestmentPanels(Transform container)
    {
        foreach (Investment investment in investments)
        {
            InvestmentPanel investmentPanel = Instantiate(investmentPanelPrefab, displayContainer).GetComponent<InvestmentPanel>();
            investmentPanelList.Add(investmentPanel);
        }
    }

    public void PopulateInvestmentButtons(Transform container)
    {
        foreach (Investment investment in investments)
        {
            InvestmentButton button = Instantiate(investmentPrefab, container).GetComponent<InvestmentButton>();
            button.Populate(investment.displayName, 0, investment);
            investmentQuantities.Add(investment.displayName, 0);
            investmentButtons.Add(button);
        }
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

    public void AddInvestmentToPanel(Investment investment)
    {
        if(!InvestmentPanels.ContainsKey(investment))
        {
            investmentPanelList[0].PopulateInvestmentPanel(investment);
            InvestmentPanels.Add(investment, investmentPanelList[0]);
            SetScrollBarMap(investment, InvestmentPanels.Count);
            investmentPanelList.RemoveAt(0);

            if(InvestmentPanels.Count > 2)
            {
                scrollbarCover.SetActive(false);
            }
        }

        UIManager.Instance.investmentInstructions.SetActive(false);
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
                    UIManager.Instance.upgradeInstructions.SetActive(false);
                }
            }
        }
    }
    
    public void AddPuchasedUpgradeID(int ID)
    {
        purchasedUpgradeIDs.Add(ID);
    }

    private void SetScrollBarMap(Investment investment, int index)
    {
        switch(index)
        {
            case 1:
                ScrollBarMap.Add(investment, 1);
                break;
            case 2:
                ScrollBarMap.Add(investment, 1);
                break;
            case 3:
                ScrollBarMap.Add(investment, .75f);
                break;
            case 4:
                ScrollBarMap.Add(investment, .5f);
                break;
            case 5:
                ScrollBarMap.Add(investment, .25f);
                break;
            case 6:
                ScrollBarMap.Add(investment, 0);
                break;
            case 7:
                ScrollBarMap.Add(investment, 0);
                break;
        }
    }

    public void FocusInvestment(Investment investment)
    {
        investmentFeedbackScrollbar.value = ScrollBarMap[investment];
    }

}

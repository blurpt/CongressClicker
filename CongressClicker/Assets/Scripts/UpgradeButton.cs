using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Image image;
    public GameObject button;
    public Text price; 
    public AudioClip purchaseSound, cantAfford;
    private UpgradeRequirement upgradeRequirment;
    private Investment investment; 

    public void Populate(UpgradeRequirement UpgradeRequirement, Investment Investment)
    {
        upgradeRequirment = UpgradeRequirement;
        price.text = upgradeRequirment.cost.ToString();
        investment = Investment;
        image.sprite = upgradeRequirment.icon;
    }
    
    public void Destroy()
    {
        if(upgradeRequirment.CanAfford(investment))
        {
            InvestmentsManager.Instance.AddPuchasedUpgradeID(upgradeRequirment.ID);
            SoundManager.Instance.PlayUISound(purchaseSound);
            Destroy(gameObject);
        }
        else
        {
            SoundManager.Instance.PlayUISound(cantAfford);
        }

    }
    public UpgradeRequirement GetUpgradeRequirment()
    {
        return upgradeRequirment; 
    }

    public Investment GetInvestment()
    {
        return investment;
    }
}

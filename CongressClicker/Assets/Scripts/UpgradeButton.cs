using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Image image;
    public GameObject button;
    public AudioClip purchaseSound;
    private UpgradeRequirement upgradeRequirment;
    private Investment investment; 

    public void Populate(UpgradeRequirement UpgradeRequirement, Investment Investment)
    {
        upgradeRequirment = UpgradeRequirement;
        investment = Investment; 
    }
    
    public void Destroy()
    {
        InvestmentsManager.Instance.AddPuchasedUpgradeID(upgradeRequirment.ID);
        SoundManager.Instance.PlayUISound(purchaseSound);
        Destroy(gameObject); 
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

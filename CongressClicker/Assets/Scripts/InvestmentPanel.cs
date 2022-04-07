using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentPanel : MonoBehaviour
{
    public Transform columnContainer, rowContainer, singleRowContainer;
    public Image backgroundImage;
    public GameObject iconPrefab;
    public ScrollRect scrollRect; 
    private Investment investment;

    public void PopulateInvestmentPanel(Investment Investment)
    {
        investment = Investment;
        backgroundImage.sprite = investment.feedbackPanel_Background;
    }

    public void AddInvestment()
    {
        Transform spawnLoaction = null;

        switch (investment.spawnType)
        {
            case Investment.SpawnType.column:
                spawnLoaction = columnContainer;
                break;
            case Investment.SpawnType.row:
                spawnLoaction = rowContainer;
                break;
            case Investment.SpawnType.singleRow:
                spawnLoaction = singleRowContainer;
                break;
        }

        InvestmentFeedbackPanelIcon icon = Instantiate(iconPrefab, spawnLoaction).GetComponent<InvestmentFeedbackPanelIcon>();
        icon.PopulateIcon(investment);
    }
}

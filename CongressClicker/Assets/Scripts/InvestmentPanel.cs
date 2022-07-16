using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentPanel : MonoBehaviour
{
    public HorizontalLayoutGroup rowContainer;
    public Image backgroundImage;
    public GameObject iconPrefab;
    public ScrollRect scrollRect;
    public Scrollbar scrollbar;
    private Investment investment;


    public void PopulateInvestmentPanel(Investment Investment)
    {
        investment = Investment;
        backgroundImage.sprite = investment.feedbackPanel_Background;
    }

    public void AddInvestment()
    {
        HorizontalLayoutGroup spawnLoaction = rowContainer;
        spawnLoaction.spacing = investment.cellSize.x + 2;
        InvestmentFeedbackPanelIcon icon = Instantiate(iconPrefab, spawnLoaction.transform).GetComponent<InvestmentFeedbackPanelIcon>();
        icon.PopulateIcon(investment);
    }

}

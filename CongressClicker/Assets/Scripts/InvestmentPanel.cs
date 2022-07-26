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
    public Text voterIncomeText;
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
        voterIncomeText.text = VoterIncome(investment);
    }

    public string VoterIncome(Investment investment)
    {
        int voters = investment.votesPerCache * InvestmentsManager.Instance.GetInvestmentQuantity(investment);
        for (int i = 0; i < InvestmentsManager.Instance.InvestmentUpgradesAquired(investment); i++)
        {
            voters = voters * 2;
        }
        return voters.ToString() + " Voters every " + investment.secondsToCache + " seconds";
    }

}

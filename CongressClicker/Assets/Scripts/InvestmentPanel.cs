using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentPanel : MonoBehaviour
{
    public GridLayoutGroup columnContainer, rowContainer, singleRowContainer;
    public Image backgroundImage;
    public GameObject iconPrefab;
    public ScrollRect scrollRect; 
    private Investment investment;
    private RectTransform containerToSpawn;
    private Vector2 startingPos;


    public void Update()
    {
        if (!containerToSpawn && !investment) return; 

        if(containerToSpawn.anchoredPosition.normalized != startingPos.normalized)
        {
            if (GetParentContainer(investment) == singleRowContainer)
            {
                containerToSpawn.anchoredPosition = containerToSpawn.anchoredPosition.normalized + investment.offSet.normalized;
            }
        }
    }

    public void PopulateInvestmentPanel(Investment Investment)
    {
        investment = Investment;
        backgroundImage.sprite = investment.feedbackPanel_Background;
    }

    public void AddInvestment()
    {
        GridLayoutGroup spawnLoaction = GetParentContainer(investment);
        spawnLoaction.cellSize = investment.cellSize;
        containerToSpawn = spawnLoaction.GetComponent<RectTransform>();
        InvestmentFeedbackPanelIcon icon = Instantiate(iconPrefab, spawnLoaction.transform).GetComponent<InvestmentFeedbackPanelIcon>();
        icon.PopulateIcon(investment);
        startingPos = containerToSpawn.anchoredPosition + investment.offSet;
    }

    private GridLayoutGroup GetParentContainer(Investment investment)
    {
        switch (investment.spawnType)
        {
            case Investment.SpawnType.column:
                return columnContainer;
            case Investment.SpawnType.row:
                return rowContainer;
            case Investment.SpawnType.singleRow:
                return singleRowContainer;
        }
        return columnContainer;

    }
}

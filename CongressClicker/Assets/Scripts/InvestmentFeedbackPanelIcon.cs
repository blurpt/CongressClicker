using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentFeedbackPanelIcon : MonoBehaviour
{
    public Image icon;
    private Investment investment; 

    public void PopulateIcon(Investment Investment)
    {
        investment = Investment;
        icon.sprite = investment.GetIcon();
        icon.transform.position = offsetPosition(icon.transform);
        icon.rectTransform.sizeDelta = new Vector2(investment.cellSize.x, investment.cellSize.y);
    }

    private Vector3 offsetPosition(Transform iconTransform)
    {
        return new Vector3(iconTransform.position.x + investment.offSet.x, iconTransform.position.y + investment.offSet.y, 0);
    }

}

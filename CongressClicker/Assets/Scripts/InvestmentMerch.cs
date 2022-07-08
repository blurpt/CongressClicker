using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/InvestmentMerch")]

public class InvestmentMerch : Investment
{
    public override Sprite GetIcon()
    {
        return InvestmentsManager.Instance.GetMerchInvestmentIcon(PlayerManager.Instance.GetMerchIndex());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchPopUp : MonoBehaviour
{
    public Transform popUp, shirtPanel;
    public GameObject merchUISelectionPrefab;
    public ToggleGroup toggleGroup;
    
    public void Populate()
    {
        int i = 0;
        foreach(MerchSet merchSet in InvestmentsManager.Instance.merch)
        {
            MerchUISelection spawnedMerchUI = Instantiate<GameObject>(merchUISelectionPrefab, shirtPanel).GetComponent<MerchUISelection>();
            spawnedMerchUI.Populate(merchSet.investmentIcon, toggleGroup, i);
            i++;
        }
    }

    public void Enable()
    {
        popUp.transform.gameObject.SetActive(true);
    }

    public void SelectShirt()
    {
        InvestmentsManager.Instance.UpdateMerchSelection(toggleGroup.GetFirstActiveToggle().GetComponent<MerchUISelection>().index);
        Disable();
    }
    private void Disable()
    {
        popUp.transform.gameObject.SetActive(false);
    }
}

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
        icon.transform.position = icon.transform.position + AddNoise(investment.noiseMin, investment.noiseMax); 
    }
    private Vector3 AddNoise(float min, float max)
    {
 
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);

        Vector3 noise = new Vector3
            (
                Mathf.Sin(2 * Mathf.PI * xNoise / 360),
                Mathf.Sin(2 * Mathf.PI * yNoise / 360),
                0
            );
        return noise;
    }
}

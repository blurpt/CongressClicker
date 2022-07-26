using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaphone : MonoBehaviour
{
    public int gainedVoters;
    public void UseMegaphone()
    {
        if(GameManager.Instance.GetTotalVoters() == 0)
        {
            UIManager.Instance.TurnOffMegaphoneInstructions();
            UIManager.Instance.investmentInstructions.SetActive(true);
        }
        GameManager.Instance.AddVoters(gainedVoters);
    }
}

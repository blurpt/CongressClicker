using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : Singleton<UIManager>
{
    public Text voterLable, levelLable, upgradeInfo;
    public Transform investmentContainer;
    public QuizPopUp quizPopUp;
    public MerchPopUp merchPopUp;
    public Slider promotionProgressBar;
    public Image portrait;
    public GameObject megaphoneInstructions, megaphoneGlow, investmentInstructions, upgradeInstructions;


    public void UpdateVoterCount(int num)
    {
        voterLable.text = GetVotersLableText(num); 
    }
    public void PopulateMerch()
    {
        merchPopUp.Populate();
    }

    public void SetPlayerLevelUI(PlayerLevel playerLevel)
    {
        levelLable.text = "LvL" + PlayerManager.Instance.GetLevelFromPLD(playerLevel) + ": " + playerLevel.displayName; 
    }

    public void StartQuiz(PlayerLevel playerLevel)
    {
        quizPopUp.Enable(playerLevel.quizQuestions, playerLevel.requiredQuizGrade, playerLevel.displayName); 
    }

    public void SetPromotionProgressBar(float progress)
    {
        promotionProgressBar.value = progress; 
    }

    public void UpdatePortrait(Sprite icon)
    {
        portrait.sprite = icon; 
    }

    public void TurnOffMegaphoneInstructions()
    {
        megaphoneInstructions.SetActive(false);
        megaphoneGlow.SetActive(false);
    }

    public void UpdateUpgradeInfo(Investment investment)
    {
        upgradeInfo.text = "Double voters earned from " + investment.displayName + "!"; 
    }

    public void ClearUpgradeInfo()
    {
        upgradeInfo.text = ""; 
    }

    private string GetVotersLableText(int voters)
    {
        return "Voters: " + voters.ToString(); 
    }
}

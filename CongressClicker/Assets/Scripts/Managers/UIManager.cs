using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : Singleton<UIManager>
{
    public Text voterLable, levelLable;
    public Transform investmentContainer;
    public QuizPopUp quizPopUp;
    public MerchPopUp merchPopUp;
    public Slider promotionProgressBar;
    public Image portrait;


    public void UpdateVoterCount(int num)
    {
        voterLable.text = GetVotersLableText(num); 
    }
    public void PopulateInvestments()
    {
        InvestmentsManager.Instance.SpawnInvestments(investmentContainer);
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

    private string GetVotersLableText(int voters)
    {
        return "Voters: " + voters.ToString(); 
    }
}

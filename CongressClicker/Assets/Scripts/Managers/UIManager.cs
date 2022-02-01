using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : Singleton<UIManager>
{
    public Text voterLable, levelLable;
    public Transform investmentContainer;
    public QuizPopUp quizPopUp;
    public Slider promotionProgressBar; 

    public void UpdateVoterCount(int num)
    {
        voterLable.text = GetVotersLableText(num); 
    }
    public void PopulateInvestments()
    {
        InvestmentsManager.Instance.SpawnInvestments(investmentContainer);
    }

    public void SetPlayerLevelUI(PlayerLevel playerLevel)
    {
        levelLable.text = "LvL" + PlayerManager.Instance.GetLevelFromPLD(playerLevel) + ": " + playerLevel.displayName; 
    }

    public void StartQuiz(PlayerLevel playerLevel)
    {
        quizPopUp.Enable(playerLevel.quizQuestions, playerLevel.requiredQuizGrade); 
    }

    public void SetPromotionProgressBar(float progress)
    {
        promotionProgressBar.value = progress; 
    }

    private string GetVotersLableText(int voters)
    {
        return "Voters: " + voters.ToString(); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPopUp : MonoBehaviour
{
    public Text questionText, voterBonusText, title;
    public Transform popUp, answerParent, qustionMarkerParent, submitButton, closeButton, voterBonusParent, incorrectAnswerParent; 
    public GameObject answerPrefab, questionMarkerPrefab;
    public ToggleGroup answerToggleGroup;
    public AudioClip correct, incorrect, levelUp; 
    private string quizFailedText = "Didn't get the job...";
    private string levelName;
    private float requiredGrade;
    private int currentQuizQuestionIndex, correctAnswers;
    private QuizQuestion[] questions;
    
    public void SubmitAnswer()
    {
        submitButton.gameObject.SetActive(false);
        StartCoroutine("InternalSubmitAnswer"); 
    }

    private IEnumerator InternalSubmitAnswer()
    {

        if (answerToggleGroup.GetFirstActiveToggle().GetComponentInParent<AnswerLabel>().GetCorrectAnswer())
        {
            CorrectAnswer();
            SoundManager.Instance.PlayUISound(correct);
            yield return new WaitForSeconds(2);
            NextQuestion();

        }
        else
        {
            IncorrectAnswer();
            SoundManager.Instance.PlayUISound(incorrect);
            yield return new WaitForSeconds(2);
            incorrectAnswerParent.gameObject.SetActive(false);
        }
        submitButton.gameObject.SetActive(true);

    }

    public void Enable(QuizQuestion[] quizQuestions, float RequiredGrade, string level)
    {
        questions = quizQuestions;
        requiredGrade = RequiredGrade; 
        currentQuizQuestionIndex = 0;
        submitButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        PopulateQuestion(quizQuestions[currentQuizQuestionIndex]);
        popUp.transform.gameObject.SetActive(true);
        levelName = level;
        title.text = "Get questions right to earn a promotion!";

    }

    public void Disable()
    {
        popUp.transform.gameObject.SetActive(false);
        GameManager.Instance.EndQuiz(true);
        Clear();
        submitButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    private void Clear()
    {
        questionText.text = "";
        voterBonusParent.gameObject.SetActive(false);
        foreach(RectTransform child in answerParent)
        {
            child.gameObject.SetActive(false);
        }
        foreach (RectTransform child in qustionMarkerParent)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void PopulateQuestion(QuizQuestion quizQuestion)
    {
       Clear();
        questionText.text = quizQuestion.question;
        for(int i = 0; i < quizQuestion.answers.Length; i++)
        {
            AnswerLabel answer = Instantiate(answerPrefab, answerParent).GetComponent<AnswerLabel>();
            answer.Populate(quizQuestion.answers[i], answerToggleGroup, i == quizQuestion.answerIndex);
        }

    }

    private void CorrectAnswer()
    {
        GameManager.Instance.AddVoters(10); 
        voterBonusParent.gameObject.SetActive(true);
        correctAnswers++; 
        print("correct!");
    }

    private void IncorrectAnswer()
    {
        print("incorrect!");
        incorrectAnswerParent.gameObject.SetActive(true);
    }

    private void NextQuestion()
    {
        if (currentQuizQuestionIndex == questions.Length - 1)
        {
            QuizCompleted();
            return;
        }

        currentQuizQuestionIndex++;
        PopulateQuestion(questions[currentQuizQuestionIndex]);
    }

    private void QuizCompleted()
    {
        Clear();
        bool passed = (correctAnswers / questions.Length) >= requiredGrade; 

        if(passed)
        {
            title.text = "Congratulations!";
            questionText.text = "Promoted to " + levelName + "!";
            SoundManager.Instance.PlayUISound(levelUp);
        }
        else
        {
            questionText.text = quizFailedText;
        }
        SoundManager.Instance.StopMusic();
        submitButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        PlayerManager.Instance.Promote();
        
        if (PlayerManager.Instance.IsMaxLevel())
        {
            title.text = "YOU WON!";
            questionText.text = "Congratulations! You have earned your final promotion! " + levelName + "! You have reached the highest position in the galaxy! You can continue to earn more voters, or take a break and enjoy your new found powers.";
        }
    }
}

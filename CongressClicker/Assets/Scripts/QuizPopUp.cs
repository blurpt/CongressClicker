using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPopUp : MonoBehaviour
{
    public Text questionText;
    public Transform popUp, answerParent, qustionMarkerParent, submitButton, closeButton; 
    public GameObject answerPrefab, questionMarkerPrefab;
    public ToggleGroup answerToggleGroup;
    private string quizPassedText = "Debate Won!";
    private string quizFailedText = "Debate Lost...";
    private float requiredGrade;
    private int currentQuizQuestionIndex, correctAnswers;
    private QuizQuestion[] questions; 
    
    public void SubmitAnswer()
    {
        if (answerToggleGroup.GetFirstActiveToggle().GetComponentInParent<AnswerLabel>().GetCorrectAnswer())
        {
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }

        NextQuestion(); 
    }

    public void Enable(QuizQuestion[] quizQuestions, float RequiredGrade)
    {
        questions = quizQuestions;
        requiredGrade = RequiredGrade; 
        currentQuizQuestionIndex = 0;
        submitButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        PopulateQuestion(quizQuestions[currentQuizQuestionIndex]);
        popUp.transform.gameObject.SetActive(true);
    }

    public void Disable()
    {
        popUp.transform.gameObject.SetActive(false);
        GameManager.Instance.EndQuiz(questionText.text == quizPassedText);
        Clear();
        submitButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    private void Clear()
    {
        questionText.text = "";
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
        correctAnswers++; 
        print("correct!");
    }

    private void IncorrectAnswer()
    {
        print("incorrect!");
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
        print("Quiz Complete!");
        Clear();
        bool passed = (correctAnswers / questions.Length) >= requiredGrade; 

        if(passed)
        {
            questionText.text = quizPassedText;
        }
        else
        {
            questionText.text = quizFailedText;
        }
        submitButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
    }
}

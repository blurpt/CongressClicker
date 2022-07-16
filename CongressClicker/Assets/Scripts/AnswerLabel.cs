using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswerLabel : MonoBehaviour
{
    public Text text;
    public Toggle toggle;
    public AudioClip selected;
    private bool correctAnswer;
    public void Populate(string Text, ToggleGroup ToggleGroup, bool CorrectAnswer)
    {
        text.text = Text;
        toggle.group = ToggleGroup;
        correctAnswer = CorrectAnswer; 
    }

    public void Select()
    {
        SoundManager.Instance.PlayUISound(selected);
    }
    public bool GetCorrectAnswer()
    {
        return correctAnswer;
    }
}

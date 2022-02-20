using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerLevel")]
public class PlayerLevel : ScriptableObject
{
    public string displayName;
    public Image playerAvatar;
    public int voterRequirement;
    public QuizQuestion[] quizQuestions;
    public float requiredQuizGrade;
}

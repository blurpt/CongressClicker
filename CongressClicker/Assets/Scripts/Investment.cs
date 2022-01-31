using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[CreateAssetMenu(menuName = "Scriptable Object/Investment")]
public class Investment : ScriptableObject
{
    public string displayName;
    public int unlockLevel;
    public int startingCost;
    public int votesPerCache;
    public float secondsToCache; 
    public Image buttonIcon;
    public UpgradeRequirement[] upgradeRequirements;
    public Image feedbackPanel_Background;
    public Image[] feedbackPanel_ObjectsToPopulate;
}

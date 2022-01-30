using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Investment")]
public class Investment : ScriptableObject
{
    public string displayName;
    public int unlockLevel;
    public int startingCost;
    public int votesPerCache;
    public float secondsToCache; 
    public Sprite icon;
    public UpgradeRequirement[] upgradeRequirements;
}

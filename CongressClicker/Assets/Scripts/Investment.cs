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
    public Sprite feedbackPanel_Background;
    public Sprite[] feedbackPanel_ObjectsToPopulate;
    public int noiseMin, noiseMax;
    public Vector2 cellSize;
    public Vector2 offSet; 
    public enum SpawnType { column, row, singleRow };
    public SpawnType spawnType;

    public Sprite GetIcon()
    {
        return feedbackPanel_ObjectsToPopulate[Random.Range(0, feedbackPanel_ObjectsToPopulate.Length)];   
    }
}

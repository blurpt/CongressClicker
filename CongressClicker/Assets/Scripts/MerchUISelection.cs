using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchUISelection : MonoBehaviour
{
    public Toggle toggle;
    public Image icon;
    public int index; 

    public void Populate(Sprite Icon, ToggleGroup ToggleGroup, int Index)
    {
        icon.sprite = Icon;
        toggle.group = ToggleGroup;
        index = Index;
    }

    public void SelectToggle(AudioClip audioClip)
    {
        SoundManager.Instance.PlayUISound(audioClip); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSourceUI;
    public AudioClip purchaseInvestmentSound;
    public void PlayUISound(AudioClip clip)
    {
        audioSourceUI.PlayOneShot(clip); 
    }

    public void PlayPurchaseInvestmentSound()
    {
        SoundManager.Instance.PlayUISound(purchaseInvestmentSound);
    }
}

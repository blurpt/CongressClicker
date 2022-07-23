using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSourceUI, musicSource;
    public AudioClip backgroundMusic, quizMusic;
    public void PlayUISound(AudioClip clip)
    {
        audioSourceUI.PlayOneShot(clip); 
    }

    public void PlayBackgroundMusic()
    {
        musicSource.Stop();
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayQuizMusic()
    {
        musicSource.Stop();
        musicSource.clip = quizMusic;
        musicSource.Play();
    }


}

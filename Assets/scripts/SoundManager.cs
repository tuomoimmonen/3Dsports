using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource[] soundFXsToPlay;


    int musicIndex = 0;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayAudioNotPitched(int index)
    {
        soundFXsToPlay[index].pitch = 1f;
        soundFXsToPlay[index].Play();
    }

    public void PlayAudio(int index)
    {
        //StopSFX();
        float randomPitch = UnityEngine.Random.Range(0.5f, 1.2f);
        soundFXsToPlay[index].pitch = randomPitch;
        soundFXsToPlay[index].Play();
    }

    public void StopSFX()
    {
        for (int i = 0; i < soundFXsToPlay.Length; i++)
        {
            soundFXsToPlay[i].Stop();
        }
    }
}

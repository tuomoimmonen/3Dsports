using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource[] soundFXsToPlay;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip[] gameMusic;

    AudioSource musicSource;

    int musicIndex = 0;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        musicSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        musicSource.Play();
    }

    private void Update()
    {
        if (!musicSource.isPlaying && SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayNextSong();
        }
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //scene gets name, buildindex
    //loadscenemode = single or additive ei vaikuta
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip targetClip = null;

        if (scene.buildIndex == 0)
        {
            targetClip = mainMenuMusic;
            //musicSource.volume = 0.1f;
            musicSource.loop = true;
        }
        else if (scene.buildIndex != 0)
        {
            targetClip = gameMusic[musicIndex];
            //musicSource.volume = 0.05f;
            musicSource.loop = false;
        }

        if (musicSource.clip != targetClip)
        {
            musicSource.clip = targetClip;
            musicSource.Play();
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % gameMusic.Length;
        musicSource.clip = gameMusic[musicIndex];
        musicSource.Play();
    }
}

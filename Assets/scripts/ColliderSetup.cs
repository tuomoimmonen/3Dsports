using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderSetup : MonoBehaviour
{
    [SerializeField] float waitBeforeLoadingNextLevel = 5f;


    public GameEvent sceneFinished;

    int sceneIndex;

    bool playerFinished = false;

    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.gameStarted = false;
            playerFinished = true;

            if(sceneIndex == 1) 
            {
                HighScoresText.instance.AddScores(player.runTime, sceneIndex); 
                HighScoresText.instance.UpdateScoreText();
                HighScoresText.instance.UpdateCurrentScoreText(player.runTime, sceneIndex);
            }

            sceneFinished.Raise(this, playerFinished);
            LoadGameOverPanel();
            //StartCoroutine(LoadNextLevel());
        }
    }

    private void LoadGameOverPanel()
    {
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(waitBeforeLoadingNextLevel);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}

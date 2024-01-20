using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderSetup : MonoBehaviour
{
    [SerializeField] float waitBeforeLoadingNextLevel = 5f;


    public GameEvent sceneFinished;
    //public GameEvent playerOverStepped;
    public GameEvent changeCanMoveBool;

    int sceneIndex;

    bool playerFinished = false;
    bool playerCanMove = true;

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
            playerCanMove = false;
            playerFinished = true;

            if(sceneIndex == 1) 
            {
                HighScoresText.instance.AddScores(player.runTime, sceneIndex);
                HighScoresText.instance.UpdateScoreText();
                HighScoresText.instance.UpdateCurrentScoreText(player.runTime, sceneIndex);
                sceneFinished.Raise(this, playerFinished);
                changeCanMoveBool.Raise(this, playerCanMove);
            }
            else if ((sceneIndex == 2) || (sceneIndex == 3)) 
            {
                //sceneFinished.Raise(this, playerFinished);
                //playerOverStepped.Raise(this, playerFinished);
                changeCanMoveBool.Raise(this, playerCanMove);
            }

        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(waitBeforeLoadingNextLevel);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}

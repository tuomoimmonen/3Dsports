using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject gameOverPanel;
    void Start()
    {
        finishPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        
    }
    
    private void ShowFinishPanel(bool showGameFinishPanel) { finishPanel.SetActive(showGameFinishPanel);}

    private void ShowGameOverPanel(bool showGameOverPanel) { gameOverPanel.SetActive(showGameOverPanel);}

    public void HandleGameFinishedPanel(Component sender, object data)
    {
        if(data is bool)
        {
            bool showGameFinishedPanel = (bool)data;
            //ShowFinishPanel(showGameFinishedPanel);
            StartCoroutine(StartLoadingFinishPanel(showGameFinishedPanel));
        }
    }

    public void HandleGameOverPanel(Component sender, object data)
    {
        if (data is bool)
        {
            bool showGameOverPanel = (bool)data;
            ShowGameOverPanel(showGameOverPanel);
        }
    }
    

    public void NextLevelButton()
    {
        StartCoroutine(StartLoadingNextLevel());
    }

    public void ChooseNextLevelButton(int sceneIndex)
    {
        StartCoroutine(LoadThePickedLevel(sceneIndex));
    }

    IEnumerator LoadThePickedLevel(int sceneIndex)
    {
        SoundManager.instance.PlayAudio(0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void TryAgainButton()
    {
        StartCoroutine(ReloadTheScene());
    }
    IEnumerator StartLoadingNextLevel()
    {
        SoundManager.instance.PlayAudio(0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    IEnumerator ReloadTheScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator StartLoadingFinishPanel(bool showFinishPanel)
    {
        yield return new WaitForSeconds(1.5f);
        ShowFinishPanel(showFinishPanel);
    }
}

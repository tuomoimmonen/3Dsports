using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject finishPanel;
    void Start()
    {
        finishPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    private void ShowPanel(bool showPanel)
    {
        finishPanel.SetActive(showPanel);
    }

    public void HandleGameFinishedPanel(Component sender, object data)
    {
        if(data is bool)
        {
            bool showPanel = (bool)data;
            ShowPanel(showPanel);
        }
    }

    public void NextLevelButton()
    {
        StartCoroutine(StartLoadingNextLevel());
    }

    IEnumerator StartLoadingNextLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}

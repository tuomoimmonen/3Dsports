using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject levelPanel;
    public void StartGame(int sceneIndex)
    {
        StartCoroutine(StartLoadingLevel(sceneIndex));
    }

    private IEnumerator StartLoadingLevel(int sceneIndex)
    {
        SoundManager.instance.PlayAudio(0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHighScoresButton()
    {
        SoundManager.instance.PlayAudio(0);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void ToggleSettingsPanel()
    {
        SoundManager.instance.PlayAudio(0);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleLevelPanel()
    {
        SoundManager.instance.PlayAudio(0);
        levelPanel.SetActive(!levelPanel.activeSelf);
    }


}

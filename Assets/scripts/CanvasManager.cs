using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager instance;

    [SerializeField] GameObject speedText;
    [SerializeField] GameObject javelinDistanceText;
    [SerializeField] GameObject jumpDistanceText;
    [SerializeField] GameObject runTimeText;

    [SerializeField] GameObject jumpButton;
    [SerializeField] GameObject runButton;
    [SerializeField] GameObject throwButton;

    int sceneIndex = 0;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        sceneIndex = UpdateSceneIndex();
    }
    void Start()
    {
        UpdateTextObjectsOnScreen();
    }

    void Update()
    {

    }
    
    private void UpdateTextObjectsOnScreen()
    {
        switch (sceneIndex)
        {
            case 0:
                runButton.SetActive(false);
                jumpButton.SetActive(false);
                throwButton.SetActive(false);
                break;

            case 1:
                runButton.SetActive(true);
                jumpButton.SetActive(false);
                throwButton.SetActive(false);
                break;

            case 2:
                runButton.SetActive(false);
                jumpButton.SetActive(true);
                throwButton.SetActive(false);
                break;

            case 3:
                runButton.SetActive(false);
                jumpButton.SetActive(false);
                throwButton.SetActive(true);
                break;

        }
    }

    private int UpdateSceneIndex()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        return sceneIndex;
    }
}

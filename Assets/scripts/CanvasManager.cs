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
                runTimeText.SetActive(false);
                speedText.SetActive(false);
                javelinDistanceText.SetActive(false);
                jumpDistanceText.SetActive(false);
                runButton.SetActive(false);
                jumpButton.SetActive(false);
                throwButton.SetActive(false);
                break;

            case 1:
                runTimeText.SetActive(true);
                speedText.SetActive(true);
                runButton.SetActive(true);
                jumpButton.SetActive(false);
                throwButton.SetActive(false);
                javelinDistanceText.SetActive(false);
                jumpDistanceText.SetActive(false);
                break;

            case 2:
                runTimeText.SetActive(false);
                speedText.SetActive(true);
                runButton.SetActive(false);
                jumpButton.SetActive(true);
                throwButton.SetActive(false);
                javelinDistanceText.SetActive(false);
                jumpDistanceText.SetActive(true);
                break;

            case 3:
                runTimeText.SetActive(false);
                speedText.SetActive(true);
                runButton.SetActive(false);
                jumpButton.SetActive(false);
                throwButton.SetActive(true);
                javelinDistanceText.SetActive(true);
                jumpDistanceText.SetActive(false);
                break;

        }
    }

    private int UpdateSceneIndex()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        return sceneIndex;
    }
}

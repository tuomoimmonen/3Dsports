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
                break;

            case 1:
                runTimeText.SetActive(true);
                speedText.SetActive(true);
                javelinDistanceText.SetActive(false);
                jumpDistanceText.SetActive(false);
                break;

            case 2:
                runTimeText.SetActive(false);
                speedText.SetActive(true);
                javelinDistanceText.SetActive(false);
                jumpDistanceText.SetActive(true);
                break;

            case 3:
                runTimeText.SetActive(false);
                speedText.SetActive(true);
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

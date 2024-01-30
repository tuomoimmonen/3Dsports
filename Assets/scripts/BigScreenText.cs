using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigScreenText : MonoBehaviour
{
    [SerializeField] TMP_Text bigScreenRunTimeText;
    [SerializeField] TMP_Text bigScreenJumpDistanceText;
    [SerializeField] TMP_Text bigScreenJavelinDistanceText;

    int sceneIndex;

    bool showSpeedText;
    bool showJumpDistanceText;
    bool showJavelinDistanceText;
    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        switch (sceneIndex)
        {
            case 1:
                bigScreenRunTimeText.enabled = true;
                bigScreenJumpDistanceText.enabled = false;
                bigScreenJavelinDistanceText.enabled = false;
                break;
            case 2:
                bigScreenRunTimeText.enabled = false;
                bigScreenJumpDistanceText.enabled = true;
                bigScreenJavelinDistanceText.enabled = false;
                break;
            case 3:
                bigScreenRunTimeText.enabled = false;
                bigScreenJumpDistanceText.enabled = false;
                bigScreenJavelinDistanceText.enabled = true;
                break;
        }
    }

    void Update()
    {
        
    }

    public void SetSpeedText(Component sender, object data)
    {
        if(data is float)
        {
            float time = (float)data;
            ChangeRunTimeText(time);
        }
    }

    void ChangeRunTimeText(float time)
    {
        bigScreenRunTimeText.text = time.ToString("F") + "s";
    }

    public void SetJumpDistanceText(Component sender, object data)
    {
        if (data is float)
        {
            float distance = (float)data;
            ChangeJumpDistanceText(distance);
        }
    }

    void ChangeJumpDistanceText(float distance)
    {
        bigScreenJumpDistanceText.text = distance.ToString("F") + "m";
    }

    public void SetJavelinDistanceText(Component sender, object data)
    {
        if(data is float)
        {
            float distance = (float)data;
            ChangeJavelinDistanceText(distance);
        }
    }

    void ChangeJavelinDistanceText(float distance)
    {
        bigScreenJavelinDistanceText.text = distance.ToString("F") + "m";
    }
}

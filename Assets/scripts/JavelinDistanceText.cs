using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JavelinDistanceText : MonoBehaviour
{
    [SerializeField] TMP_Text javelinDistanceText;
    private void Awake()
    {
        SetJavelinDistanceText(0);
    }

    void SetJavelinDistanceText(float distance)
    {
        javelinDistanceText.text = "JavelinDistance: " + distance.ToString("F") + "m";
    }

    public void ChangeJavelinDistanceText(Component sender, object data)
    {
        if(data is float)
        {
            float distance = (float)data;
            SetJavelinDistanceText(distance);
        }
    }
}

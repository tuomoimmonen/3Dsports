using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuntimeText : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;

    private void Awake()
    {
        SetTimeText(0);
    }

    private void SetTimeText(float time)
    {
        timeText.text = "Time: " + time.ToString("F") + "s";
    }

    public void ChangeTimeText(Component sender, object data)
    {
        if(data is float)
        {
            float time = (float)data;
            SetTimeText(time);
        }
    }
}

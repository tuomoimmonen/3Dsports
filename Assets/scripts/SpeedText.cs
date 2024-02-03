using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedText : MonoBehaviour
{
    [SerializeField] TMP_Text speedText;

    private void Awake()
    {
        SetSpeedText(0);
    }

    private void SetSpeedText(float speed)
    {
        speedText.text = "Speed: " + speed.ToString("F1") + "km/h";
    }

    public void UpdateSpeedText(Component sender, object data)
    {
        if(data is float)
        {
            float speed = (float)data;
            SetSpeedText(speed);
        }
    }
}

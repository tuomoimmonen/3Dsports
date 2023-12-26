using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    private void Awake()
    {
        SetHealth(0);
    }

    private void SetHealth(float health)
    {
        healthText.text = "score: " + health.ToString();
    }

    public void UpdateHealthText(Component sender, object data)
    {
        if(data is float)
        {
            float health = (float)data;
            SetHealth(health);
        }
    }
}

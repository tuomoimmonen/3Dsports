using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
    [SerializeField] TMP_Text distanceText;

    private void Awake()
    {
        SetDistance(0);
    }
    void Start()
    {

    }

    void Update()
    {

    }

    void SetDistance(float distance)
    {
        distanceText.text = distance.ToString() + "m";
    }

    public void UpdateDistanceText(Component sender, object data)
    {
        if(data is float)
        {
            float distance = (float)data;
            SetDistance(distance);
        }
    }
}

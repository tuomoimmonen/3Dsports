using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    [SerializeField] Slider timingSlider;
    [SerializeField] float sliderSpeed = 1f;

    [SerializeField] Color greenZoneColor;
    [SerializeField] Color redZoneColor;

    [SerializeField] Sprite greenZone;
    [SerializeField] Sprite redZone;

    int direction = 1;
    void Start()
    {
        
    }

    void Update()
    {
        if(timingSlider.value <= timingSlider.minValue || timingSlider.value >= timingSlider.maxValue)
        {
            direction *= -1;
        }

        timingSlider.value += sliderSpeed * direction * Time.deltaTime;

        if(timingSlider.value >= 0.4f && timingSlider.value <= 0.8f)
        {
            timingSlider.fillRect.GetComponent<Image>().sprite = greenZone;
        }
        else
        {
            timingSlider.fillRect.GetComponent<Image>().sprite = redZone;
        }
    }
}

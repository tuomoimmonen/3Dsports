using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] RectTransform movingObject; //object moving between the timing bar
    [SerializeField] RectTransform greenZone; //boost zone in the middle of the timing bar

    [SerializeField] ParticleSystem greenZoneEffect;
    [SerializeField] ParticleSystem redZoneEffect;

    public bool boostAvailable;

    public GameEvent isBoostAvailable;

    void Start()
    {
        
    }

    void Update()
    {
        if (movingObject.anchorMin.x >= greenZone.anchorMin.x && movingObject.anchorMax.x <= greenZone.anchorMax.x)
        {
            boostAvailable = true;
            isBoostAvailable.Raise(this, boostAvailable);
        }
        else 
        { 
            boostAvailable = false;
            isBoostAvailable.Raise(this, boostAvailable);
        }
    }

    public void ShowGreenEffect(Component sender, object data)
    {
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(movingObject.position);
        //Instantiate(boostEffect, worldPosition, Quaternion.identity);
        if(data is bool)
        {
            greenZoneEffect.Play(); 
        }
    }

    public void ShowRedEffect(Component sender, object data)
    {
        if(data is bool)
        {
            redZoneEffect.Play();
        }
    }

}

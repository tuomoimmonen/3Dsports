using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinDistanceStartPoint : MonoBehaviour
{
    public Vector3 startPointForDistance;
    public GameEvent javelinDistanceCalculationStartPoint;

    private void Awake()
    {
        SetStartPoint();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SetStartPoint()
    {
        startPointForDistance = transform.position;
        javelinDistanceCalculationStartPoint.Raise(this, startPointForDistance);
    }
}

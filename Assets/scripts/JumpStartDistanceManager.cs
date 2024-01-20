using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStartDistanceManager : MonoBehaviour
{
    public GameEvent jumpDistanceStartPoint;
    public Vector3 startPoint;
    void Start()
    {
        SetSpawnPoint();
    }

    void Update()
    {
        
    }

    void SetSpawnPoint()
    {
        startPoint = transform.position;
        jumpDistanceStartPoint.Raise(this, startPoint);
    }
}

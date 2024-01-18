using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameEvent spawnPointChanged;
    void Start()
    {
        SetSpawnPoint();
    }

    void Update()
    {
        
    }

    private void SetSpawnPoint()
    {
        spawnPoint = transform.position;
        spawnPointChanged.Raise(this, spawnPoint);
    }
}

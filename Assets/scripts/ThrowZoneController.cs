using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowZoneController : MonoBehaviour
{
    bool playerInJumpZone;
    public GameEvent playerJumpZone;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInJumpZone = true;
            playerJumpZone.Raise(this, playerInJumpZone);
        }
    }
}

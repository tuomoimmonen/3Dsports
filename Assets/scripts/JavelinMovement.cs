using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinMovement : MonoBehaviour
{
    [SerializeField] Rigidbody playersRb;
    Rigidbody javelinRb;

    Collider javelinCollider;

    [SerializeField] float throwForce = 1.0f;
    float startSpeed; //players speed
    Vector3 currentSpeed;
    Vector3 startingPos;

    public bool isFlying = false;

    public GameEvent javelinDistanceChanged;

    private void Awake()
    {
        javelinRb = GetComponent<Rigidbody>();
        javelinCollider = GetComponent<Collider>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!isFlying) { javelinRb.velocity = playersRb.velocity; }

        if (Input.GetKeyDown(KeyCode.D) && !isFlying)
        {
            isFlying = true;
            startingPos = transform.position;
            javelinRb.AddForce(transform.forward * throwForce);
            javelinRb.AddForce(transform.up * throwForce);
            javelinCollider.isTrigger = false;
            javelinRb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            float distanceTraveledInAir = Vector3.Distance(startingPos, transform.position);
            javelinDistanceChanged.Raise(this, distanceTraveledInAir);
            Debug.Log(distanceTraveledInAir);
        }
    }
}

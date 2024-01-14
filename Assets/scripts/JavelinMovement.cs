using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinMovement : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody javelinRb;

    Collider javelinCollider;

    [SerializeField] float throwForce = 1.0f;
    float playersSpeed;
    float totalForwardForce;
    Vector3 startingPos;

    public bool isFlying = false;

    public GameEvent javelinDistanceChanged;

    private void Awake()
    {
        javelinRb = GetComponent<Rigidbody>();
        javelinCollider = GetComponent<Collider>();
        player = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        javelinRb.useGravity = false;
        javelinRb.isKinematic = true;
    }

    void Update()
    {
        //if (!isFlying) { javelinRb.velocity = playersRb.velocity; }

        if (Input.GetKeyDown(KeyCode.D) && !isFlying)
        {
            playersSpeed = player.speed;
            totalForwardForce = throwForce * playersSpeed;
            transform.SetParent(null);
            isFlying = true;
            javelinRb.isKinematic = false;
            startingPos = transform.position;
            javelinRb.AddForce(Vector3.forward * totalForwardForce);
            javelinRb.AddForce(Vector3.up * totalForwardForce);
            //javelinRb.AddForce(transform.forward * throwForce);
            //javelinRb.AddForce(transform.up * throwForce);
            javelinCollider.isTrigger = false;
            javelinRb.useGravity = true;
            player.gameStarted = false;
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

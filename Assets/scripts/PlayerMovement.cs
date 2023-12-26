using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float startSpeed = 0f;
    [SerializeField] float accelerationSpeed = 0.01f;
    [SerializeField] float speedIncrease = 2f; //for pushing the right input
    [SerializeField] float speedDecrease = 0.5f; //for missing the input
    [SerializeField] float speed; //players speed

    private Vector3 previousPosition; //for measuring the distance
    private float distance = 0f;

    [SerializeField] float slowingDrag = 5f; //slowdown the player for missing the input
    [SerializeField] float slowingDuration = 0.5f;

    public bool gameStarted = false;

    [SerializeField] TMP_Text speedText;

    public GameEvent playerSpeedIncreased;
    public GameEvent playerSpeedDecreased;
    public GameEvent playerDistanceIncreased;

    bool isBoosting = false;
    bool isSlowing = false;

    [SerializeField] float jumpForce = 10f;
    bool isJumping = false;
    Vector3 startingPosition; //start position for jump


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = startSpeed;
        previousPosition = transform.position;
    }

    void Update()
    {
        if (gameStarted)
        {
            StartAccelerating();

            CalculateDistanceMoved();

            speedText.text = "speed: " + speed.ToString();
        }


        //jumping mechanic
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isJumping)
        {
            float speedFactor = rb.velocity.magnitude;
            //rb.velocity = Vector3.up * jumpForce;
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            isJumping = true;
            startingPosition = rb.position;
        }

        //todo gamemanager
        if (Input.GetKeyDown(KeyCode.Return)) { gameStarted = true; }
    }

    private void StartAccelerating()
    {
        //start accelerating the player
        speed += accelerationSpeed * Time.deltaTime;

        if (speed <= 0) //guard for negative
        {
            speed = 0;
        }
    }

    private void CalculateDistanceMoved()
    {
        //distance calculations
        float distanceMovedThisFrame = Vector3.Distance(previousPosition, transform.position);
        distance += distanceMovedThisFrame;
        previousPosition = transform.position;
        playerDistanceIncreased.Raise(this, distance);
    }

    private void FixedUpdate()
    {
        if (isBoosting)
        {
            //rb.AddForce(transform.forward * speedIncrease, ForceMode.Impulse);
            //speed += speedIncrease;
            speed *= speedIncrease;
            isBoosting = false;
        }
        if (isSlowing)
        {
            //rb.AddForce(transform.forward * -speedDecrease, ForceMode.Impulse);
            //speed -= speedDecrease;
            speed *= speedDecrease;
            isSlowing = false;
        }
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            isJumping = false;
        }

        StartRunning();

    }

    void StartRunning()
    {
        rb.AddForce(transform.forward * speed);
    }
    public void SpeedIncrease(Component sender, object data)
    {
        if(data is bool)
        {
            bool canBoost = (bool)data;

            if (canBoost && Input.GetButtonDown("Jump"))
            {
                //speed += speedIncrease;
                playerSpeedIncreased.Raise(this, canBoost);
                isBoosting = true;

            }
            else if (!canBoost && Input.GetButtonDown("Jump"))
            {
                //speed -= speedDecrease;
                isSlowing = true;
                playerSpeedDecreased.Raise(this, canBoost);
                StartCoroutine(IsSlowing());
            }
        }
    }

    IEnumerator IsSlowing()
    {
        rb.drag = slowingDrag;
        yield return new WaitForSeconds(slowingDuration);
        rb.drag = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            float distanceJumped = Vector3.Distance(startingPosition, rb.position); //startpos from jumpstart
            Debug.Log(distanceJumped);
        }
    }

}

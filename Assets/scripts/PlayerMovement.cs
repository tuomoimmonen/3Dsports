using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float startSpeed = 0f;
    private float defaultSpeed;
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
    public GameEvent playerJumpedChanged;

    bool isBoosting = false;
    bool isSlowing = false;

    [SerializeField] float jumpForce = 10f;
    bool isJumping = false;
    Vector3 startingPosition; //start position for jump

    JavelinMovement javelin;


    void Start()
    {
        javelin = FindObjectOfType<JavelinMovement>();
        rb = GetComponent<Rigidbody>();
        speed = startSpeed;
        previousPosition = transform.position;
        defaultSpeed = startSpeed;
    }

    void Update()
    {

        //DEBUG REMEMBER TO DISABLE
        if(Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        //DEBUG REMEMBER TO DISABLE

        if (gameStarted)
        {
            StartAccelerating();

            CalculateDistanceMoved();

            speedText.text = "speed: " + speed.ToString("F");
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
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            gameStarted = true;
            isJumping = false;
        }
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
        if (gameStarted && !javelin.isFlying)
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
                gameStarted = false; //stop the game when jumped
                speed = defaultSpeed; //reset the speed
            }

            StartRunning();
        }


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
            if(distanceJumped < 1) { distanceJumped = 0; } //guard for minimal jump at the start
            playerJumpedChanged.Raise(this, distanceJumped);
            //Debug.Log(distanceJumped);
        }
    }

}

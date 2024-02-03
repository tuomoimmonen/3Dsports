using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject groundCheckObject;
    Rigidbody rb;
    [SerializeField] float startSpeed = 0f;
    [SerializeField] float maxSpeed = 50f;
    private float initialSpeed;
    [SerializeField] float accelerationSpeed = 0.01f;
    [SerializeField] float speedIncrease = 2f; //for pushing the right input
    [SerializeField] float speedDecrease = 0.5f; //for missing the input
    [SerializeField] public float speed; //players speed

    private Vector3 previousPosition; //for measuring the distance
    private float distance = 0f;
    public float runTime;

    [SerializeField] float slowingDrag = 5f; //slowdown the player for missing the input
    [SerializeField] float slowingDuration = 0.5f;
    [SerializeField] float finishSlowDuration = 3f; //slow the player rapidly in the running finish state

    public bool gameStarted = false;

    public GameEvent playerSpeedIncreased; //for ui
    public GameEvent playerSpeedDecreased; //for ui
    public GameEvent playerDistanceIncreased; //testing purpose
    public GameEvent playerJumpedChanged;
    public GameEvent playersSpeedValue; //for ui
    public GameEvent playerRunTimeValue; //for ui
    public GameEvent overStepped;
    public GameEvent playerFinished;

    bool isBoosting = false; //ui elements
    bool isSlowing = false; //ui elements

    [SerializeField] float jumpForce = 10f;
    bool isJumping = false;
    bool canSendJumpData = false; //prevent wrong data when changing scenes
    Vector3 jumpStartPosition; //start position for jump

    JavelinMovement javelin;

    bool jumpForceAdded = false;
    public bool isGrounded;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] float gravityInAir = -18f; //added in fixed update

    int sceneIndex;

    bool canMove;
    bool playerIsInJumpZone;

    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        javelin = FindObjectOfType<JavelinMovement>();
        rb = GetComponent<Rigidbody>();
        speed = startSpeed;
        previousPosition = transform.position;
        initialSpeed = startSpeed;
    }

    void Update()
    {

        //DEBUG REMEMBER TO DISABLE
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
        //DEBUG REMEMBER TO DISABLE

        //todo gamemanager

        //StartGameWithEnterKey();
        HandleSpeedData();
        CheckTheGround();

        JumpKey();

        if (canMove)
        {
            initialSpeed = 1f; //get boost at the start
            StartAccelerating();
            UpdatePlayersRunTime();
            CalculateDistanceMoved();
            

            if (isBoosting)
            {
                speed *= speedIncrease;
                isBoosting = false;
            }

            if (isSlowing)
            {
                speed *= speedDecrease;
                isSlowing = false;
            }
        }
        else
        {
            initialSpeed = 0f; //prevent wrong calculation in the ui
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * finishSlowDuration); //slow the player in the finish line
        }

        DebugDrawRay();
    }

    public void JumpKey()
    {
        //jumping mechanic
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Keyboard.current.leftCtrlKey.wasPressedThisFrame) && isGrounded && sceneIndex == 2)
        {
            AnimationController.instance.SetAnimationTrigger("jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            SoundManager.instance.PlayAudio(3);
            if (canMove) 
            {
                isJumping = true;
                canSendJumpData = true; 
            }
        }
    }

    public void StartGameWithEnterKey()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Keyboard.current.enterKey.wasPressedThisFrame))
        {
            gameStarted = true;
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        if (!javelin.isFlying)
        {
            StartRunning();
            /*
            if (isJumping && !jumpForceAdded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                isJumping = false;
                jumpForceAdded = true;
            }
            */

            if (!isGrounded)
            {
                rb.AddForce(Vector3.up * gravityInAir, ForceMode.Acceleration); //negative custom gravity
            }
        }
    }

    private void CheckTheGround()
    {
        isGrounded = CheckGround();

        bool CheckGround()
        {
            RaycastHit hit;
            Vector3 raycastOrigin = groundCheckObject.transform.position;
            bool raycastHit = Physics.Raycast(raycastOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer);
            return raycastHit;
        }

        if (isGrounded)
        {
            jumpForceAdded = false;
        }
    }

    private void DebugDrawRay()
    {
        // Draw a ray from the object's position downward for visualization
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.yellow);
    }

    private void HandleSpeedData()
    {
        float correctedSpeedValue = speed - initialSpeed; //for ui
        playersSpeedValue.Raise(this, correctedSpeedValue);

        if (speed <= 0) //guard for negative
        {
            speed = 0;
        }
    }

    private void UpdatePlayersRunTime()
    {
        runTime += Time.deltaTime;
        playerRunTimeValue.Raise(this, runTime);
    }

    private void StartAccelerating()
    {
        //start accelerating the player
        speed += accelerationSpeed * Time.deltaTime;
        speed = Mathf.Clamp(speed, initialSpeed, maxSpeed);
    }

    private void CalculateDistanceMoved()
    {
        //distance calculations
        float distanceMovedThisFrame = Vector3.Distance(previousPosition, transform.position);
        distance += distanceMovedThisFrame;
        previousPosition = transform.position;
        playerDistanceIncreased.Raise(this, distance);
    }

    void StartRunning()
    {
        Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }
    public void SpeedIncrease(Component sender, object data)
    {
        if(data is bool)
        {
            bool canBoost = (bool)data;

            if (canBoost && (Input.GetButtonDown("Jump") || Keyboard.current.spaceKey.wasPressedThisFrame))
            {
                //speed += speedIncrease;
                playerSpeedIncreased.Raise(this, canBoost);
                isBoosting = true;
                SoundManager.instance.PlayAudio(1);

            }
            else if (!canBoost && (Input.GetButtonDown("Jump") || Keyboard.current.spaceKey.wasPressedThisFrame))
            {
                //speed -= speedDecrease;
                isSlowing = true;
                AnimationController.instance.SetAnimationTrigger("runFail");
                playerSpeedDecreased.Raise(this, canBoost);
                StartCoroutine(IsSlowing());
                SoundManager.instance.PlayAudio(2);
            }
        }
    }

    public void SetStartPosition(Component sender, object data)
    {
        if(data is Vector3)
        {
            Vector3 pos = (Vector3)data;
            transform.position = pos;
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
        if (collision.gameObject.CompareTag("Ground") && canSendJumpData)
        {
            canSendJumpData = false;
            if(playerIsInJumpZone)
            {
                float distanceJumped = Vector3.Distance(jumpStartPosition, rb.position); //startpos from jumpstart
                if (distanceJumped < 1) { distanceJumped = 0; } //guard for minimal jump at the start
                playerJumpedChanged.Raise(this, distanceJumped);
                HighScoresText.instance.AddScores(distanceJumped, sceneIndex);
                HighScoresText.instance.UpdateScoreText();
                HighScoresText.instance.UpdateCurrentScoreText(distanceJumped, sceneIndex);
                SoundManager.instance.PlayAudio(7);
                AnimationController.instance.SetAnimationTrigger("win");
            }
            else if (!playerIsInJumpZone)
            {
                HighScoresText.instance.AddScores(0, sceneIndex);
                HighScoresText.instance.UpdateScoreText();
                HighScoresText.instance.UpdateCurrentScoreText(0, sceneIndex);
                SoundManager.instance.PlayAudio(2);
            }
            playerFinished.Raise(this, true);
            canMove = false;
        }
    }

    public void ChangeJumpDistanceStartPoint(Component sender, object data)
    {
        if(data is Vector3)
        {
            Vector3 startPosition = (Vector3)data;
            jumpStartPosition = startPosition;
        }
    }

    public void ChangeCanMoveBool(Component sender, object data)
    {
        if(data is bool)
        {
            bool playerIsAbleToMove = (bool)data;
            canMove = playerIsAbleToMove;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Yli"))
        {
            //canMove = false;
            overStepped.Raise(this, true);
            SoundManager.instance.PlayAudio(2);
        }
    }

    public void SetJumpZoneBool(Component sender, object data)
    {
        if (data is bool) 
        {
            bool playerInJumpZone = (bool)data;
            playerIsInJumpZone = playerInJumpZone;
        }
    }
    

}

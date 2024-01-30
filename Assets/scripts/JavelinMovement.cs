using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class JavelinMovement : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    PlayerMovement player;
    Rigidbody javelinRb;

    Collider javelinCollider;

    [SerializeField] float throwForce = 1.0f;
    float playersSpeed;
    float totalForwardForce;
    Vector3 startingPos;

    public bool isFlying = false;
    bool javelinFinished = false;

    public GameEvent javelinDistanceChanged;
    public GameEvent javelinFinish;
    public GameEvent canPlayerMove;
    public GameEvent canMoveCameraToFinalPosition;

    int sceneIndex;

    bool canThrowJavelin;
    bool playerInThrowZone;

    private void Awake()
    {
        javelinRb = GetComponent<Rigidbody>();
        javelinCollider = GetComponent<Collider>();
        player = FindObjectOfType<PlayerMovement>();

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        javelinRb.useGravity = false;
        javelinRb.isKinematic = true;

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if(buildIndex != 3)
        {
            MeshRenderer javelinMesh = GetComponent<MeshRenderer>();
            javelinMesh.enabled = false;
        }
        trail.enabled = false;
    }

    void Update()
    {
        if(canThrowJavelin) { ThrowJavelin(); }

    }

    public void ThrowJavelin()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Keyboard.current.dKey.wasPressedThisFrame) && !isFlying && sceneIndex == 3)
        {
            AnimationController.instance.SetAnimationTrigger("throw");
            SoundManager.instance.PlayAudio(4);
            playersSpeed = player.speed;
            totalForwardForce = throwForce * playersSpeed;
            trail.enabled = true;
            transform.SetParent(null);
            isFlying = true;
            javelinRb.isKinematic = false;
            javelinRb.AddForce(Vector3.forward * totalForwardForce * 1.2f);
            javelinRb.AddForce(Vector3.up * totalForwardForce * 0.6f);
            javelinCollider.isTrigger = false;
            javelinRb.useGravity = true;
            canPlayerMove.Raise(this, false);
            canMoveCameraToFinalPosition.Raise(this, true);
            //transform.Rotate(-10, 0, 0); //rotate the javelin to land with nose on
            //transform.Rotate(0, 10, 0);
            //transform.rotation = Quaternion.identity;
            transform.rotation = Quaternion.Euler(-5, 180, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            javelinFinished = true;
            javelinRb.velocity = Vector3.zero;
            if (playerInThrowZone)
            {
                float distanceTraveledInAir = Vector3.Distance(startingPos, transform.position);
                javelinDistanceChanged.Raise(this, distanceTraveledInAir);
                HighScoresText.instance.AddScores(distanceTraveledInAir, sceneIndex);
                HighScoresText.instance.UpdateScoreText();
                HighScoresText.instance.UpdateCurrentScoreText(distanceTraveledInAir, sceneIndex);
                SoundManager.instance.PlayAudioNotPitched(7);

            }
            else if (!playerInThrowZone)
            {
                HighScoresText.instance.AddScores(0, sceneIndex);
                HighScoresText.instance.UpdateScoreText();
                HighScoresText.instance.UpdateCurrentScoreText(0, sceneIndex);
            }
            javelinFinish.Raise(this, javelinFinished);
            trail.enabled = false;
        }
    }

    public void ChangeCanThrowJavelinBool(Component sender, object data)
    {
        if(data is bool)
        {
            bool isAbleToThrowJavelin = (bool)data;
            canThrowJavelin = isAbleToThrowJavelin;
        }
    }

    public void SetJavelinDistanceStartPoint(Component sender, object data)
    {
        if(data is Vector3)
        {
            Vector3 startPoint = (Vector3)data;
            startingPos = startPoint;
        }
    }

    public void SetPlayerInThrowZoneBool(Component sender, object data)
    {
        if (data is bool)
        {
            bool playerIsInThrowZone = (bool)data;
            playerInThrowZone = playerIsInThrowZone;
        }
    }


}

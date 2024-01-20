using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    bool javelinFinished = false;

    public GameEvent javelinDistanceChanged;
    public GameEvent javelinFinish;

    int sceneIndex;

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
    }

    void Update()
    {
        ThrowJavelin();
    }

    public void ThrowJavelin()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Keyboard.current.dKey.wasPressedThisFrame) && !isFlying && sceneIndex == 3)
        {
            AnimationController.instance.SetAnimationTrigger("throw");
            playersSpeed = player.speed;
            totalForwardForce = throwForce * playersSpeed;
            transform.SetParent(null);
            isFlying = true;
            javelinRb.isKinematic = false;
            startingPos = transform.position;
            javelinRb.AddForce(Vector3.forward * totalForwardForce * 1.25f);
            javelinRb.AddForce(Vector3.up * totalForwardForce * 0.65f);
            javelinCollider.isTrigger = false;
            javelinRb.useGravity = true;
            player.gameStarted = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            javelinFinished = true;
            float distanceTraveledInAir = Vector3.Distance(startingPos, transform.position);
            javelinDistanceChanged.Raise(this, distanceTraveledInAir);
            javelinFinish.Raise(this, javelinFinished);

            HighScoresText.instance.AddScores(distanceTraveledInAir, sceneIndex);
            HighScoresText.instance.UpdateScoreText();
            HighScoresText.instance.UpdateCurrentScoreText(distanceTraveledInAir, sceneIndex);
            //StartCoroutine(BackToRunningScene());

        }
    }

    IEnumerator BackToRunningScene()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Running");
    }
}

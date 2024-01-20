using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public enum GameState { menu, running, jumping, javelin, end };
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameTutorialComplete = false;

    [SerializeField] float startTimer;
    [SerializeField] TMP_Text timerText;

    GameState gameState;

    [SerializeField] TMP_Text pushForMoreSpeedText;
    bool pushSpeedButton = false;
    [SerializeField] TMP_Text pushToJumpText;
    bool pushJumpButton = false;
    [SerializeField] TMP_Text pushToThrowText;
    bool pushThrowButton = false;

    int sceneIndex;

    public GameEvent startGame;
    bool startTimerStarted;
    bool startTimerFinished;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        timerText.text = "";
    }

    void Update()
    {
        DebugLevels();
        GameTutorial();
    }

    private void GameTutorial()
    {
        switch(sceneIndex)
        {
            case 1:
                if(!gameTutorialComplete && !pushSpeedButton)
                {
                    pushForMoreSpeedText.enabled = true;
                    pushToJumpText.enabled = false;
                    if((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                    {
                        pushForMoreSpeedText.enabled = false;
                        gameTutorialComplete = true;
                    }
                }
                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) { timerText.text = "GET READY"; }
                    StartCoroutine(StartTheTimer());
                    if(startTimer >= 1 && startTimerStarted)
                    {
                        timerText.text = startTimer.ToString("F0");
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        timerText.text = "GO!";
                        StartCoroutine(DisableStartTimerText());
                    }
                }
                break;
            case 2:
                if(!gameTutorialComplete)
                {
                    if (!pushSpeedButton)
                    {
                        pushForMoreSpeedText.enabled = true;
                        if ((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                        {
                            pushForMoreSpeedText.enabled = false;
                            pushSpeedButton = true;
                        }

                    }
                    else if (pushSpeedButton && !pushJumpButton)
                    {
                        pushToJumpText.enabled = true;
                        if((Input.GetKeyDown(KeyCode.LeftControl) || Keyboard.current.leftCtrlKey.wasPressedThisFrame))
                        {
                            pushToJumpText.enabled = false;
                            pushJumpButton = true;
                            gameTutorialComplete = true;
                        }
                    }
                }
                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) { timerText.text = "GET READY"; }
                    StartCoroutine(StartTheTimer());
                    if (startTimer >= 1 && startTimerStarted)
                    {
                        timerText.text = startTimer.ToString("F0");
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        timerText.text = "GO!";
                        StartCoroutine(DisableStartTimerText());
                    }
                }
                break;
            case 3:
                if(!gameTutorialComplete)
                {
                    if (!pushSpeedButton)
                    {
                        pushForMoreSpeedText.enabled = true;
                        if ((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                        {
                            pushForMoreSpeedText.enabled = false;
                            pushSpeedButton = true;
                        }
                    }

                    else if (pushSpeedButton && !pushThrowButton)
                    {
                        pushToThrowText.enabled = true;
                        if ((Input.GetKeyDown(KeyCode.D) || Keyboard.current.dKey.wasPressedThisFrame))
                        {
                            pushToThrowText.enabled = false;
                            pushThrowButton = true;
                            gameTutorialComplete = true;
                        }
                    }
                }

                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) { timerText.text = "GET READY"; }
                    StartCoroutine(StartTheTimer());
                    if (startTimer >= 1 && startTimerStarted)
                    {
                        timerText.text = startTimer.ToString("F0");
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        timerText.text = "GO!";
                        StartCoroutine(DisableStartTimerText());
                    }
                }
                break;
        }
    }

    private void DebugLevels()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if ((Input.GetKeyDown(KeyCode.T) || Keyboard.current.tKey.wasPressedThisFrame))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex - 1) % SceneManager.sceneCountInBuildSettings);
        }
    }


    IEnumerator StartTheTimer()
    {
        yield return new WaitForSeconds(2f);
        startTimerStarted = true;
        startTimer -= Time.deltaTime;
    }

    IEnumerator DisableStartTimerText()
    {
        yield return new WaitForSeconds(1f);
        timerText.enabled = false;
        startTimerFinished = true;
    }



}

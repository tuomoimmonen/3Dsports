using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameTutorialComplete = false;

    [SerializeField] float startTimer;
    [SerializeField] TMP_Text timerText;

    [SerializeField] TMP_Text pushForMoreSpeedText;
    [SerializeField] GameObject tapRunButtonImage;
    bool pushSpeedButton = false;
    [SerializeField] TMP_Text pushToJumpText;
    [SerializeField] GameObject tapJumpButtonImage;
    bool pushJumpButton = false;
    [SerializeField] TMP_Text pushToThrowText;
    [SerializeField] GameObject tapThrowButtonImage;
    bool pushThrowButton = false;

    [SerializeField] GameObject rightHelpArrowImage;
    [SerializeField] GameObject leftHelpArrowImage;

    [SerializeField] GameObject energyZoneHelpImage;
    [SerializeField] GameObject energyArrowHelpImage;

    int sceneIndex;

    public GameEvent startGame;
    bool startTimerStarted;
    bool startTimerFinished;
    bool startBeepsPlayed;
    bool startPistolPlayed;
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

        if(startTimerStarted && !startBeepsPlayed) 
        { 
            startBeepsPlayed = true;
            SoundManager.instance.PlayAudioNotPitched(5); 
        }

        if(startTimerFinished && !startPistolPlayed && sceneIndex != 2 && sceneIndex != 3)
        {
            startPistolPlayed = true;
            SoundManager.instance.PlayAudioNotPitched(6);
        }
    }

    private void GameTutorial()
    {
        switch(sceneIndex)
        {
            case 0:
                //pushForMoreSpeedText.enabled = false;
                //pushToJumpText.enabled = false;
                //pushToThrowText.enabled = false;
                tapRunButtonImage.SetActive(false);
                tapJumpButtonImage.SetActive(false);
                tapThrowButtonImage.SetActive(false);
                rightHelpArrowImage.SetActive(false);
                leftHelpArrowImage.SetActive(false);
                energyArrowHelpImage.SetActive(false);
                energyZoneHelpImage.SetActive(false);
                break;
            case 1:
                if(!gameTutorialComplete && !pushSpeedButton)
                {
                    //pushForMoreSpeedText.enabled = true;
                    tapRunButtonImage.SetActive(true);
                    rightHelpArrowImage.SetActive(true);
                    if((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                    {
                        //pushForMoreSpeedText.enabled = false;
                        tapRunButtonImage.SetActive(false);
                        rightHelpArrowImage.SetActive(false);
                        gameTutorialComplete = true;

                    }
                }
                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) { timerText.text = "GET READY"; }
                    StartCoroutine(StartTheTimer());
                    if(startTimer >= 1 && startTimerStarted)
                    {
                        timerText.text = "";
                        energyArrowHelpImage.SetActive(true);
                        energyZoneHelpImage.SetActive(true);
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        timerText.text = "GO!";
                        energyArrowHelpImage.SetActive(false);
                        energyZoneHelpImage.SetActive(false);
                        StartCoroutine(DisableStartTimerText());
                    }
                }

                break;
            case 2:
                if(!gameTutorialComplete)
                {
                    if (!pushSpeedButton)
                    {
                        //pushForMoreSpeedText.enabled = true;
                        tapRunButtonImage.SetActive(true);
                        rightHelpArrowImage.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                        {
                            //pushForMoreSpeedText.enabled = false;
                            tapRunButtonImage.SetActive(false);
                            rightHelpArrowImage.SetActive(false);
                            pushSpeedButton = true;
                        }

                    }
                    else if (pushSpeedButton && !pushJumpButton)
                    {
                        //pushToJumpText.enabled = true;
                        tapJumpButtonImage.SetActive(true);
                        leftHelpArrowImage.SetActive(true);
                        if((Input.GetKeyDown(KeyCode.LeftControl) || Keyboard.current.leftCtrlKey.wasPressedThisFrame))
                        {
                            //pushToJumpText.enabled = false;
                            tapJumpButtonImage.SetActive(false);
                            leftHelpArrowImage.SetActive(false);
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
                        //timerText.text = startTimer.ToString("F0");
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
                        //pushForMoreSpeedText.enabled = true;
                        tapRunButtonImage.SetActive(true);
                        rightHelpArrowImage.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                        {
                            //pushForMoreSpeedText.enabled = false;
                            tapRunButtonImage.SetActive(false);
                            rightHelpArrowImage.SetActive(false);
                            pushSpeedButton = true;
                        }
                    }

                    else if (pushSpeedButton && !pushThrowButton)
                    {
                        //pushToThrowText.enabled = true;
                        tapThrowButtonImage.SetActive(true);
                        leftHelpArrowImage.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.D) || Keyboard.current.dKey.wasPressedThisFrame))
                        {
                            //pushToThrowText.enabled = false;
                            tapThrowButtonImage.SetActive(false);
                            leftHelpArrowImage.SetActive(false);
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
                        //timerText.text = startTimer.ToString("F0");
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
        startTimerFinished = true;
        yield return new WaitForSeconds(1f);
        timerText.enabled = false;
    }
}

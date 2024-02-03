using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

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

    [SerializeField] GameObject getReadyImage;
    [SerializeField] GameObject goImage;
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
                tapRunButtonImage.SetActive(false);
                tapJumpButtonImage.SetActive(false);
                tapThrowButtonImage.SetActive(false);
                rightHelpArrowImage.SetActive(false);
                leftHelpArrowImage.SetActive(false);
                energyArrowHelpImage.SetActive(false);
                energyZoneHelpImage.SetActive(false);
                goImage.SetActive(false);
                getReadyImage.SetActive(false);
                break;
            case 1:
                if(!gameTutorialComplete && !pushSpeedButton)
                {

                    tapRunButtonImage.SetActive(true);
                    rightHelpArrowImage.SetActive(true);
                    if((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                    {
                        tapRunButtonImage.SetActive(false);
                        rightHelpArrowImage.SetActive(false);
                        gameTutorialComplete = true;

                    }
                }
                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) 
                    { 
                        getReadyImage.SetActive(true);
                    }
                    StartCoroutine(StartTheTimer());
                    if(startTimer >= 1 && startTimerStarted)
                    {
                        getReadyImage.SetActive(false);
                        energyArrowHelpImage.SetActive(true);
                        energyZoneHelpImage.SetActive(true);
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        goImage.SetActive(true);
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
                        tapRunButtonImage.SetActive(true);
                        rightHelpArrowImage.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                        {
                            tapRunButtonImage.SetActive(false);
                            rightHelpArrowImage.SetActive(false);
                            pushSpeedButton = true;
                        }

                    }
                    else if (pushSpeedButton && !pushJumpButton)
                    {
                        tapJumpButtonImage.SetActive(true);
                        leftHelpArrowImage.SetActive(true);
                        if((Input.GetKeyDown(KeyCode.LeftControl) || Keyboard.current.leftCtrlKey.wasPressedThisFrame))
                        {
                            tapJumpButtonImage.SetActive(false);
                            leftHelpArrowImage.SetActive(false);
                            pushJumpButton = true;
                            gameTutorialComplete = true;
                        }
                    }
                }
                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) { getReadyImage.SetActive(true); }
                    StartCoroutine(StartTheTimer());
                    if (startTimer >= 1 && startTimerStarted)
                    {
                        getReadyImage.SetActive(false);
                        energyArrowHelpImage.SetActive(true);
                        energyZoneHelpImage.SetActive(true);
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        goImage.SetActive(true);
                        energyArrowHelpImage.SetActive(false);
                        energyZoneHelpImage.SetActive(false);
                        StartCoroutine(DisableStartTimerText());
                    }
                }
                break;
            case 3:
                if(!gameTutorialComplete)
                {
                    if (!pushSpeedButton)
                    {
                        tapRunButtonImage.SetActive(true);
                        rightHelpArrowImage.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.Space) || Keyboard.current.spaceKey.wasPressedThisFrame))
                        {
                            tapRunButtonImage.SetActive(false);
                            rightHelpArrowImage.SetActive(false);
                            pushSpeedButton = true;
                        }
                    }

                    else if (pushSpeedButton && !pushThrowButton)
                    {
                        tapThrowButtonImage.SetActive(true);
                        leftHelpArrowImage.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.D) || Keyboard.current.dKey.wasPressedThisFrame))
                        {
                            AnimationController.instance.SetAnimationTrigger("throw");
                            SoundManager.instance.PlayAudio(4);
                            tapThrowButtonImage.SetActive(false);
                            leftHelpArrowImage.SetActive(false);
                            pushThrowButton = true;
                            gameTutorialComplete = true;
                        }
                    }
                }

                else if (gameTutorialComplete && !startTimerFinished)
                {
                    if (!startTimerStarted) { getReadyImage.SetActive(true); }
                    StartCoroutine(StartTheTimer());
                    if (startTimer >= 1 && startTimerStarted)
                    {
                        getReadyImage.SetActive(false);
                        energyArrowHelpImage.SetActive(true);
                        energyZoneHelpImage.SetActive(true);
                    }
                    else if (startTimer <= 0)
                    {
                        startGame.Raise(this, gameTutorialComplete);
                        goImage.SetActive(true);
                        energyArrowHelpImage.SetActive(false);
                        energyZoneHelpImage.SetActive(false);
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
        goImage.SetActive(false);
        //timerText.enabled = false;
    }
}

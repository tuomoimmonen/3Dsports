using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera playerCamera;

    [SerializeField] float playersMaxSpeedForCamera = 15f;
    [SerializeField] float fovMin = 25f;
    [SerializeField] float fovMax = 45f;

    JavelinMovement javelin;

    PlayerMovement player;

    CinemachineFramingTransposer framingTransposer;

    bool playerFinished = false;

    int sceneIndex;

    private void Awake()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        player = FindObjectOfType<PlayerMovement>();
        framingTransposer = playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        javelin = GameObject.FindObjectOfType<JavelinMovement>();

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
    
    }

    void Update()
    {
        switch (sceneIndex)
        {
            case 1:
                if (playerFinished)
                {
                    StartCoroutine(WaitBeforeMovingTheCamera());
                }
                break;
            case 2:
                if (playerFinished)
                {
                    StartCoroutine(WaitBeforeMovingTheCamera());
                }
                break;
            case 3:
                if (playerFinished)
                {
                    playerCamera.enabled = false;
                    JavelinFinishedCameraPosition();

                    if (!javelin.isFlying)
                    {

                    }
                }
                break;
        }
    }

    public void ChangeFovValue(Component sender, object data)
    {
        if(data is float)
        {
            if(javelin.isFlying) 
            {
                return;
            }

            float playerSpeed = (float)data;
            float normalizedSpeed = playerSpeed / playersMaxSpeedForCamera; // value 0-1
            normalizedSpeed = Mathf.Clamp01(normalizedSpeed);
            float remappedSpeed = fovMin + normalizedSpeed * (fovMax - fovMin); //remap the normalized speed to field of view value

            playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, remappedSpeed, Time.deltaTime); //screen tearing guard with lerp

        }
    }

    private void ChangeCameraToJavelin()
    {
        framingTransposer.m_ZDamping = 20f;

        //framingTransposer.m_TrackedObjectOffset = new Vector3(0,1,-7);
        //framingTransposer.m_LookaheadTime = 1f;
        //framingTransposer.m_SoftZoneHeight = 2f;

        //playerCamera.m_Follow = javelin.transform;
        //playerCamera.m_LookAt = javelin.transform;

        JavelinFinishedCameraPosition();


    }

    private void JavelinFinishedCameraPosition()
    {
        float lerpSpeed = 0.4f;
        Vector3 offset = new Vector3(0.5f, 1f, -3);
        //Vector3 playerPosition = player.transform.position + player.transform.forward * 4f;
        Vector3 playerPosition = player.transform.position;
        Vector3 finalPosition = playerPosition + offset;
        //Quaternion finalRotation = Quaternion.Euler(25, 180, 0);
        Quaternion finalRotation = Quaternion.Euler(-5, 0, 0);

        Vector3 lerpedPos = Vector3.Lerp(Camera.main.transform.position, finalPosition, lerpSpeed * Time.deltaTime);
        Quaternion lerpedRot = Quaternion.Lerp(Camera.main.transform.rotation, finalRotation, lerpSpeed * Time.deltaTime);

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovMax + 10f, Time.deltaTime);
        Camera.main.transform.position = lerpedPos;
        Camera.main.transform.rotation = lerpedRot;
    }

    private void RunningFinishedCameraPosition()
    {
        playerCamera.enabled = false;
        float lerpSpeed = 2f;
        Vector3 offset = new Vector3(0f, 2f, 1f);
        Vector3 playerPosition = player.transform.position + player.transform.forward * 4f;
        Vector3 finalPosition = playerPosition + offset;
        Quaternion finalRotation = Quaternion.Euler(13, 180, 0);

        Vector3 lerpedPos = Vector3.Lerp(Camera.main.transform.position, finalPosition, lerpSpeed * Time.deltaTime);
        Quaternion lerpedRot = Quaternion.Lerp(Camera.main.transform.rotation, finalRotation, lerpSpeed * Time.deltaTime);

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovMin + 10f, Time.deltaTime);
        Camera.main.transform.position = lerpedPos;
        Camera.main.transform.rotation = lerpedRot;
    }

    private void JumpingFinishedCameraPosition()
    {
        playerCamera.enabled = false;
        float lerpSpeed = 2f;
        Vector3 offset = new Vector3(0f, 2f, 1f);
        Vector3 playerPosition = player.transform.position + player.transform.forward * 4f;
        Vector3 finalPosition = playerPosition + offset;
        Quaternion finalRotation = Quaternion.Euler(13, 180, 0);

        Vector3 lerpedPos = Vector3.Lerp(Camera.main.transform.position, finalPosition, lerpSpeed * Time.deltaTime);
        Quaternion lerpedRot = Quaternion.Lerp(Camera.main.transform.rotation, finalRotation, lerpSpeed * Time.deltaTime);

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovMin + 10f, Time.deltaTime);
        Camera.main.transform.position = lerpedPos;
        Camera.main.transform.rotation = lerpedRot;
    }

    public void SetPlayerFinishedBool(Component sender, object data)
    {
        if(data is bool)
        {
            bool playerIsFinished = (bool)data;
            playerFinished = playerIsFinished;
        }
    }

    IEnumerator WaitBeforeMovingTheCamera()
    {
        yield return new WaitForSeconds(1.5f);
        RunningFinishedCameraPosition();
    }
}

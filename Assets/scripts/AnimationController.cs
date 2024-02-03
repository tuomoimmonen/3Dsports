using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    public static AnimationController instance;

    Animator anim;

    private float playerSpeed;

    int sceneIndex;
    [SerializeField] float minAnimSpeed = 1.0f;
    [SerializeField] float maxAnimSpeed = 2.0f;
    [SerializeField] float maxPlayerSpeed = 25.0f;

    bool playerCanMove = true;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        anim = GetComponent<Animator>();

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        switch (sceneIndex)
        {
            case 1:
                anim.SetTrigger("runState");
            break; 
            
            case 2:
                anim.SetTrigger("jumpState");
            break;

            case 3:
                anim.SetTrigger("javelinState");
            break;
        }
    }

    void Update()
    {
        SetAnimations();
    }

    private void SetAnimations()
    {
        AnimatorStateInfo animatorState = anim.GetCurrentAnimatorStateInfo(0);

        anim.SetFloat("zVelocity", playerSpeed, 0.1f, Time.deltaTime);

        if (animatorState.IsName("run/idle") && playerCanMove)
        {
            float normalizedSpeed = playerSpeed / maxPlayerSpeed; // value 0-1
            normalizedSpeed = Mathf.Clamp01(normalizedSpeed);
            float remappedSpeed = minAnimSpeed + normalizedSpeed * (maxAnimSpeed - minAnimSpeed); //remap the normalized speed to min max animator speed

            anim.speed = Mathf.Lerp(anim.speed, remappedSpeed, Time.deltaTime);
        }
        else { anim.speed = 1f; }
    }

    public void SetAnimationTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public void GetPlayerSpeed(Component sender, object data)
    {
        if(data is float)
        {
            playerSpeed = (float)data;
        }
    }

    public void CanPlayerMove(Component sender, object data)
    {
        if(data is bool)
        {
            playerCanMove = (bool)data;
        }
    }

}

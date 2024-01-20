using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MotionBlurController : MonoBehaviour
{
    MotionBlur motionBlur;

    private void Awake()
    {
        motionBlur = GetComponent<MotionBlur>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

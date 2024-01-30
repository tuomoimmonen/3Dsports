using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem runEffect;

    bool canPlayParticles = false;

    private void Awake()
    {
        runEffect = GetComponent<ParticleSystem>();

    }
    void Start()
    {

    }

    void Update()
    {
    }

    public void SetCanPlayParticlesBool(Component sender, object data)
    {
        if(data is bool)
        {
            bool shouldPlayParticles = (bool)data;
            canPlayParticles = shouldPlayParticles;

            if(canPlayParticles) { runEffect.Play(); }
            else if (!canPlayParticles) { runEffect.Stop(); }

        }
    }
}

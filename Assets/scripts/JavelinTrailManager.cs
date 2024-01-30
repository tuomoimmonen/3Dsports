using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinTrailManager : MonoBehaviour
{
    [SerializeField] TrailRenderer javelinTrail;

    bool canShowTrail;
    void Start()
    {
        javelinTrail.enabled = false;
    }

    void Update()
    {
        
    }

    public void ChangeCanShowTrailbool(Component sender, object data)
    {
        if(data is bool)
        {
            bool isAbleToShowTrail = (bool)data;
            javelinTrail.enabled = isAbleToShowTrail;
        }
    }


}

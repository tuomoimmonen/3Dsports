using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] float speed = 1f; //speed for moving object
    private RectTransform rectTransform; //ui transform component

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float newPosition = Mathf.PingPong(Time.time * speed, 1); //oscillates between 0 and (parameter 1) with speed value
        rectTransform.anchorMin = new Vector2(newPosition, rectTransform.anchorMin.y); //lower left corner of the parents container
        rectTransform.anchorMax = new Vector2(newPosition, rectTransform.anchorMax.y); //top right corner of the parents container
    }
}

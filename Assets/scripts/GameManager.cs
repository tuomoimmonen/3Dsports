using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { menu, running, jumping, javelin, end };
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    GameState state;

    GameObject javelinObject;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        javelinObject = GameObject.FindGameObjectWithTag("Javelin");
    }
    void Start()
    {
        if(state != GameState.javelin)
        {
            javelinObject.SetActive(false);
        }
    }

    void Update()
    {
        SearchJavelinObject();
    }

    private void SearchJavelinObject()
    {
        if (javelinObject == null)
        {
            javelinObject = GameObject.FindGameObjectWithTag("Javelin");

            if (state != GameState.javelin)
            {
                javelinObject.SetActive(false);
            }
        }
    }
}

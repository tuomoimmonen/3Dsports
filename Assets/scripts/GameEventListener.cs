using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent; //what event to listen

    public CustomGameEvent response; //can link events from the editor

    void OnEnable()
    {
        gameEvent.RegisterListener(this); //tune in to the radio channel
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this); //dont listen to the radio
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}

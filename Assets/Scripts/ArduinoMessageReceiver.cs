using System;
using UnityEngine;

public abstract class ArduinoMessageReceiver : MonoBehaviour
{
    protected MessageType messageTypeFilter;

    private void Start()
    {
        ArduinoBridge.onReceiveMessage += ReceiveMessage;
    }

    private void ReceiveMessage(ArduinoMessageData data)
    {
        if (data.type == MessageType.NONE)
        {
            Debug.LogWarning(transform.name + "'s ArduinoMessageReceiver's filter is set to NONE, " +
                             "not allowing any data to be received");
            return;
        }
        
        if (data.type == messageTypeFilter)
        {
            switch (data.type)
            {
                default:
                    if (float.TryParse(data.value, out float floatVal))
                    {
                        ProcessFloatData(floatVal);
                    }

                    break;
                
                case MessageType.NFC:
                    ProcessStringData(data.value);
                    break;
            }
        }
    }

    protected virtual void ProcessStringData(string value){}
    protected virtual void ProcessFloatData(float value){}
}

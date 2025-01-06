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
                        ParseFloatData(floatVal);
                    }

                    break;
                
                case MessageType.NFC:
                    ParseStringData(data.value);
                    break;
            }
        }
    }

    protected abstract void ParseStringData(string value);
    protected abstract void ParseFloatData(float value);
}

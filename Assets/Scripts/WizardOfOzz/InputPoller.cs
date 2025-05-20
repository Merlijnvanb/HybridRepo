using System;
using UnityEngine;

public class InputPoller : ArduinoMessageReceiver
{
    private void Awake()
    {
        messageTypeFilter = MessageType.BUTTON;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OzManager.Instance.StartGame();
        }
        
        // Knop, Draaiding, NFC Chips
    }

    protected override void ProcessFloatData(float value)
    {
        OzManager.Instance.StartGame();
    }
}

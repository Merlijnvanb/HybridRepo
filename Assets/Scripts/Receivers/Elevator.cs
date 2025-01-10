using System;
using UnityEngine;

public class Elevator : ArduinoMessageReceiver
{
    [SerializeField] private float moveScale;
    private float lastValue = 0;
    
    private void Awake()
    {
        messageTypeFilter = MessageType.ROPE_ELEVATOR;
    }

    protected override void ProcessFloatData(float value)
    {
        float diff = (value - lastValue) *moveScale;
        transform.Translate(Vector3.up*diff);
        
        lastValue = value;
    }
}

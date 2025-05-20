using System;
using UnityEngine;

public class Elevator : ArduinoMessageReceiver
{
    [SerializeField] private LiftController lift;
    [SerializeField] private float inputLockTime;
    private float lastValue = 0;
    private float lockedInSign = 0;
    private float lastInput = Mathf.NegativeInfinity;
    
    private void Awake()
    {
        messageTypeFilter = MessageType.CRANK;
    }

    protected override void ProcessFloatData(float value)
    {
        if (lastInput + inputLockTime < Time.time)
        {
            if (lastValue < value)
            {
                lockedInSign = 1;
            }
            else if (lastValue > value)
            {
                lockedInSign = -1;
            }
            lastInput = Time.time;
        }

        lift.MoveLift(lockedInSign);
        
        lastValue = value;
    }
}

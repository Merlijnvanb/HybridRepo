using UnityEngine;

public class Blower : ArduinoMessageReceiver
{
    [SerializeField] private BlowingGame game;
    
    void Start()
    {
        messageTypeFilter = MessageType.BUTTON;
    }

    protected override void ProcessFloatData(float value)
    {
        game.Blow();
    }
}

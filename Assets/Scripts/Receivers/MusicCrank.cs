using UnityEngine;

public class MusicCrank : ArduinoMessageReceiver
{
    [SerializeField] private MusicGame game;
    void Start()
    {
        messageTypeFilter = MessageType.CRANK;
    }

    protected override void ProcessFloatData(float value)
    {
        game.Crank();
    }
}

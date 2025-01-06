using System;
using UnityEngine;

public class Puzzle : ArduinoMessageReceiver
{
    public bool isCompleted { get; protected set; }
    public event Action onPuzzleCompleted;

    public virtual void ResetPuzzle()
    {
        isCompleted = false;
    }
}

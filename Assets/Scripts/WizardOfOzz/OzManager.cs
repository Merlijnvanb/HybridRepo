using UnityEngine;

public class OzManager : MonoBehaviour
{
    public static OzManager Instance { get; private set; }

    public event System.Action OnGameStart;
    public event System.Action OnMusicCompleted;
    public event System.Action OnEnding;
    
    public OzState CurrentOzState;
    public bool LiftDown;
    public bool LiftUp;
    public bool PickedUpKernels;
    public bool DeliveredKernels;
    public bool AtMusic;
    public bool IsMusicCompleted;
    public bool IsBlowingCompleted;
    public bool IsIngredientsCompleted;
    
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
        } 
        else 
        { 
            Instance = this;
        }

        CurrentOzState = OzState.IDLE;
    }
    
    public void StartGame()
    {
        if (CurrentOzState == OzState.IDLE)
        {
            OnGameStart?.Invoke();
            CurrentOzState = OzState.PLAYING;
        }
    }

    public void MusicCompleted()
    {
        IsMusicCompleted = true;
        OnMusicCompleted?.Invoke();
    }

    public void BlowingCompleted()
    {
        IsBlowingCompleted = true;
        CheckEnding();
    }

    public void IngredientsCompleted()
    {
        IsIngredientsCompleted = true;
        CheckEnding();
    }

    private void CheckEnding()
    {
        if (IsBlowingCompleted && IsIngredientsCompleted)
        {
            OnEnding?.Invoke();
            CurrentOzState = OzState.END;
        }
    }
    
    public enum OzState
    {
        IDLE,
        PLAYING,
        END
    }
}

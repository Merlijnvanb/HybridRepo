using System;
using UnityEngine;

public class BlowingGame : MonoBehaviour
{
    public Transform ArrowPivot;
    public float RequiredTime;
    public float ButtonModifier;
    public AudioSource blowingSound; // Assign this in the inspector
    
    private float PivotMax;
    private float currentTime;

    void Start()
    {
        PivotMax = ArrowPivot.localRotation.eulerAngles.z;
    }

    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING || 
            !OzManager.Instance.IsMusicCompleted || 
            OzManager.Instance.IsBlowingCompleted)
            return;

        HandlePivot();

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentTime += ButtonModifier;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        currentTime = Mathf.Max(currentTime, 0);

        // Sound logic
        if (currentTime > 0)
        {
            if (!blowingSound.isPlaying)
            {
                blowingSound.Play();
            }
        }
        else
        {
            if (blowingSound.isPlaying)
            {
                blowingSound.Pause(); // or .Stop() if you want it to reset
            }
        }

        Debug.Log("Current time: " + currentTime + ", Required time: " + RequiredTime);

        if (currentTime >= RequiredTime)
            OzManager.Instance.BlowingCompleted();
    }

    public void Blow()
    {
        currentTime += ButtonModifier;
    }

    private void HandlePivot()
    {
        var t = currentTime / RequiredTime;
        var newPivot = Mathf.Lerp(PivotMax, -PivotMax, t);

        ArrowPivot.localRotation = Quaternion.Euler(0, 0, newPivot);
    }
}

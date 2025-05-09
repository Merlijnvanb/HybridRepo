using System;
using UnityEngine;

public class BlowingGame : MonoBehaviour
{
    public Transform ArrowPivot;
    public float RequiredTime;
    public float ButtonModifier;
    
    private float PivotMax;
    private float currentTime;

    void Start()
    {
        PivotMax = ArrowPivot.localRotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING || !OzManager.Instance.IsMusicCompleted || OzManager.Instance.IsBlowingCompleted)
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

        Debug.Log("Current time: " + currentTime + ", Required time: " + RequiredTime);
        
        if (currentTime >= RequiredTime) // add logic for staying in threshold
            OzManager.Instance.BlowingCompleted();
    }

    private void HandlePivot()
    {
        var t = currentTime / RequiredTime;
        var newPivot = Mathf.Lerp(PivotMax, -PivotMax, t);
        
        ArrowPivot.localRotation = Quaternion.Euler(0, 0, newPivot);
    }
}

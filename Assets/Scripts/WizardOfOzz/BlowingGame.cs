using UnityEngine;

public class BlowingGame : MonoBehaviour
{
    public float RequiredTime;
    public float ButtonModifier;
    
    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING || !OzManager.Instance.IsMusicCompleted)
            return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentTime += Time.deltaTime * ButtonModifier;
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
}

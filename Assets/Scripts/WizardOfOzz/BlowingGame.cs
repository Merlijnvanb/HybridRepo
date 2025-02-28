using UnityEngine;

public class BlowingGame : MonoBehaviour
{
    public float RequiredTime;

    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING && !OzManager.Instance.IsMusicCompleted)
            return;

        if (Input.GetKey(KeyCode.B))
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
        currentTime = Mathf.Max(currentTime, 0);

        if (currentTime >= RequiredTime)
            OzManager.Instance.BlowingCompleted();
    }
}

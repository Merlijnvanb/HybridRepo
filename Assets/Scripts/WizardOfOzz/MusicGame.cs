using UnityEngine;

public class MusicGame : MonoBehaviour
{
    public float RequiredTime;

    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING && !OzManager.Instance.AtMusic)
            return;

        if (Input.GetKey(KeyCode.M))
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
        currentTime = Mathf.Max(currentTime, 0);

        if (currentTime >= RequiredTime)
            OzManager.Instance.MusicCompleted();
    }
}

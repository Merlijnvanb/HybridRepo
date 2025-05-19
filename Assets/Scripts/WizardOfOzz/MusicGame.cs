using UnityEngine;

public class MusicGame : MonoBehaviour
{
    public float RequiredTime;
    public AudioSource musicAudioSource;

    private float currentTime;
    private bool hasStartedMusic = false;

    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING && !OzManager.Instance.AtMusic)
            return;

        if (Input.GetKey(KeyCode.M))
        {
            currentTime += Time.deltaTime;

            if (!hasStartedMusic)
            {
                musicAudioSource.Play();
                hasStartedMusic = true;
            }
            else if (!musicAudioSource.isPlaying)
            {
                musicAudioSource.UnPause();
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(currentTime, 0);

            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Pause();
            }
        }

        if (currentTime >= RequiredTime)
        {
            OzManager.Instance.MusicCompleted();
        }
    }
}

using System.Collections;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light LowLight;
    public Light MainLight;
    public Light PointLight;

    void Start()
    {
        OzManager.Instance.OnGameStart += OnStart;
        OzManager.Instance.OnMusicCompleted += OnMusicCompleted;
        OzManager.Instance.OnEnding += OnEnding;

        LowLight.intensity = 0;
        MainLight.intensity = 0;
        PointLight.intensity = 0;
    }

    void Update()
    {
        if (OzManager.Instance.CurrentOzState == OzManager.OzState.END)
        {
            PointLight.intensity = FlickerLight(PointLight.intensity, .25f);
            PointLight.intensity = Mathf.Clamp(PointLight.intensity, 2, 5);
        }
        
        if (OzManager.Instance.CurrentOzState == OzManager.OzState.PLAYING && !OzManager.Instance.IsMusicCompleted)
        {
            LowLight.intensity = FlickerLight(LowLight.intensity, .25f);
            LowLight.intensity = Mathf.Clamp(LowLight.intensity, 3, 6);
        }
        else if (OzManager.Instance.IsMusicCompleted)
        {
            MainLight.intensity = FlickerLight(MainLight.intensity, .1f);
            MainLight.intensity = Mathf.Clamp(MainLight.intensity, 3, 5);
        }
            
    }

    private void OnStart()
    {
        StartCoroutine(IncreaseLightIntensity(LowLight, 5f, 2f)); // Adjust duration as needed
    }

    private void OnMusicCompleted()
    {
        StartCoroutine(IncreaseLightIntensity(LowLight, 2f, 2f));
        StartCoroutine(IncreaseLightIntensity(MainLight, 4f, 2f));
    }

    private void OnEnding()
    {
        StartCoroutine(IncreaseLightIntensity(LowLight, 0f, 3f));
        StartCoroutine(IncreaseLightIntensity(MainLight, 0f, 3f));
        StartCoroutine(IncreaseLightIntensity(PointLight, 3f, 2f));
    }

    private IEnumerator IncreaseLightIntensity(Light light, float targetIntensity, float duration)
    {
        float startIntensity = light.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            yield return null;
        }

        light.intensity = targetIntensity;
    }
    
    private float FlickerLight(float baseIntensity, float range)
    {
        float noise = Random.Range(-range, range);
        return baseIntensity + noise;
    }
}


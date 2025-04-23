using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class IngredientGame : MonoBehaviour
{
    /*
     * Volgorde is:
     * Cirkel, Ster, Hart, Octagon, Diamant
     */

    public Light CorrectLight;
    public Light IncorrectLight;
    public Transform SpawnPoint;
    public float Movespeed;
    public float Duration;

    public GameObject Cirkel;
    public GameObject Ster;
    public GameObject Hart;
    public GameObject Octagon;
    public GameObject Diamant;

    private int currentStep = 0;

    void Update()
    {
        if (OzManager.Instance.IsIngredientsCompleted)
        {
            return; // Puzzle already solved, do nothing.
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CheckStep(0)) // Cirkel
            {
                StartCoroutine(InputVisual(Cirkel, CorrectLight, Duration));
            }
            else
            {
                StartCoroutine(InputVisual(Cirkel, IncorrectLight, Duration));
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckStep(1)) // Ster
            {
                StartCoroutine(InputVisual(Ster, CorrectLight, Duration));
            }
            else
            {
                StartCoroutine(InputVisual(Ster, IncorrectLight, Duration));
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (CheckStep(2)) // Hart
            {
                StartCoroutine(InputVisual(Hart, CorrectLight, Duration));
            }
            else
            {
                StartCoroutine(InputVisual(Hart, IncorrectLight, Duration));
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (CheckStep(3)) // Octagon
            {
                StartCoroutine(InputVisual(Octagon, CorrectLight, Duration));
            }
            else
            {
                StartCoroutine(InputVisual(Octagon, IncorrectLight, Duration));
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckStep(4)) // Diamant
            {
                StartCoroutine(InputVisual(Diamant, CorrectLight, Duration));
            }
            else
            {
                StartCoroutine(InputVisual(Diamant, IncorrectLight, Duration));
            }
        }
    }

    private bool CheckStep(int step)
    {
        if (step == currentStep)
        {
            currentStep++;

            if (currentStep >= 5)
            {
                Debug.Log("Puzzle solved!");
                OnPuzzleSolved();
            }
            return true;
        }
        
        currentStep = 0;
        Debug.Log("Incorrect sequence. Resetting...");
        return false;
    }

    private IEnumerator InputVisual(GameObject go, Light light, float duration)
    {
        var elapsedTime = 0f;
        go.transform.position = SpawnPoint.position;
        var newX = go.transform.position.x;
        go.SetActive(true);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            newX -= Time.deltaTime * Movespeed;
            go.transform.position = new Vector3(newX, go.transform.position.y, go.transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        go.SetActive(false);
        light.intensity = .5f;
        
        while (light.intensity > 0f)
        {
            light.intensity -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        light.intensity = 0;
    }

    private void OnPuzzleSolved()
    {
        OzManager.Instance.IngredientsCompleted();
    }
}
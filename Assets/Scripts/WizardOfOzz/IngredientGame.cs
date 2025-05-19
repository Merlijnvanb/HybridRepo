using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class IngredientGame : MonoBehaviour
{
    /*
     * Volgorde is:
     * Cirkel, Ster, Hart, Octagon, Diamant
     */

    /*
     * NFC chip gelezen met een van de 5 vormen -> 5 komen naast elkaar te staan op de band, wanneer er 5 liggen worden ze weggestuurd
     * dictionary met positions en index -> lerp vormen naar goede positie in index
     */
    
    public Light CorrectLight;
    public Light IncorrectLight;
    public Transform SpawnPoint;
    public float Movespeed;
    public float Duration;
    public float IngredientDist;

    public GameObject Cirkel;
    public GameObject Ster;
    public GameObject Hart;
    public GameObject Octagon;
    public GameObject Diamant;

    public AudioSource Screaming;
    public AudioSource Wrong;
    public AudioSource Right;

    private int currentStep = 0;
    private Vector3[] ingredientPositions = new Vector3[5];
    private List<GameObject> ingredientList = new List<GameObject>();
    private GameObject[] correctSequence = new GameObject[5];

    void Start()
    {
        correctSequence[0] = (Cirkel);
        correctSequence[1] = (Ster);
        correctSequence[2] = (Hart);
        correctSequence[3] = (Octagon);
        correctSequence[4] = (Diamant);
        
        for (int i = 0; i < ingredientPositions.Length; i++)
        {
            ingredientPositions[i] = SpawnPoint.position - new Vector3(IngredientDist * i, 0, 0); 
        }
    }

    void Update()
    {
        if (OzManager.Instance.IsIngredientsCompleted)
        {
            return; // Puzzle already solved, do nothing.
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnVisual(Cirkel);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnVisual(Ster);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnVisual(Hart);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnVisual(Octagon);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnVisual(Diamant);
        }
    }

    public void EnterIngredient(Ingredient ing)
    {
        switch (ing)
        {
            case Ingredient.Cirkel: SpawnVisual(Cirkel); break;
            case Ingredient.Ster: SpawnVisual(Ster); break;
            case Ingredient.Hart: SpawnVisual(Hart); break;
            case Ingredient.Octagon: SpawnVisual(Octagon); break;
            case Ingredient.Diamant: SpawnVisual(Diamant); break;
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

    private IEnumerator StartSequence(List<GameObject> objects, Light light, float duration)
    {
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var move = Time.deltaTime * Movespeed;

            foreach (var obj in objects)
            {
                var moveVector = new Vector3(move, 0, 0);
                obj.transform.position -= moveVector;
            }
            
            yield return null;
        }

        foreach (var go in objects)
        { 
            go.SetActive(false);
            Debug.Log("setting inactive of: " + go.name);
        }
        
        objects.Clear();

        light.intensity = .5f;
        
        while (light.intensity > 0f)
        {
            light.intensity -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        light.intensity = 0;
    }

    private void SpawnVisual(GameObject go)
    {
        foreach (var ingredient in ingredientList)
        {
            if (go == ingredient)
                return;
        }
        
        Debug.Log("Spawning " + go.name + "...");
        var index = ingredientList.Count;
        ingredientList.Add(go);
        go.transform.position = ingredientPositions[index];
        go.SetActive(true);
        
        if (ingredientList.Count >= ingredientPositions.Length)
        {
            var correct = CheckSequence(ingredientList);
            var light = correct ? CorrectLight : IncorrectLight;
            
            if (correct)
                OnPuzzleSolved();
            
            StartCoroutine(StartSequence(ingredientList, light, Duration));
        }
    }

    private bool CheckSequence(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != correctSequence[i])
            {
                Wrong.Play();
                return false;
            }
        }

        Right.Play();
        return true;
    }

    private void OnPuzzleSolved()
    {
        Screaming.Play();
        OzManager.Instance.IngredientsCompleted();
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IngredientReceiver : ArduinoMessageReceiver
{
    [SerializeField] private IngredientGame game;
    [SerializeField] private IngredientTagPair[] pairs;
    private Dictionary<string, Ingredient> dict = new Dictionary<string, Ingredient>();

    private void Awake()
    {
        messageTypeFilter = MessageType.NFC;

        for (int i = 0; i < pairs.Length; i++)
        {
            dict.Add(pairs[i].id.Trim().Normalize(), pairs[i].ingredient);
        }
    }

    protected override void ProcessStringData(string value)
    {
        if (dict.TryGetValue(value.Trim().Normalize(), out Ingredient ing))
        {
            game.EnterIngredient(ing);
            Debug.Log("Added ingredient "+ing);
        }
        Debug.LogWarning("No matching ingredient for id "+value);
    }

    [System.Serializable]
    public struct IngredientTagPair
    {
        public string id;
        public Ingredient ingredient;
    }
}

    
public enum Ingredient
{
        Cirkel,
        Ster,
        Hart,
        Octagon,
        Diamant
}

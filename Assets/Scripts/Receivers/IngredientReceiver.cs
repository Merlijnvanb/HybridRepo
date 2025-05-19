using System;
using System.Collections.Generic;
using UnityEngine;

public class IngredientReceiver : ArduinoMessageReceiver
{
    [SerializeField] private IngredientGame game;
    [SerializeField] private IngredientTagPair[] pairs;
    private Dictionary<int, Ingredient> dict = new Dictionary<int, Ingredient>();
    
    protected override void ProcessStringData(string value)
    {
        int num = int.Parse(value.Substring(0, 2));
        
        if (dict.TryGetValue(num, out Ingredient ing))
        {
            game.EnterIngredient(ing);
            Debug.Log("Added ingredient "+ing);
        }
        Debug.LogWarning("No matching ingredient for id "+num);
    }

    public struct IngredientTagPair
    {
        public int id;
        public Ingredient Ingredient;
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

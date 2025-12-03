using System;
using System.Collections.Generic;
using UnityEngine;

public class Doener : MonoBehaviour
{
    [SerializeField]
    List<Ingredients> Ingredients = new List<Ingredients>();

    public void AddIngredient(Ingredients ingredient)
    {
        Ingredients.Add(ingredient);
    }
    public void RemoveIngredient(Ingredients ingredient)
    {
        Ingredients.Remove(ingredient);
    }

    public List<Ingredients> getIngredients()
    {
        return Ingredients;
    }

    public void OnDestroy()
    {
        
    }
}

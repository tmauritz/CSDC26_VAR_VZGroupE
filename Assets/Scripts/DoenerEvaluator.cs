using System.Collections.Generic;
using UnityEngine;

public class DoenerEvaluator : MonoBehaviour
{
    public static List<Ingredients> EvaluateDoener(Doener doener)
    {
        return doener.getIngredients();
    }
    
    //TODO: Scoring based on Recipe
    
}

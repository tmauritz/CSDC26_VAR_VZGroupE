using UnityEngine;

public enum Ingredients
{
    Salad = 0,
    Tomatoes = 1,
    Onions = 2,
    Meat = 3
}

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Ingredients type;
    public Ingredients Type { get { return type; } }
}

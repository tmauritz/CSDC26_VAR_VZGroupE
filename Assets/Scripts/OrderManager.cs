using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    private Ingredients[] allIngredients;
    private List<Ingredients> currentOrder = new List<Ingredients>();

 
    public TextMeshProUGUI ordersText;
    public TextMeshProUGUI earnedText;

    
    public int moneyPerDoener = 5;
    private int moneyEarned = 0;

    void Start()
    {
        // alle Zutaten ins Array laden
        allIngredients = (Ingredients[])Enum.GetValues(typeof(Ingredients));

        GenerateNewOrder();
        UpdateEarnedDisplay();   
    }

    // Stellt eine zufällige Bestellung zusammen
    private void GenerateNewOrder()
    {
        currentOrder.Clear();

        List<Ingredients> availableIngredients = new List<Ingredients>(allIngredients);

        int maxItems = Mathf.Min(availableIngredients.Count, 4);
        int numberOfItems = UnityEngine.Random.Range(2, maxItems + 1);

        for (int i = 0; i < numberOfItems; i++)
        {
            int index = UnityEngine.Random.Range(0, availableIngredients.Count);
            Ingredients chosen = availableIngredients[index];

            currentOrder.Add(chosen);
            availableIngredients.RemoveAt(index);
        }

        UpdateOrderDisplay();
    }

    // die aktuelle Bestellung in das UI 
    private void UpdateOrderDisplay()
    {
        if (ordersText != null)
        {
            ordersText.text = "";
            foreach (Ingredients ing in currentOrder)
            {
                ordersText.text += "- " + ing.ToString() + "\n";
            }
        }
    }

    // anzeige des verdienten geldes aktualisieren
    private void UpdateEarnedDisplay()
    {
        if (earnedText != null)
        {
            earnedText.text = moneyEarned + " $";
        }
    }

    // wird aufgerufen wenn ein döner korrekt abgegeben wurde
    public void OnCorrectOrderServed()
    {
        moneyEarned += moneyPerDoener;
        UpdateEarnedDisplay();
        GenerateNewOrder();
    }

    // prüfe ob die Zutaten korrekt sind
    public bool CheckOrder(List<Ingredients> playerIngredients)
    {
        if (playerIngredients.Count != currentOrder.Count)
            return false;

        for (int i = 0; i < currentOrder.Count; i++)
        {
            if (playerIngredients[i] != currentOrder[i])
                return false;
        }
        return true;
    }
}

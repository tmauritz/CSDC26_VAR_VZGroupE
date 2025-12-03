using UnityEngine;

public class DoenerDelivery : MonoBehaviour
{
    
    public OrderManager orderManager;

    private void OnTriggerEnter(Collider other)
    {
        // prüfen ob das objekt ein doener ist
        Doener doener = other.GetComponent<Doener>();
        if (doener == null)
            return;

        if (orderManager == null)
        {
            Debug.LogWarning("[DoenerDelivery] Kein OrderManager zugewiesen!");
            return;
        }

        // zutatenliste vom doener holen
        var ingredientsList = doener.getIngredients();

        // mit der aktuellen bestellung vergleichen
        bool correct = orderManager.CheckOrder(ingredientsList);

        if (correct)
        {
            Debug.Log("[DoenerDelivery] Bestellung korrekt!");
            orderManager.OnCorrectOrderServed();
            doener.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("[DoenerDelivery] Bestellung falsch!");
            orderManager.PlayWrongSound();
        }
    }
}

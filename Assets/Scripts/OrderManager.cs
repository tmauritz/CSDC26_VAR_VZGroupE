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

    //Timer
    public float gameTime = 35f;
    private float currentTime;
    public TextMeshProUGUI timerText;

    //Sound
    public AudioClip correctSound; 
    public AudioClip wrongSound;     
    private AudioSource audioSource;
    public AudioClip warningSound;

    private bool warningPlayed = false;

    void Start()
    {
        // alle Zutaten ins Array laden
        allIngredients = (Ingredients[])Enum.GetValues(typeof(Ingredients));

        GenerateNewOrder();
        UpdateEarnedDisplay();

        currentTime = gameTime;
        UpdateTimerDisplay();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("OrderManager: Keine AudioSource gefunden");
        }
        warningPlayed = false;
    }
    void Update()
    {
        
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;

            // Warnsound bei 10 sekunden
            if (!warningPlayed && currentTime <= 10f)
            {
                PlayWarningSound();
                warningPlayed = true;
            }

            if (currentTime < 0f)
                currentTime = 0f;

            UpdateTimerDisplay();

            // Timer abgelaufen?
            if (currentTime <= 0f)
            {
                HandleTimeOut();
            }
        }
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

    // timertext aktualisieren
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // nur ganze sekunden anzeigen
            timerText.text = Mathf.Ceil(currentTime).ToString() + "s";
        }
    }

    private void HandleTimeOut()
    {
        // 5 € abziehen aber nicht unter 0 fallen
        moneyEarned -= moneyPerDoener;
        if (moneyEarned < 0)
            moneyEarned = 0;

        UpdateEarnedDisplay();

        // neue bestellung und timer neu starten
        GenerateNewOrder();
        currentTime = gameTime;
        warningPlayed = false;
        UpdateTimerDisplay();

        Debug.Log("Zeit abgelaufen – 5 € abgezogen und neue Bestellung gestartet");
    }


    // wird aufgerufen wenn ein döner korrekt abgegeben wurde
    public void OnCorrectOrderServed()
    {
        moneyEarned += moneyPerDoener;
        PersistenceManager.addDoenerBuilt(1);
        UpdateEarnedDisplay();
        PlayCorrectSound();
        GenerateNewOrder();

        // timer für die nächste bestellung zurücksetzen
        currentTime = gameTime;
        warningPlayed = false;
        UpdateTimerDisplay();
    }

    public void PlayCorrectSound()
    {
        if (audioSource != null && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
    }

    public void PlayWrongSound()
    {
        if (audioSource != null && wrongSound != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }

    public void PlayWarningSound()
    {
        if (audioSource != null && warningSound != null)
        {
            audioSource.PlayOneShot(warningSound);
        }
    }


    // prüfe ob die Zutaten korrekt sind
    public bool CheckOrder(List<Ingredients> playerIngredients)
    {
        if (playerIngredients.Count != currentOrder.Count)
            return false;

        for (int i = 0; i < currentOrder.Count; i++)
        {
            if (!playerIngredients.Contains(currentOrder[i]))
                return false;
        }
        return true;
    }
}

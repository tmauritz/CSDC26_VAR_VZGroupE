using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// Falls du Fehler bei 'XRGrabInteractable' bekommst, ist dies der Namespace für XRI 3.x (Unity 6 Standard):
using UnityEngine.XR.Interaction.Toolkit.Interactables; 

public class SimpleItemSpawner : MonoBehaviour
{
    [Header("Einstellungen")]
    public GameObject itemPrefab; // Das Objekt, das gespawnt werden soll
    public float respawnDelay = 1.0f; // Wartezeit

    private GameObject currentItem;

    void Start()
    {
        SpawnItem();
    }

    void SpawnItem()
    {
        // 1. Objekt an der Position des Spawners erstellen
        currentItem = Instantiate(itemPrefab, transform.position, transform.rotation);

        // 2. Wir greifen auf das XRGrabInteractable zu
        // Hinweis: In Unity 6 (XRI 3.x) heißt die Klasse XRGrabInteractable.
        XRGrabInteractable interactable = currentItem.GetComponent<XRGrabInteractable>();

        if (interactable != null)
        {
            // 3. Wir hören auf das Event "selectEntered" (wenn jemand greift)
            interactable.selectEntered.AddListener(OnItemGrabbed);
        }
        else
        {
            Debug.LogError($"Das Prefab '{itemPrefab.name}' hat kein XRGrabInteractable!");
        }
    }

    // Dieser Code wird ausgeführt, wenn das Objekt gegriffen wird
    private void OnItemGrabbed(SelectEnterEventArgs args)
    {
        // WICHTIG: Listener entfernen, damit dieses spezifische Objekt
        // den Spawner nicht NOCHMAL auslöst, wenn man es fallen lässt und wieder greift.
        XRGrabInteractable interactable = currentItem.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnItemGrabbed);
        }

        // Timer starten
        StartCoroutine(WaitAndRespawn());
    }

    IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnItem();
    }
}
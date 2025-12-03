using TMPro;
using UnityEngine;

public class KebabCounterDisplay : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI KebabCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int totalDoeners = PersistenceManager.loadDoenerStat();
        Debug.Log("Total Doeners: " + totalDoeners);
        KebabCounter.text = "Total Kebabs built: " + totalDoeners;
    }

}

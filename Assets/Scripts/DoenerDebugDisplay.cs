using TMPro;
using UnityEngine;

public class DoenerDebugDisplay : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshPro textMesh;

    public void OnTriggerEnter(Collider other)
    {
        var doener = other.GetComponent<Doener>();
        if (doener != null)
        {
            var ingredientsList = DoenerEvaluator.EvaluateDoener(doener);
            textMesh.text = "Ingredients:\n";
            foreach (var ingredient in ingredientsList)
            {
                textMesh.text += ingredient + "\n";
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        textMesh.text = "Ingredients:\n";
    }
}

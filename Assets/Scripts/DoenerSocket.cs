using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class DoenerSocket : MonoBehaviour
{
    [FormerlySerializedAs("rigidbodyColliderbase")] [SerializeField]
    public Collider rigidbodyColliderBase;
    [SerializeField]
    private Doener doener;

    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, true);
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            doener.AddIngredient(ingredient.Type);
        }
    }
    
    public void OnSelectExited(SelectExitEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, false);
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            doener.RemoveIngredient(ingredient.Type);
        }
    }
    
    private void SocketCollisionsIgnored(GameObject other, bool flag)
    {        
        Debug.Log("SocketCollisionsIgnored" + flag + other.name);
        var theirColliders = other.GetComponentsInChildren<Collider>(true);
    
        // overkill - all (A,B) pairs will be duplicated (B,A) - optimise?
        foreach (var cB in theirColliders)
            Physics.IgnoreCollision(rigidbodyColliderBase, cB, flag);
    }
}

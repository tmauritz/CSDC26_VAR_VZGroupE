using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoenerSocket : MonoBehaviour
{
    [FormerlySerializedAs("rigidbodyColliderbase")] [SerializeField]
    public Collider rigidbodyColliderBase;
    [SerializeField]
    private Doener doener;

    private XRSocketInteractor _socketInteractor;

    public void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
    }

    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, true);
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            other.transform.parent = doener.gameObject.transform;
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
            other.transform.parent = null;
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

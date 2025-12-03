using System;
using UnityEngine;
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
            doener.AddIngredient(ingredient.Type);
            _socketInteractor.showInteractableHoverMeshes = false;
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
            _socketInteractor.showInteractableHoverMeshes = true;
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

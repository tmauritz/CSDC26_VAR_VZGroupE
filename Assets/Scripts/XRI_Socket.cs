using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEvents : MonoBehaviour
{
    public Collider rigidbodyColliderbase;
        
    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, true);
    }
    
    public void OnSelectExited(SelectExitEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, false);
    }
    
    private void SocketCollisionsIgnored(GameObject other, bool flag)
    {        
        Debug.Log("SocketCollisionsIgnored" + flag + other.name);
        var theirColliders = other.GetComponentsInChildren<Collider>(true);
    
        // overkill - all (A,B) pairs will be duplicated (B,A) - optimise?
        foreach (var cB in theirColliders)
            Physics.IgnoreCollision(rigidbodyColliderbase, cB, flag);
    }
}

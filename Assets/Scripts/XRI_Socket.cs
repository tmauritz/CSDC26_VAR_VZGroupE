using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEvents : MonoBehaviour
{
    public Collider rigidbodyColliderbase;
        
    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, false);
    }
    
    public void OnSelectExited(SelectEnterEventArgs arg0)
    {
        var other = arg0.interactableObject.transform.gameObject;
        SocketCollisionsIgnored(other, true);
    }
    
    private void SocketCollisionsIgnored(GameObject other, bool flag)
    {        
        var theirColliders = other.GetComponentsInChildren<Collider>(true);
    
        // overkill - all (A,B) pairs will be duplicated (B,A) - optimise?
        foreach (var cB in theirColliders)
            Physics.IgnoreCollision(rigidbodyColliderbase, cB, flag);
    }
}

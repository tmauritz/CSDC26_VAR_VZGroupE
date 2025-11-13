using System;
using Oculus.Interaction;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] public GameObject attachPoint;
    [SerializeField] public GameObject socketBase;
    [SerializeField] public Material baseMaterialPassive;
    [SerializeField] public Material baseMaterialActive;
    [SerializeField] public LayerMask layer;
    [SerializeField] public Rigidbody parentRigidbody;
    
    private bool objectInSocket = false;
    private ConfigurableJoint joint = null;
    
    private GameObject targetGameObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if(!objectInSocket && ((layer.value & (1 << other.gameObject.layer)) > 0))
        {
            targetGameObject = other.gameObject;
            Grabbable grabbable = targetGameObject.GetComponent<Grabbable>();

            if (grabbable != null && (grabbable.SelectingPointsCount > 0))
            {
                socketBase.GetComponent<MeshRenderer>().material = baseMaterialActive;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(!objectInSocket && ((layer.value & (1 << other.gameObject.layer)) > 0))
        {
            Rigidbody targetRigidbody = targetGameObject.GetComponent<Rigidbody>();
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable != null && (grabbable.SelectingPointsCount == 0) && !objectInSocket)
            {   
                joint = parentRigidbody.gameObject.AddComponent<ConfigurableJoint>();
                joint.connectedBody = targetRigidbody;
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                joint.angularXMotion = ConfigurableJointMotion.Locked;
                joint.angularYMotion = ConfigurableJointMotion.Locked;
                joint.angularZMotion = ConfigurableJointMotion.Locked;
                joint.massScale = 1f;
                joint.connectedMassScale = 1f;
                joint.enableCollision = false;
                objectInSocket = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //only break the joint if the target object is grabbed
        if (targetGameObject.GetComponent<Grabbable>().SelectingPointsCount > 0)
        {
            targetGameObject = null;
            socketBase.GetComponent<MeshRenderer>().material = baseMaterialPassive;
            clearJoint();
        }
    }

    private void clearJoint()
    {
        Destroy(joint);
        objectInSocket = false;
    }
}

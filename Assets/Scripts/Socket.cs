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
    
    private bool objectInSocket = false;
    
    private GameObject targetGameObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if((layer.value & (1 << other.gameObject.layer)) > 0)
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
        if((layer.value & (1 << other.gameObject.layer)) > 0 && !objectInSocket)
        {
            Rigidbody targetRigidbody = targetGameObject.GetComponent<Rigidbody>();
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable != null && (grabbable.SelectingPointsCount == 0) && (objectInSocket == false))
            {
                targetGameObject.transform.parent = attachPoint.transform;
                targetGameObject.transform.position = attachPoint.transform.position;
                targetGameObject.transform.rotation = attachPoint.transform.rotation;
                targetRigidbody.useGravity = false;
                targetRigidbody.isKinematic = true;
                targetRigidbody.detectCollisions = false;
                objectInSocket = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetGameObject = null;
        socketBase.GetComponent<MeshRenderer>().material = baseMaterialPassive;
        objectInSocket = false;
    }
}

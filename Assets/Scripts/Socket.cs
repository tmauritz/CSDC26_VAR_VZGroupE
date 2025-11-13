using System;
using Oculus.Interaction;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] public GameObject attachPoint;
    [SerializeField] public GameObject socketBase;
    [SerializeField] public Material baseMaterialPassive;
    [SerializeField] public Material baseMaterialActive;
    [SerializeField] public LayerMask attachableLayer;

    private bool _objectInSocket = false;

    private GameObject _targetGameObject;
    private Transform _oldParent;
    private Rigidbody _rigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if ((attachableLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            _targetGameObject = other.gameObject;
            _oldParent = other.transform.parent;
            Grabbable grabbable = _targetGameObject.GetComponent<Grabbable>();

            if (grabbable != null && (grabbable.SelectingPointsCount > 0))
            {
                socketBase.GetComponent<MeshRenderer>().material = baseMaterialActive;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((attachableLayer.value & (1 << other.gameObject.layer)) > 0 && !_objectInSocket)
        {
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable != null && (grabbable.SelectingPointsCount == 0) && (_objectInSocket == false))
            {
                socket(_targetGameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject != _targetGameObject) return;
        unsocket(_targetGameObject);
        _targetGameObject = null;
        socketBase.GetComponent<MeshRenderer>().material = baseMaterialPassive;
    }

    private void socket(GameObject target)
    {
        if (target == null) return;
        _targetGameObject = target;
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        target.transform.parent = attachPoint.transform;
        target.transform.position = attachPoint.transform.position;
        target.transform.rotation = attachPoint.transform.rotation;
        targetRigidbody.isKinematic = true;
        _objectInSocket = true;
    }

    private void unsocket(GameObject target)
    {
        if (target == null) return;
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        target.transform.parent = _oldParent;
        targetRigidbody.isKinematic = false;
        _objectInSocket = false;
    }
}
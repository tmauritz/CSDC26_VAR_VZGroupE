using System;
using UnityEngine;

public class CubeActions : MonoBehaviour
{
    
    private Rigidbody _cubeRigidBody;

    private void Awake()
    {
        _cubeRigidBody = GetComponent<Rigidbody>();
    }

    public void freezeRigidBody()
    {
        _cubeRigidBody.isKinematic = true;
    }
    
}

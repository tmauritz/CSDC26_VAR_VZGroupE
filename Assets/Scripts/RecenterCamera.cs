using UnityEngine;
using UnityEngine.InputSystem;

public class RecenterCamera : MonoBehaviour
{
    [SerializeField] private GameObject cameraPosition;
    public InputActionReference resetCameraAction;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = cameraPosition.transform.position;
    }

    private void Awake()
    {
        resetCameraAction.action.Enable();
        resetCameraAction.action.performed += Recenter;
    }

    /*private void OnDestroy() 
    {
        resetCameraAction.action.Disable();
    }*/

    private void Recenter(InputAction.CallbackContext context)
    {
        Vector3 currentPosition = cameraPosition.transform.position;
        Debug.Log("Position of camera is: " + currentPosition);
        float offset = (currentPosition.y - startingPosition.y) * -1;

        transform.position = new Vector3(0, startingPosition.y, 0);
        Debug.Log("New position is " + transform.position + ", offset is " + offset);
    }

}

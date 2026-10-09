using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Transform _orientationTransform;

    private void Start()
    {
        CursorLocking();
        CameraCheck();

    }

    private void Update()
    {
       
        CameraMovement();
    }

    private void CursorLocking()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CameraCheck()
    {
        if (_cameraTransform == null && Camera.main != null)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    private void CameraMovement()
    {
        if (_cameraTransform == null || _orientationTransform == null) { return; }

        Vector3 viewDirection = _cameraTransform.forward;
        viewDirection.y = 0f;

        _orientationTransform.forward = viewDirection.normalized;
    }

}


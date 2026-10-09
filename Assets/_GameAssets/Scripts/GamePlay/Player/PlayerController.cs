using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    [Header("References")]

    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]

    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _groundDrag;

    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput
        + _orientationTransform.right * _horizontalInput;

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        _playerRigidbody.linearDamping = _groundDrag;
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 _limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity =
            new Vector3(_limitedVelocity.x, _playerRigidbody.linearVelocity.y, _limitedVelocity.z);

        }
    }

    public void ApplySlow(float _slowDuration)
    {
        _movementSpeed = 15f;
        Invoke(nameof(ResetSpeed), _slowDuration);
    }
    
    private void ResetSpeed()
    {
        _movementSpeed = 50f;
    }
}

using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance = 3f;

    private void Update() 
    {
        if(!Input.GetKeyDown(KeyCode.E)) {return;}

        if(TryGetInteractable(out var interactable))
        {
            interactable.Interact();
        }
    }
    private bool TryGetInteractable(out IInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, _interactDistance))
        {
            interactable = hit.collider.GetComponentInParent<IInteractable>();
            return interactable != null;
        }
        
        return false;
    } 
}

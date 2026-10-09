using UnityEngine;

public class TrapController : MonoBehaviour, IInteractable
{
    [Header("Effects")]
    [SerializeField] private float _healthReward = 10f;
    [SerializeField] private float _slowDuration = 3f;       

    private bool _isResolved = false;

    public void Interact()
    {
        if (_isResolved) return;
        _isResolved = true;

        if (EcosystemManager.Instance != null)
        {
            EcosystemManager.Instance.AddHealth(_healthReward);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isResolved) return;

        if (other.CompareTag("Player"))
        {
            _isResolved = true;

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySlow(_slowDuration);
                Debug.Log("Yavaşladı");
                
            }

            Destroy(gameObject);
        }
    }
}
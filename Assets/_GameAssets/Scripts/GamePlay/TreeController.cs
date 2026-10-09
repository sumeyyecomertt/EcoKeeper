using UnityEngine;

public class TreeController : MonoBehaviour, IInteractable
{
   [Header("Sapling Prefab")]
   [SerializeField] private GameObject _saplingPrefab;

   [Header("Current State")]
   [SerializeField] private TreeState _currentState = TreeState.Healthy;

   [Header("Ecosystem Values")]
   [SerializeField] private float _healthPenalty = 15f;
   [SerializeField] private float _healthAward = 20f;

   private Collider _collider;

   private void Awake()
   {
      _collider = GetComponent<Collider>();
   }

   [ContextMenu("Debug : Fall tree")]

   public void ChopDown()
   {
      if (_currentState != TreeState.Healthy) { return; }

      _currentState = TreeState.Fallen;

      transform.Rotate(0f, 0f, 90f, Space.Self);

      if (EcosystemManager.Instance != null)
      {
         EcosystemManager.Instance.DecreaseHealth(_healthPenalty);
      }
      if (_collider != null) { _collider.enabled = true; }
   }
   public void Interact()
   {
      if (_currentState != TreeState.Fallen) { return; }

      if (EcosystemManager.Instance != null)
      {
         EcosystemManager.Instance.AddHealth(_healthAward);
      }

      if (_saplingPrefab != null)
      {
         Instantiate(_saplingPrefab, transform.position, _saplingPrefab.transform.rotation);
      }

      Destroy(gameObject);
   }
   public TreeState CurrentState => _currentState;
}
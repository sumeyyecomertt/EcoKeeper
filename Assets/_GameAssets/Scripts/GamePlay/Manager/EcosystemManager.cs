using UnityEngine;
using UnityEngine.UI;

public class EcosystemManager : MonoBehaviour
{
    public static EcosystemManager Instance {get; private set;}

    [Header ("UI References")]

    [SerializeField] private Slider _healthSlider;

    [Header ("Health Settings")]

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 50f;

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if(_healthSlider != null)
        {
            _healthSlider.minValue = 0f;
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = _currentHealth;
        }
    }

    public void AddHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0f, _maxHealth);
        UpdateUI();
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0f, _maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(_healthSlider != null)
        {
            _healthSlider.value = _currentHealth;
        }
    }
}

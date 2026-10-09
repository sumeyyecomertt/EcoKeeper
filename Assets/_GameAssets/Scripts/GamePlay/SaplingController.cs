using System.Threading;
using UnityEngine;

public class SaplingController : MonoBehaviour
{
    [Header("Growth Settings")]
    [SerializeField] private float _growthDuration = 30f;
    [SerializeField] private GameObject[] _treeVarieties;

    [Header("Ecosystem Values")]
    [SerializeField] private float _fullGrowthBonus = 10f;

    private float _timer;

    private void Update()
    {
      _timer += Time.deltaTime;

      if(_timer >= _growthDuration)
        {
            GrowIntoAdultTree();
        } 
    }

    private void GrowIntoAdultTree()
    {
        if (_treeVarieties != null && _treeVarieties.Length > 0)
        {
            int randomIndex = Random.Range(0, _treeVarieties.Length);

            GameObject selectedTree = _treeVarieties[randomIndex];

            if (selectedTree != null)
            {
                Instantiate(selectedTree, transform.position, selectedTree.transform.rotation);
            }
        }

        if (EcosystemManager.Instance != null)
        {
            EcosystemManager.Instance.AddHealth(_fullGrowthBonus);
        }

        Destroy(gameObject);
    }
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoacherManager : MonoBehaviour
{
    [Header("Target and Distance")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minChopDistance = 25f;

    [Header("Time Settings")]
    [SerializeField] private float _minChopInterval = 45f;
    [SerializeField] private float _maxChopInterval = 90f;
    [SerializeField] private bool _autoChopActive = true;

    private float _currentInterval;
    private float _timer;

    private void Start()
    {
        if (_playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _playerTransform = playerObj.transform;
            }
        }
        SetNewInterval();
    }
    private void Update()
    {
        if (!_autoChopActive) { return; }

        _timer += Time.deltaTime;

        if (_timer >= _currentInterval)
        {
            _timer = 0f;
            SetNewInterval();
            ChopRandomTree();
        }
    }

    private void SetNewInterval()
    {
        _currentInterval = Random.Range(_minChopInterval, _maxChopInterval);
    }

    [ContextMenu("Debug: Fall Random Tree")]
    private void ChopRandomTree()
    {
        TreeController[] allTrees = Object.FindObjectsByType<TreeController>();

        List<TreeController> eligibleTrees = new List<TreeController>();

        foreach (var tree in allTrees)
        {
            if (tree.CurrentState != TreeState.Healthy) continue;

            if (_playerTransform != null)
            {
                float distance = Vector3.Distance(_playerTransform.position, tree.transform.position);
                if (distance < _minChopDistance) continue;
            }
            eligibleTrees.Add(tree);
        }

        if (eligibleTrees.Count == 0)
        {
            Debug.LogWarning("No trees left for fall in eligible distance!");
            return;
        }

        int randomIndex = Random.Range(0, eligibleTrees.Count);
        eligibleTrees[randomIndex].ChopDown();
    }
}

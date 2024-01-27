using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private BonusPoint _bonusPointPrefab;
    private BonusPoint _currentBonusPoint;

    [SerializeField] private float _badProbability;
    private bool _isGood;

    [SerializeField] private Vector2 _spawnPos;
    [SerializeField] private float _incSpeed;
    [SerializeField] private float _lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBonusPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnNewBonusPoint()
    {
        if (_currentBonusPoint != null) Destroy(_currentBonusPoint.gameObject);
        _currentBonusPoint = Instantiate(_bonusPointPrefab, _spawnPos, _bonusPointPrefab.transform.rotation);
        _currentBonusPoint.SetBonusPoint(_incSpeed, _lifeTime);
    }

    void SetParameters()
    {
        
    }
}

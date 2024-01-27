using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private BonusPoint _bonusPointPrefab;
    private BonusPoint _currentBonusPoint;

    [SerializeField] private BadPoint _badPointPrefab;
    [SerializeField] private float _badProbability;
    [SerializeField] private float _badSpawnInterval;
    [SerializeField] private float _badIncSpeed;

    private Vector2 _spawnPos;
    [SerializeField] private float _incSpeed;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _minScale;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBonusPoint();
        InvokeRepeating("SpawnBadPoint", 0, _badSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewBonusPoint()
    {
        if (_currentBonusPoint != null) Destroy(_currentBonusPoint.gameObject);
        GenerateSpawnPos();
        _currentBonusPoint = Instantiate(_bonusPointPrefab, _spawnPos, _bonusPointPrefab.transform.rotation);
        _currentBonusPoint.SetBonusPoint(_incSpeed, GenerateLifeTime(), GenerateScale(), this);
    }

    void SpawnBadPoint()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random > _badProbability) return;
        GenerateSpawnPos();
        var badPoint = Instantiate(_badPointPrefab, _spawnPos, _bonusPointPrefab.transform.rotation);
        badPoint.SetBonusPoint(_badIncSpeed, GenerateLifeTime(), GenerateScale(), this);
    }

    void GenerateSpawnPos()
    {
        _spawnPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    }

    float GenerateLifeTime()
    {
        return Random.Range(_minLifeTime, _maxLifeTime);
    }

    float GenerateScale()
    {
        return Random.Range(_minScale, _maxScale);
    }
}

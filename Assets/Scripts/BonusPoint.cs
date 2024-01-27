using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{
    private SpawnManager _spawnManager;

    [SerializeField] private float _incSpeed;
    public float incSpeed {  get { return _incSpeed; } private set {  _incSpeed = value; } }

    [SerializeField] protected float _lifeTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            _spawnManager.SpawnNewBonusPoint();
        }
    }

    public void SetBonusPoint(float incSpeed, float lifeTime, float scale, SpawnManager spawnManager)
    {
        _incSpeed = incSpeed;
        _lifeTime = lifeTime;
        transform.localScale *= scale;
        _spawnManager = spawnManager;
    }
}

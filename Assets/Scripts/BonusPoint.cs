using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{
    [SerializeField] private float _incSpeed;
    public float incSpeed { get; private set; }

    [SerializeField] private float _lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0 )
        {

        }
    }

    public void SetBonusPoint(float incSpeed, float lifeTime)
    {
        _incSpeed = incSpeed;
        _lifeTime = lifeTime;
    }
}

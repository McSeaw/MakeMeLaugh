using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPoint : BonusPoint
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(gameObject);
    }
}

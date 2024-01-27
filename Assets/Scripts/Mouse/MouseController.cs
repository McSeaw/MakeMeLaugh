using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _boundX;

    private bool _hasFood;
    private Food _food;

    private float _spawnX;

    private bool _direction;

    // Start is called before the first frame update
    void Start()
    {
        _hasFood = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBound();
    }

    void Move()
    {
        if (!_hasFood)
        {
            transform.Translate(_speed * Time.deltaTime * Vector2.right);
        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Food food))
        {
            _food = food;
        }
    }

    void CheckBound()
    {
        if (transform.position.x < -_boundX || transform.position.x > _boundX) Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float _searchSpeed;
    [SerializeField] private float _runSpeed;

    [SerializeField] private float _boundX;

    private Food _food;
    [SerializeField] private float _foodOffset;

    private float _spawnX;

    private bool _isToRight;

    // Start is called before the first frame update
    void Start()
    {
        float temp = Random.Range(0.0f, 1.0f);
        _isToRight = temp > 0.5f;
        _spawnX = (_isToRight ? -1 : 1) * 10.0f;
        transform.position = new Vector2(_spawnX, -4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBound();
    }

    void Move()
    {
        transform.localScale = new Vector2(_isToRight ? 1 : -1, 1);
        if (_food == null)
        {
            transform.Translate(_searchSpeed * Time.deltaTime * new Vector2(_isToRight ? 1 : -1, 0));
        }
        else
        {
            transform.Translate(_runSpeed * Time.deltaTime * new Vector2(_isToRight ? 1 : -1, 0));
            _food.BeStolen(new Vector2(transform.position.x, _foodOffset));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_food == null && collision.gameObject.TryGetComponent(out Food food))
        {
            _food = food;
            _isToRight = !_isToRight;
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

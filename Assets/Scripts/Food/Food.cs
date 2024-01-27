using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private HungryManager hungryManager;
    private Rigidbody2D foodRb;

    [SerializeField] private Vector2 playerPos;
    [SerializeField] private float eatDistance;

    private bool _isDragging;

    private Vector2 _offset;

    [SerializeField] private float _minSpawnHeight;
    [SerializeField] private float _maxSpawnHeight;

    [SerializeField] private float _minFoodSpeed;
    [SerializeField] private float _maxFoodSpeed;

    private float _gravityModifier = 0.5f;

    [SerializeField] private int _foodType;

    [SerializeField] private float _boundX;
    [SerializeField] private float _boundY;

    // Start is called before the first frame update
    void Start()
    {
        foodRb = GetComponent<Rigidbody2D>();
        float temp = Random.Range(0.0f, 1.0f);
        bool flag = temp > 0.5 ? true : false;
        transform.position = GenerateSpawnPos(flag);
        foodRb.velocity = new Vector2(GenerateVelocity(flag), 0);
        Physics.gravity *= _gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDragging)
        {
            foodRb.isKinematic = false;
            return;
        }
        foodRb.isKinematic = true;
        var mousePos = GetMousePos();
        transform.position = mousePos - _offset;
        CheckBound();
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, playerPos) < eatDistance)
        {

            if (_foodType == hungryManager._expectedFood) 
            {
                Destroy(gameObject);
                hungryManager.RelieveHungry();
            }
            else
            {
                LevelManager.Instance.DontLikeFood();
            }
        }
        _isDragging = false;
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GenerateSpawnPos(bool flag)
    {
        float x = flag ? -10 : 10;
        float y = Random.Range(_minSpawnHeight, _maxSpawnHeight);
        return new Vector2(x, y);
    }

    float GenerateVelocity(bool flag)
    {
        float v = Random.Range(_minFoodSpeed, _maxFoodSpeed);
        return flag ? v : -v;
    }

    public void SetManager(HungryManager manager)
    {
        hungryManager = manager;
    }

    void CheckBound()
    {
        if (transform.position.x < -_boundX || transform.position.x > _boundX || transform.position.y < -_boundY)
            Destroy(gameObject);
    }

    public void BeStolen(float x)
    {
        transform.position = new Vector2(x, 0);
    }
}

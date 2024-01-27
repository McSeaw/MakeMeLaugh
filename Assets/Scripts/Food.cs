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

    // Start is called before the first frame update
    void Start()
    {
        foodRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDragging)
        {
            return;
        }
        var mousePos = GetMousePos();
        transform.position = mousePos - _offset;
    }

    private void OnMouseDown()
    {
        if (Vector2.Distance(transform.position, playerPos) < eatDistance)
        {
            Destroy(gameObject);
            hungryManager.RelieveHungry();
        }
        _isDragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetParameters(float speed, HungryManager manager)
    {
        foodRb.velocity = new Vector2(speed, 0);
        hungryManager = manager;
    }
}

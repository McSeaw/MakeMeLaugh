using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 _offset;

    private Vector2 _defaultPos;

    private Image _stickImg;

    // Start is called before the first frame update
    void Start()
    {
        _stickImg = GetComponent<Image>();
        _defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = eventData.position - (Vector2)_stickImg.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _stickImg.transform.position = eventData.position - _offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _stickImg.transform.position = _defaultPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private Texture2D _pointerNormal;
    [SerializeField] private Texture2D _pointerDrag;


    // Start is called before the first frame update
    void Start()
    {
        OnMouseDragOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnMouseDragOn();
        else if (Input.GetMouseButtonUp(0)) OnMouseDragOff();
    }

    void OnMouseDragOn()
    {
        Cursor.SetCursor(_pointerDrag, Vector2.zero, CursorMode.Auto);
        //gameObject.GetComponent<SpriteRenderer>().sprite = _pointerNormal;
    }
    void OnMouseDragOff()
    {
        Cursor.SetCursor(_pointerNormal, Vector2.zero, CursorMode.Auto);
        //gameObject.GetComponent<SpriteRenderer>().sprite = _pointerDrag;
    }
}

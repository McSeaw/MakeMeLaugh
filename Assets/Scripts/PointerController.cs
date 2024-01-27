using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private Texture2D _pointerNormal;
    [SerializeField] private Texture2D _pointerDrag_1;
    [SerializeField] private Texture2D _pointerDrag_2;



    // Start is called before the first frame update
    void Start()
    {
        OnMouseDragOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) OnMouseDragOn();
        else if (Input.GetMouseButtonUp(0)) OnMouseDragOff();
    }

    private float currenttime = 0f;
    private float changetime = 0.1f;
    private Texture2D currentTexture;
    void OnMouseDragOn()
    {
        Debug.Log(11111);
        currenttime += Time.deltaTime;
        if (currentTexture == null) Cursor.SetCursor(_pointerDrag_1, Vector2.zero, CursorMode.Auto);
        if (currenttime > changetime) 
        {
            if (currentTexture == _pointerDrag_1)
            {
                Cursor.SetCursor(_pointerDrag_2, Vector2.zero, CursorMode.Auto);
                currentTexture = _pointerDrag_2;
            }
            else
            {
                Cursor.SetCursor(_pointerDrag_1, Vector2.zero, CursorMode.Auto);
                currentTexture = _pointerDrag_1;
            }
            currenttime = 0f;
        }
      
    }


    void OnMouseDragOff()
    {
        Cursor.SetCursor(_pointerNormal, Vector2.zero, CursorMode.Auto);
    }
}

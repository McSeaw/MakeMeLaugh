using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [SerializeField] private PointerController _pointerPrefab;
    private PointerController _myPointer;

    void Start()
    {
        _myPointer = GeneratePointer();
    }

    // Update is called once per frame
    void Update()
    {
        PointerUpdate();
    }


    PointerController GeneratePointer()
    {
        PointerController pointer = Instantiate(_pointerPrefab, Vector3.zero, Quaternion.identity);
        return pointer;
    }

    void PointerUpdate()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        _myPointer.gameObject.transform.position = pos;
    }

}

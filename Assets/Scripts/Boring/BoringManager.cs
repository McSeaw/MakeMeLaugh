using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoringManager : MonoBehaviour
{
    [SerializeField] private float _boringIntervel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Boring()
    {
        while (true)
        {
            yield return new WaitForSeconds(_boringIntervel);
        }
    }
}

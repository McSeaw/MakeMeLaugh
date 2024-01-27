using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawn : MonoBehaviour
{
    [SerializeField] private MouseController _mousePrefab;

    [SerializeField] private float spawnIntervel;

    private bool _isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        _isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnMouse()
    {
        Instantiate(_mousePrefab);
        yield return new WaitForSeconds(spawnIntervel);
    }

    public void OnHungry()
    {
        if (!_isSpawning) StartCoroutine(SpawnMouse());
        _isSpawning = true;
    }
    public void ExitHungry()
    {
        _isSpawning = false;
        StopCoroutine(SpawnMouse());
    }
}

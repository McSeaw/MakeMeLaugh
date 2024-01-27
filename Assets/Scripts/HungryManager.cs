using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryManager : MonoBehaviour
{
    [SerializeField] private Food _foodPrefab;

    [SerializeField] private float _minHungryInterval;
    [SerializeField] private float _maxHungryInterval;

    [SerializeField] private float _minSpawnFoodInterval;
    [SerializeField] private float _maxSpawnFoodInterval;

    [SerializeField] private float _minSpawnHeight;
    [SerializeField] private float _maxSpawnHeight;

    [SerializeField] private float _minFoodSpeed;
    [SerializeField] private float _maxFoodSpeed;

    [SerializeField] private Image _hungryBubble;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Hungry());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Hungry()
    {
        while (true)
        {
            var hungryIntervel = Random.Range(_minHungryInterval, _maxHungryInterval);
            yield return new WaitForSeconds(hungryIntervel);
            _hungryBubble.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            LevelManager.Instance.State = LevelManager.PlayerState.Hungry;
            yield return SpawnFoods();
        }
    }

    public void RelieveHungry()
    {
        _hungryBubble.gameObject.SetActive(false);
        LevelManager.Instance.State = LevelManager.PlayerState.Normal;
        StopCoroutine(SpawnFoods());
    }

    IEnumerator SpawnFoods()
    {
        var spawnInterval = Random.Range(_minSpawnFoodInterval, _maxSpawnFoodInterval);
        yield return new WaitForSeconds(spawnInterval);
        bool flag = Random.Range(0, 1) > 0.5 ? true : false;
        var food = Instantiate(_foodPrefab, GenerateSpawnPos(flag), _foodPrefab.transform.rotation);
        food.SetParameters(GenerateVelocity(flag), this);
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
        return flag? v : -v;
    }
}

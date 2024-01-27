using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryManager : MonoBehaviour
{
    [SerializeField] private List<Food> _foodPrefabs;
    [SerializeField] private List<Sprite> _foodSprites;

    [SerializeField] private float _minHungryInterval;
    [SerializeField] private float _maxHungryInterval;

    [SerializeField] private float _minSpawnFoodInterval;
    [SerializeField] private float _maxSpawnFoodInterval;

    [SerializeField] private Image _hungryBubble;
    [SerializeField] private Image _expectedFoodImg;

    private bool _isSpawning;

    public int _expectedFood;

    // Start is called before the first frame update
    void Start()
    {
        _isSpawning = false;
        StartCoroutine(Hungry());
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.State == LevelManager.PlayerState.Hungry && !_isSpawning)
        {
            _isSpawning = true;
        }
    }

    IEnumerator Hungry()
    {
        while (true)
        {
            if (LevelManager.Instance.State == LevelManager.PlayerState.Normal)
            {
                var hungryIntervel = Random.Range(_minHungryInterval, _maxHungryInterval);
                yield return new WaitForSeconds(hungryIntervel);

                _expectedFood = Random.Range(0, _foodPrefabs.Count);
                _expectedFoodImg.sprite = _foodSprites[_expectedFood];
                _hungryBubble.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
            
                LevelManager.Instance.State = LevelManager.PlayerState.Hungry;
                CharacterController.Instance.OnEvent();
            }
        }
    }

    public void RelieveHungry()
    {
        _hungryBubble.gameObject.SetActive(false);
        LevelManager.Instance.State = LevelManager.PlayerState.Normal;
        _isSpawning = false;
        StopCoroutine(SpawnFoods());
        CharacterController.Instance.OnEventEnd();
    }

    IEnumerator SpawnFoods()
    {
        while (true)
        {
            var spawnInterval = Random.Range(_minSpawnFoodInterval, _maxSpawnFoodInterval);
            yield return new WaitForSeconds(spawnInterval);
            var spawnType = Random.Range(0, _foodPrefabs.Count);
            var food = Instantiate(_foodPrefabs[spawnType]);
            food.SetManager(this);
        }
    }
}

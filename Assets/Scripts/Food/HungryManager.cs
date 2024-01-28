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

    public int _expectedFood;

    private Coroutine _hungryCoroutine;
    private Coroutine _spawnCoroutine;

    private MouseSpawn _mouseSpawn;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _bubbleAudio;
    [SerializeField] private AudioClip _hungryAudio;

    // Start is called before the first frame update
    void Start()
    {
        _hungryCoroutine = StartCoroutine(Hungry());
        _mouseSpawn = GetComponent<MouseSpawn>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Hungry()
    {
        var hungryIntervel = Random.Range(_minHungryInterval, _maxHungryInterval);
        yield return new WaitForSeconds(hungryIntervel);

        _expectedFood = Random.Range(0, _foodPrefabs.Count);
        _expectedFoodImg.sprite = _foodSprites[_expectedFood];
        _hungryBubble.gameObject.SetActive(true);
        _audioSource.PlayOneShot(_bubbleAudio);
        yield return new WaitForSeconds(1);
        if (LevelManager.Instance.State == LevelManager.PlayerState.Normal)
        {
            LevelManager.Instance.State = LevelManager.PlayerState.Hungry;
            _audioSource.PlayOneShot(_hungryAudio);
            _spawnCoroutine = StartCoroutine(SpawnFoods());
            if (_mouseSpawn != null) { _mouseSpawn.OnHungry(); }
            CharacterController.Instance.OnEvent();
        }
    }

    public void RelieveHungry()
    {
        _hungryBubble.gameObject.SetActive(false);
        LevelManager.Instance.State = LevelManager.PlayerState.Normal;
        StopCoroutine(_spawnCoroutine);
        if (_mouseSpawn != null) { _mouseSpawn.ExitHungry(); }
        _hungryCoroutine = StartCoroutine(Hungry());
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

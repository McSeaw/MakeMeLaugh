using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{
    private SpawnManager _spawnManager;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _badPointSprite;

    [SerializeField] private float _incSpeed;
    public float incSpeed {  get { return _incSpeed; } private set {  _incSpeed = value; } }

    [SerializeField] private float _lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0 )
        {
            _spawnManager.SpawnNewBonusPoint();
        }
    }

    public void SetBonusPoint(float incSpeed, float lifeTime, SpawnManager spawnManager)
    {
        _incSpeed = incSpeed;
        _lifeTime = lifeTime;
        _spawnManager = spawnManager;
    }

    public void SetBadPointSprite()
    {
        _spriteRenderer.sprite = _badPointSprite;
    }
}

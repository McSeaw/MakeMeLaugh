using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Camera _camera;

    [SerializeField] private Slider _timeSlider;

    [SerializeField] public float _timeLimit;
    public float _currentTime;

    private float _IncSpeed;

    [SerializeField] private float _defaultDecSpeed;
    private float _currentDecSpeed;
    [SerializeField] private float _decSpeedChange;
    [SerializeField] private float _maxDecSpeed;

    [SerializeField] private float _hungryDecSpeed;

    private Vector2 _previousMousePos;

    [SerializeField] private float _dontLikeFoodDec;

    private float _delayTouchTime = 0.5f;
    private bool _touched = false;
    private bool _isDelayTouching = false;

    public PlayerState State;

    public enum PlayerState
    {
        Normal,
        Hungry,
        Boring,
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        State = PlayerState.Normal;
        _currentTime = _timeLimit / 2;
        _currentDecSpeed = _defaultDecSpeed;
        UpdateTimeSlider();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange() && State == PlayerState.Normal && _IncSpeed > 0)
        {
            _currentTime += _IncSpeed * Time.deltaTime;
            _isDelayTouching = false;
            StopCoroutine(DelayedOnTouchEnd());
            _delayTouchTime = 0.5f;
            CharacterController.Instance.OnTouch();
            _touched = true;
        }
        else
        {
            if (_touched && !_isDelayTouching)
            {
                _isDelayTouching = true;
                StartCoroutine(DelayedOnTouchEnd());
            }
            if (IsInRange())
            {
                CharacterController.Instance.OnReject();
                if (State == PlayerState.Normal) { _currentTime += _IncSpeed * Time.deltaTime; }
                else { _currentTime -= _hungryDecSpeed * Time.deltaTime; }
            }
            else
            {
                _currentTime -= _currentDecSpeed * Time.deltaTime;
                if (State != PlayerState.Normal) _currentTime -= _currentDecSpeed * Time.deltaTime;
            }
        }
        UpdateTimeSlider();
        UpdateDecSpeed();
        CheckTime();
    }

    bool IsInRange()
    {
        if (!Input.GetMouseButton(0) || _previousMousePos == (Vector2)Input.mousePosition) return false;

        _previousMousePos = Input.mousePosition;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        CharacterController.Instance.TouchCheck(hit);
        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent(out BonusPoint bonusPoint))
            {
                _IncSpeed = bonusPoint.incSpeed;
                if (bonusPoint.gameObject.TryGetComponent(out BadPoint badPoint)) _IncSpeed = badPoint.incSpeed;
                return true;
            }
        }

        return false;
    }

    void UpdateTimeSlider()
    {
        _timeSlider.value = _currentTime / _timeLimit;
    }

    void UpdateDecSpeed()
    {
        _currentDecSpeed += _decSpeedChange * Time.time;
        if (_currentDecSpeed > _maxDecSpeed) _currentDecSpeed = _maxDecSpeed;
    }

    void CheckTime()
    {
        if (_currentTime < 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
        if (_currentTime > _timeLimit)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Win);
        }
    }

    public void DontLikeFood()
    {
        _currentTime = _currentTime - _dontLikeFoodDec > 0 ? _currentTime - _dontLikeFoodDec : 0;
    }

    IEnumerator DelayedOnTouchEnd()
    {
        while (_delayTouchTime > 0)
        {
            yield return new WaitForEndOfFrame();
            _delayTouchTime -= Time.deltaTime;
        }
        Debug.Log("TouchEnd");
        CharacterController.Instance.OnTouchEnd();
        _touched = false;
        _isDelayTouching = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Slider _timeSlider;

    [SerializeField] private float _timeLimit;
    [SerializeField] private float _currentTime;

    [SerializeField] private float _IncSpeed;

    [SerializeField] private float _defaultDecSpeed;
    [SerializeField] private float _currentDecSpeed;
    [SerializeField] private float _decSpeedChange;
    [SerializeField] private float _maxDecSpeed;

    [SerializeField] private Vector2 _previousMousePos;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _timeLimit / 2;
        _currentDecSpeed = _defaultDecSpeed;
        UpdateTimeSlider();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange())
        {
            _currentTime += _IncSpeed * Time.deltaTime;
        }
        else
        {
            _currentTime -= _currentDecSpeed * Time.deltaTime;
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
        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent(out BonusPoint bonusPoint))
            {
                _IncSpeed = bonusPoint.incSpeed;
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
}

using System;
using Prime31;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private TKLSwipeDetector _swipeDetector;

    public float _swipeThreshold = 0.1f;
    public float _lastTimeSwiped;
    public enum SwipeDirection
    {
        Left,
        Right,
        Up,
        Down,
        Default
    }

    public event Action<SwipeDirection> OnSwipe;


    private void Start()
    {
        _lastTimeSwiped = Time.time;
        _swipeDetector ??= GetComponent<TKLSwipeDetector>();
        _swipeDetector.onSwipeDetected += (direction) => OnSwipe?.Invoke(GetSwipeDirection(direction));
    }

    private void Update()
    {
        if ((_lastTimeSwiped >= Time.time - _swipeThreshold)) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            _lastTimeSwiped = Time.time;
            OnSwipe?.Invoke(SwipeDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _lastTimeSwiped = Time.time;
            OnSwipe?.Invoke(SwipeDirection.Up);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _lastTimeSwiped = Time.time;
            OnSwipe?.Invoke(SwipeDirection.Down);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _lastTimeSwiped = Time.time;
            OnSwipe?.Invoke(SwipeDirection.Right);
        }
        
    }

    private SwipeDirection GetSwipeDirection(Prime31.SwipeDirection externalSwipeDirection)
    {
        return externalSwipeDirection switch
        {
            Prime31.SwipeDirection.Left => SwipeDirection.Left,
            Prime31.SwipeDirection.Right => SwipeDirection.Right,
            Prime31.SwipeDirection.Up => SwipeDirection.Up,
            Prime31.SwipeDirection.Down => SwipeDirection.Down,
            _ => SwipeDirection.Default
        };
    }
}
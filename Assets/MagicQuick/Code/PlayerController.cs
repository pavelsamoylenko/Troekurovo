using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private InputService _inputService;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _dodgeSpeed = 0.5f;
    public int score;
    public TextMeshProUGUI scoreText;

    private Side _side;

    private Transform _leftTrack;
    private Transform _midTrack;
    private Transform _rightTrack;

    public bool dead;
    private float _moveScaleFactor = 2f;
    [SerializeField] private float _newXPos = 0f;
    private float _xValue = 2f;
    [SerializeField] private float _x = 0f;

    public enum Side
    {
        Left = -1,
        Mid = 0,
        Right = 1
    }
    // Start is called before the first frame update
    void Start()
    {
        _inputService.OnSwipe += OnSwipe;
    }


    // Update is called once per frame
    private void OnSwipe(InputService.SwipeDirection direction)
    {
        switch (direction)
        {
            case InputService.SwipeDirection.Left:
                print("left");
                _animator.Play("dodgeLeft");
                _newXPos -= _xValue;
                _side--;
                break;
            case InputService.SwipeDirection.Right:
                print("right");
                _animator.Play("dodgeRight");
                _newXPos += _xValue;
                _side++;
                break;
            case InputService.SwipeDirection.Up:
                _characterController.Move(Vector3.up * _moveScaleFactor);
                break;
            case InputService.SwipeDirection.Down:
                _characterController.Move(Vector3.down * _moveScaleFactor);
                break;
            default:
                Debug.Log("Unknown Swipe Direction", this);
                break;
        }
        _side = (Side) Mathf.Clamp((int)_side, (int)Side.Left, (int)Side.Right);
        _newXPos = Mathf.Clamp(_newXPos, -_xValue, _xValue);
        
        transform.rotation = Quaternion.identity;
        

    }

    private void Update()
    {
        _x = Mathf.Lerp(_x, _newXPos,_dodgeSpeed);
        _characterController.Move((_x - transform.position.x) * Vector3.right);
        if (_characterController.isGrounded)
        {
            
        }
        else
        {
            
        }
        
    }
}
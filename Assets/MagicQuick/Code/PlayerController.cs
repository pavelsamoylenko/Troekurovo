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
    [SerializeField] private CharacterController _char;
    [SerializeField] private float _dodgeSpeed = 0.5f;
    public int score;
    public TextMeshProUGUI scoreText;

    private Side _side;

    private Transform _leftTrack;
    private Transform _midTrack;
    private Transform _rightTrack;

    public bool dead;
    [SerializeField] private float _newXPos = 0f;
    private float _xValue = 2f;
    [SerializeField] private float _charVelocity;
    [SerializeField] private float _x = 0f;
    [SerializeField] private float _y = 0f;
    private bool _inJump;
    private bool _inRoll;
    [SerializeField] private float _jumpPower = 3f;
    private float _rollDuration;

    private float _colliderHeight;
    private float _colliderCenterY;

    public enum Side
    {
        Left = -1,
        Mid = 0,
        Right = 1
    }

    public event Action OnRestart;

    // Start is called before the first frame update
    void Start()
    {
        _colliderHeight = _char.height;
        _colliderCenterY = _char.center.y;
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
                Jump();
                break;
            case InputService.SwipeDirection.Down:
                Roll();
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
        _charVelocity = _char.velocity.y;
        _x = Mathf.Lerp(_x, _newXPos,_dodgeSpeed);
        Vector3 moveVector = new Vector3(_x - transform.localPosition.x, _y - transform.localPosition.y, 0);
        ProcessState();
        _char.Move(moveVector);
    }

    public void ProcessState()
    {
        _rollDuration -= Time.deltaTime;
        if (_rollDuration <= 0f)
        {
            _inRoll = false;
            _rollDuration = 0f;
            _char.center = new Vector3(0, _colliderCenterY, 0);
            _char.height = _colliderHeight;    
        }
        if (transform.localPosition.y <= 0.25f)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                _animator.Play("Landing");
                _inJump = false;
            }
        }
        else
        {
            _y -= 2 * _jumpPower * Time.deltaTime;
            if(_char.velocity.y < -0.1f)
                _animator.Play("Falling");
        }
    }

    private void Roll()
    {
        _rollDuration = 0.5f;
        if (!_inRoll)
        {
            _y = 0f;
            _char.height = _colliderHeight / 2f;
            _char.center = new Vector3(0, _colliderCenterY / 2f, 0);
        }
        _animator.CrossFadeInFixedTime("Roll", 0.1f);
        _inJump = false;
        _inRoll = true;
    }

    private void Jump()
    {
        Debug.Log(transform.localPosition.y <=0.25f ? "Grounded" : "Not Grounded");
        if (transform.localPosition.y <= 0.25f)
        {
            _y = _jumpPower;
            _animator.CrossFadeInFixedTime("Jump", 0.1f);
            _inJump = true;
        }
    }

    public void Restart()
    {
        transform.localPosition = Vector3.zero;
        OnRestart?.Invoke();
    }
    
}
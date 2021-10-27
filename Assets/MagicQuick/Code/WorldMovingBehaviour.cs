using UnityEngine;
using System.Collections;

public class WorldMovingBehaviour : MonoBehaviour
{
    public PlayerController Player;
    public float moveSpeed;
    public float addSpeed;
    public float speedTimer;
    public float scoreTimer;

    private Vector3 _initialPos;

    // Use this for initialization
    void Start ()
    {
        _initialPos = transform.localPosition;
        Player.OnRestart += OnRestart;
    }
	
    // Update is called once per frame
    void Update () {
        if (!Player.dead) {
            speedTimer += Time.deltaTime;
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 2) {
                scoreTimer = 0;
                Player.score += 10;
            }
            if (speedTimer >= 10) {
                speedTimer = 0;
                moveSpeed += addSpeed;
                Debug.Log ("increased");
            }
            transform.Translate (0, 0, -moveSpeed * Time.deltaTime);
        }
    }

    public void OnRestart()
    {
        transform.localPosition = _initialPos;
        moveSpeed = 10f;
    }
}
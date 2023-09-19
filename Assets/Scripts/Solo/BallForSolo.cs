using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallForSolo : MonoBehaviour
{
    [SerializeField] private float _speed;
    private SpriteRenderer _spriteRenderer;
    private float _currentSpeed;
    public static event Action<string> goalTo;
    public static event Action TouchedRed;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        _currentSpeed = _speed;
        transform.position = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.left * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "LineOfGoalBlue":
                goalTo?.Invoke("Red");
                break;
            case "LineOfGoalRed":
                goalTo?.Invoke("Blue");
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentSpeed++;
        if (collision.gameObject.name == "BlueRacket")
        {
            ReboundBall(collision.gameObject, 1);
        }
        if (collision.gameObject.name.Contains("RedRacket"))
        {
            TouchedRed?.Invoke();
        }
    }
    private void ReboundBall(GameObject racket, int dir)
    {
        float y = HitFactor(transform.position, racket.transform.position, racket.gameObject.GetComponent<BoxCollider2D>().bounds.size.y);
        Vector2 direction = new Vector2(dir, y).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * _currentSpeed;
    }
    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHight)
    {
        return (ballPosition.y - racketPosition.y) / racketHight;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    private SpriteRenderer _spriteRenderer;
    private float _currentSpeed;
    public static event Action<string> GoalTo;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    private void OnEnable()
    {
        _currentSpeed = _speed;
        transform.position = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                GetComponent<Rigidbody2D>().velocity = Vector2.right * _speed;
                break;
            case 1:
                GetComponent<Rigidbody2D>().velocity = Vector2.left * _speed;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "LineOfGoalBlue":
                GoalTo?.Invoke("Red");
                break;
            case "LineOfGoalRed":
                GoalTo?.Invoke("Blue");
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentSpeed+=2;
        if(collision.gameObject.name == "BlueRacket")
        {
            _spriteRenderer.DOColor(Color.blue,0);
            ReboundBall(collision.gameObject, 1);
        }
        if(collision.gameObject.name == "RedRacket")
        {

            _spriteRenderer.DOColor(Color.red, 0);
            ReboundBall(collision.gameObject, -1);
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

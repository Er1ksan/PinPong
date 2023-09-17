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

    public static event Action<string> goalTo;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    private void OnEnable()
    {
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
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }
    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHight)
    {
        return (ballPosition.y - racketPosition.y) / racketHight;
    }
}

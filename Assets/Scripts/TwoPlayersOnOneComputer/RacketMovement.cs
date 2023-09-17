using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RacketMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private string _axis;

    void FixedUpdate()
    {
        float inputLeftRacket = Input.GetAxisRaw(_axis);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, inputLeftRacket) * _speed;
    }
}

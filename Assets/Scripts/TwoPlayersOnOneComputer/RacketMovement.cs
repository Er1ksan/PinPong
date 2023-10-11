using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class RacketMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private string _axisBlue;
    [SerializeField] private string _axisRed;
    [SerializeField] private Transform _blue;
    [SerializeField] private Transform _red;

    [SerializeField] private bool _isSolo;
    void FixedUpdate()
    {
        Vector2 dir = new Vector2(0, 0);
        if (YandexGame.EnvironmentData.isMobile || Application.isMobilePlatform)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                Debug.Log(touchPosition);
                if (touchPosition.x > 0)
                {
                    MoveRacket(_blue, touchPosition.y);
                }
                else if(touchPosition.x < 0 && !_isSolo)
                {
                    MoveRacket(_red, touchPosition.y);
                }
            }
        }
        else
        {
            if(!_isSolo) {
                _blue.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxisRaw(_axisBlue)) * _speed;
                _red.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxisRaw(_axisRed)) * _speed;
            }
            else
            {
                _blue.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxisRaw(_axisBlue)) * _speed;
            }
            
        }

    }
    private void MoveRacket(Transform racket, float y)
    {
        Vector2 input = new Vector2(racket.position.x, y);
        input.y = Mathf.Clamp(input.y, -4.2f, 4.2f);
        racket.position = Vector2.MoveTowards(racket.position, input, 1);
    }
}

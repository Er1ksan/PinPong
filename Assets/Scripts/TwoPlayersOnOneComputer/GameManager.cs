using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _blueScoreText;
    [SerializeField] private TMP_Text _redScoreText;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private TMP_Text _notification;
    [Header("Settings")]
    [SerializeField] private float _secondsInRound;
    [SerializeField] private float _timeBeforeStart;
    [SerializeField] private float _timeOfGoalNotification;
    [Header("Prefabs")]
    [SerializeField] private GameObject _ballPrefab;
    [Header("WinnerWindow")]
    [SerializeField] private GameObject _winnerWindow;
    [SerializeField] private TMP_Text _winner;
    private GameObject _ball;
    private float _timeRemaining;
    private bool _isTimerRunning = false;
    private int _blueScore = 0;
    private int _redScore = 0;
    public static event Action TimeIsLeft;
    private void OnEnable()
    {
        Time.timeScale = 1;
        _blueScoreText.text = _blueScore.ToString();
        _redScoreText.text = _redScore.ToString();
        Ball.GoalTo += OnGoalSwitchScore;
        Ball.GoalTo += OnGoalNotification;
        TimeIsLeft += LookingForWinner;
        _timeRemaining = _secondsInRound;
        StartCoroutine(OnStartRound());
    }
    private void OnDisable()
    {
        Ball.GoalTo -= OnGoalSwitchScore;
        Ball.GoalTo -= OnGoalNotification;
        TimeIsLeft -= LookingForWinner;
    }
    private void Start()
    {
        
    }
    void Update()
    {
        Timer();
        DisplayTime();
    }
    private void LookingForWinner()
    {
        _winnerWindow.SetActive(true);
        _ball.SetActive(false);
        if (_blueScore < _redScore)
        {
            _winner.color = Color.red;
            _winner.text = "Красный, красава!";
        }
        else if (_blueScore > _redScore)
        {
            _winner.color = Color.blue;
            _winner.text = "Синий, красава!";
        }
        else
        {
            _winner.color = Color.green;
            _winner.text = "Ничья!";
        }
    }
    public  void LoadLobbyScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    private IEnumerator OnStartRound()
    {
        _isTimerRunning = false;
        float time = _timeBeforeStart;
        
        for (; ; )
        {
            _notification.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
            if (time <= 0)
            {
                _notification.gameObject.SetActive(false);
                _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
                _isTimerRunning = true;
                break;
            }
        }
        
    }
    private void OnGoalNotification(string colorOfGoal)
    {
        _ball.SetActive(false);
        _isTimerRunning = false;
        Color color = colorOfGoal == "Blue" ? Color.blue:Color.red;
        _notification.gameObject.SetActive(true);
        _notification.color = color;
        _notification.text = "GOOOOAAAAL";
        StartCoroutine(WaitNotificationEnd());
    }
    private IEnumerator WaitNotificationEnd()
    {
        float time = _timeOfGoalNotification;
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            time--;
            if (time <= 0)
            {
                _ball.SetActive(true);
                _isTimerRunning = true;
                _notification.gameObject.SetActive(false);
                break;
            }
        }
    }
    private void OnGoalSwitchScore(string goalToColor)
    {
        switch (goalToColor)
        {
            case "Blue":
                _blueScore++;
                _blueScoreText.text = _blueScore.ToString();
                break;
            case "Red":
                _redScore++;
                _redScoreText.text = _redScore.ToString();
                break;
            default:
                break;
        }
    }
    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(_timeRemaining / 60);
        float seconds = Mathf.FloorToInt(_timeRemaining % 60);
        _time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void Timer()
    {
        if(_isTimerRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                _timeRemaining = 0;
                TimeIsLeft?.Invoke();
                _isTimerRunning = false;
                Time.timeScale = 0;
            }
        }
    }
}

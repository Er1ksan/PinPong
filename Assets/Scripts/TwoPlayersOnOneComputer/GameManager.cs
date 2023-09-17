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
    //private bool _isTimerRunning = false;
    private int _blueScore = 0;
    private int _redScore = 0;
    public static event Action TimeIsLeft;
    private void Start()
    {
        _blueScoreText.text = _blueScore.ToString();
        _redScoreText.text = _redScore.ToString();
        Ball.goalTo += OnGoalSwitchScore;
        Ball.goalTo += OnGoalNotification;
        TimeIsLeft += LookingForWinner;
        _timeRemaining = _secondsInRound;
        StartCoroutine(OnStartRound());
    }
    private void OnDestroy()
    {
        Ball.goalTo -= OnGoalSwitchScore;
        Ball.goalTo -= OnGoalNotification;
        TimeIsLeft -= LookingForWinner;
    }
    void Update()
    {
        Timer();
        DisplayTime();
    }
    private void LookingForWinner()
    {
        if (_blueScore > _redScore)
        {
            _winner.color = Color.red;
            _winner.text = "�������, �������!";
        }
        else if (_blueScore < _redScore)
        {
            _winner.color = Color.blue;
            _winner.text = "�����, �������!";
        }
        else
        {
            _winner.color = Color.green;
            _winner.text = "�����!";
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
        Time.timeScale = 0;
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
                Time.timeScale = 1;
                break;
            }
        }
        
    }
    private void OnGoalNotification(string colorOfGoal)
    {
        _ball.SetActive(false);
        Time.timeScale = 0;
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
                Time.timeScale = 1;
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

            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                _timeRemaining = 0;
                TimeIsLeft?.Invoke();
                Time.timeScale = 0;
            }
        
    }
}

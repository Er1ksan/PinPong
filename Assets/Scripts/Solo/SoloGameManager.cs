using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class SoloGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _countOfPunchesText;
    [SerializeField] private TMP_Text _timerBeforeGameStart;
    [Header("Settings")]
    [SerializeField] private float _timeBeforeStart;

    [Header("Prefabs")]
    [SerializeField] private GameObject _ballPrefab;
    [Header("WinnerLoserWindow")]
    [SerializeField] private GameObject _winnerWindow;
    [SerializeField] private TMP_Text _massageInWinnerWindow;
    private GameObject _ball;
    private int _countOfPunchesLeft;
    private void OnEnable()
    {
        _countOfPunchesLeft = User.Levels[User.SelectedLvl]._countOfPunchesMax;
        _countOfPunchesText.text = _countOfPunchesLeft.ToString();
        Time.timeScale = 1;
        BallForSolo.goalTo += OnGoal;
        BallForSolo.TouchedRed += Minus1InPunchesLeft;
        StartCoroutine(OnStartRound());
    }
    private void OnDisable()
    {
        BallForSolo.goalTo -= OnGoal;
        
    }
    private void Start()
    {

    }
    void Update()
    {
        
    }
    private void Minus1InPunchesLeft()
    {
        _countOfPunchesLeft--;
        if (_countOfPunchesLeft <= 0)
        {
            Lose();
        }
    }
    private IEnumerator OnStartRound()
    {
        float time = _timeBeforeStart;

        for (; ; )
        {
            _timerBeforeGameStart.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
            if (time <= 0)
            {
                _timerBeforeGameStart.gameObject.SetActive(false);
                _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
                break;
            }
        }

    }
    private void OnGoal(string colorOfGoal)
    {
        _ball.SetActive(false);
        if(colorOfGoal == "Blue")
        {
            Win();
        }
        else
        {
            Lose();
        }
        
    }
    private void Win()
    {
        Time.timeScale = 0;
        _winnerWindow.SetActive(true);
        _massageInWinnerWindow.color = Color.green;
        _massageInWinnerWindow.text = "Умница!";
        if (_countOfPunchesLeft > 1)
        {
            User.Levels[User.SelectedLvl]._starsCount = 3;
        }
    }
    private void Lose()
    {
        Time.timeScale = 0;
        _winnerWindow.SetActive(true);
        _massageInWinnerWindow.color = Color.red;
        _massageInWinnerWindow.text = "Попробуйте еще раз";
    }
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(3);
    }
}

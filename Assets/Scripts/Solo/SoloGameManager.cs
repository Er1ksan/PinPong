using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using YG;

public class SoloGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _countOfPunchesText;
    [SerializeField] private TMP_Text _timerBeforeGameStart;

    [SerializeField] private GameObject[] _objectsToHideOnEnd;
    [Header("Settings")]
    [SerializeField] private float _timeBeforeStart;
    [Header("Prefabs")]
    [SerializeField] private GameObject _ballPrefab;
    [Header("WinnerLoserWindow")]
    [SerializeField] private GameObject _winnerWindow;
    [SerializeField] private TMP_Text _massageInWinnerWindow;
    [SerializeField] private TMP_Text _countofPunchesOnEnd;
    private GameObject _ball;
    private int _countOfPunches = 0;

    private void OnEnable()
    {
        _countOfPunchesText.text = _countOfPunches.ToString();
        Time.timeScale = 1;
        BallForSolo.goalTo += OnGoal;
        BallForSolo.TouchedRed += OnTouchedRed;

        StartCoroutine(OnStartRound());
        _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        _ball.SetActive(false);
        YandexGame.RewardVideoEvent += OnContinueGame;
    }
    private void OnDisable()
    {
        BallForSolo.goalTo -= OnGoal;
        BallForSolo.TouchedRed -= OnTouchedRed;
        YandexGame.RewardVideoEvent -= OnContinueGame;
    }
    private void OnTouchedRed()
    {
        _countOfPunches++;
        _countOfPunchesText.text = _countOfPunches.ToString();
    }
    private IEnumerator OnStartRound()
    {
        Time.timeScale = 1;
        _timerBeforeGameStart.gameObject.SetActive(true);
        float time = _timeBeforeStart;

        for (; ; )
        {
            _timerBeforeGameStart.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
            if (time <= 0)
            {
                _timerBeforeGameStart.gameObject.SetActive(false);
                _ball.SetActive(true);
                break;
            }
        }

    }
    private void OnContinueGame(int id)
    {
        _winnerWindow.SetActive(false);
        for (int i = 0; i < _objectsToHideOnEnd.Length; i++)
        {
            _objectsToHideOnEnd[i].SetActive(true);
        }
        StartCoroutine(OnStartRound());
    }
    private void OnGoal(string colorOfGoal)
    {
        if(colorOfGoal == "Red")
        {
            _ball.SetActive(false);
            Lose();
        }
        
    }
    private void Lose()
    {
        Time.timeScale = 0;
        YandexGame.savesData.AddCoins(_countOfPunches);
        for(int i = 0;i<_objectsToHideOnEnd.Length;i++)
        {
            _objectsToHideOnEnd[i].SetActive(false);
        }
        _countofPunchesOnEnd.text = "+" + _countOfPunches.ToString();
        _winnerWindow.SetActive(true);
        if (YandexGame.savesData.record < _countOfPunches)
        {
            _massageInWinnerWindow.text = "Новый рекорд!";
            YandexGame.savesData.SetRecord(_countOfPunches);
        }
        else if (YandexGame.savesData.record - _countOfPunches < 5)
        {
            _massageInWinnerWindow.text = "Почти рекорд, попробуйте еще!";
        }
    }
    public void OnContinueClick()
    {
        YandexGame.RewVideoShow(1);
        //OnContinueGame(1);
    }
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("NEWSOLO");
    }
}

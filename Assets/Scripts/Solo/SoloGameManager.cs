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
    [SerializeField] private GameObject[] _stars;
    [Header("Settings")]
    [SerializeField] private float _timeBeforeStart;
    [SerializeField] private int _countOfPunchesMax;
    [SerializeField] private int _countOfPunchesForThreeStars;
    [SerializeField] private int _countOfPunchesForTwoStars;
    [Header("Prefabs")]
    [SerializeField] private GameObject _ballPrefab;
    [Header("WinnerLoserWindow")]
    [SerializeField] private GameObject _winnerWindow;
    [SerializeField] private TMP_Text _massageInWinnerWindow;
    private GameObject _ball;
    private int _countOfPunchesLeft;

    private void OnEnable()
    {
        _countOfPunchesLeft = _countOfPunchesMax;
        _countOfPunchesText.text = _countOfPunchesLeft.ToString();
        Time.timeScale = 1;
        BallForSolo.goalTo += OnGoal;
        BallForSolo.TouchedRed += OnTouchedRed;
        StartCoroutine(OnStartRound());
    }
    private void OnDisable()
    {
        BallForSolo.goalTo -= OnGoal;
        BallForSolo.TouchedRed -= OnTouchedRed;
    }
    private void Start()
    {

    }
    void Update()
    {
        
    }
    private void OnTouchedRed()
    {
        _countOfPunchesLeft--;
        _countOfPunchesText.text = _countOfPunchesLeft.ToString();
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
        if (_countOfPunchesLeft >= _countOfPunchesForThreeStars)
        {
            for (int i = 0; i < 3; i++)
            {
                _stars[i].SetActive(true);
            }
                User.Levels[User.SelectedLvl].StarsCount = 3;
        }
        else if (_countOfPunchesLeft >= _countOfPunchesForTwoStars&&_countOfPunchesLeft < _countOfPunchesForThreeStars)
        {
            for (int i = 0; i < 2; i++)
            {
                _stars[i].SetActive(true);
            }
            if (User.Levels[User.SelectedLvl].StarsCount < 2)
            {
                User.Levels[User.SelectedLvl].StarsCount = 2;
            }
        }
        else
        {
            _stars[0].SetActive(true);
            if (User.Levels[User.SelectedLvl].StarsCount < 1)
            {
                User.Levels[User.SelectedLvl].StarsCount = 1;
            }
        }
        User.Levels[User.SelectedLvl+1].IsOpen = true;
        User.SaveToFile();
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
        SceneManager.LoadScene(2);
    }
    public void LoadGameSceneAgain()
    {
        SceneManager.LoadScene(3+User.SelectedLvl);
    }
    public void LoadNextLvl()
    {
        User.SelectedLvl++;
        SceneManager.LoadScene(3 + User.SelectedLvl);
    }
}

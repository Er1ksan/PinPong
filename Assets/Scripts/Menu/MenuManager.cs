using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using TMPro;


public class MenuManager : MonoBehaviour
{
    [Header("FirstLaunch")]
    [SerializeField] private GameObject _firstLaunchGameMenu;
    [SerializeField] private TMP_Text _errorMassage;
    [SerializeField] private TMP_InputField _inputedNickname;
    [Header("Settings")]
    [SerializeField] private TMP_Text _nickname;
    [SerializeField] private TMP_Text _coins;
    [Header("PathOfUserFile")]
    [SerializeField] private string _saveFileName = "UserSettings.json";
    private bool _isStart = true;
    private void Awake()
    {
        SetSavePath();
        if (!User.LoadFromFile()&&_isStart)
        {
            FirstLaunchGame();
            _isStart = false;
        }
        Debug.Log(User.Nickname);
        _nickname.text = User.Nickname;
        _coins.text = User.Money.ToString();
    }
    public void FirstLaunchGame()
    {
        _firstLaunchGameMenu.SetActive(true);
        User.Money = 0;
        
    }
    
    public void OnNicknameEnter()
    {
        if (_inputedNickname.text == null)
        {
            OutputError("������� ���������� �������!<3");
        }
        else if (_inputedNickname.text.Length < 5)
        {
            OutputError("������� ��������� �������.���� ������ 5 ��������.");
        }
        else
        {
            User.Nickname = _inputedNickname.text;
            _nickname.text = User.Nickname;
            User.SaveToFile();
            _firstLaunchGameMenu.SetActive(false);
        }
    }
    private void OutputError(string massage)
    {
        _errorMassage.text = massage;
    }
    public void LoadGameScene1x1OnDevice()
    {
        _isStart = false;
        SceneManager.LoadScene(1);
    }
    public void LoadMenuSceneSolo()
    {
        _isStart = false;
        SceneManager.LoadScene(2);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OnMoneyButtonClick()
    {
        Application.OpenURL("https://vk.com/eriksan04");
    }
    private void SetSavePath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        User.savePath = Path.Combine(Application.persistentDataPath,saveFileName);
#else 
        User.savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif
    }
    private void OnApplicationQuit()
    {
        User.SaveToFile();
    }
    private void OnApplicationPause(bool pause)
    {
        if(Application.platform == RuntimePlatform.Android)
        {
           User.SaveToFile();
        }
    }
}

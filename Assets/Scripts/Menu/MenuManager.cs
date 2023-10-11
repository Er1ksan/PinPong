using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using TMPro;
using YG;
using DG.Tweening;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _settingsMenu;
    [Header("UI")]
    [SerializeField] private Image _sounds;
    [SerializeField] private Sprite _soundsOn;
    [SerializeField] private Sprite _soundsOff;
    [SerializeField] private GameObject _gift;
    [SerializeField] private TextMeshProUGUI _coins;
    public static MenuManager Instance;
    private int coins;
    public void Awake()
    {
        _gift.GetComponent<Gift>().Init();
        Instance = this;
        if (YandexGame.SDKEnabled == true)
        {
            LoadData();
        }
    }
    private void Start()
    {
        DOTween.Init();
        Time.timeScale = 1.0f;
        if (YandexGame.savesData.isSoundOn)
        {
            _sounds.sprite = _soundsOn;
            AudioListener.volume = 1.0f;
        }
        else
        {
            _sounds.sprite = _soundsOff;
            AudioListener.volume= 0f;
        }
    }
    public void AddCoinsToUI(int coin)
    {
        coins += coin;
        _coins.text = coins.ToString();
    }
    private void LoadData()
    {
        coins = YandexGame.savesData.coins;
        _coins.text = coins.ToString();

    }
    private void OnEnable()
    {

        YandexGame.GetDataEvent += LoadData;

    }
    public void OnResetDataClick()
    {
        YandexGame.ResetSaveProgress();
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadData;
    }
    public void OnSoundClick()
    {
        if (AudioListener.volume == 1f)
        {
            _sounds.sprite = _soundsOff;
            YandexGame.savesData.SwitchAudio();

        }
        else
        {
            _sounds.sprite = _soundsOn;
            YandexGame.savesData.SwitchAudio();
        }

    }
    public void OnExitFromSettingsClick()
    {
        _settingsMenu.GetComponent<SettingsInMenu>().Disable();
    }
    public void OnSettingsClick()
    {
        _settingsMenu.GetComponent<SettingsInMenu>().Enable();
    }
    public void OnAuthClick()
    {
        YandexGame.Instance._OpenAuthDialog();
    }
    public void LoadGameScene1x1OnDevice()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMenuSceneSolo()
    {
        SceneManager.LoadScene(2);
    }

}

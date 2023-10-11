using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using YG;

public class SoloMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;
    [SerializeField] private GameObject _notificationG;
    [SerializeField] private TMP_Text _notificationT;
    private void OnEnable()
    {

        if (YandexGame.savesData.Levels.Count != _levels.Count)
        {
            LoadLevelsOnFirstLaunch();
        }
        else
        {
            LoadLevels();
        }
    }
    private void LoadLevelsOnFirstLaunch()
    {
        YandexGame.savesData.Levels = new List<Level>(_levels.Count);
        YandexGame.savesData.Levels.Add(new Level(true));
        for(int i = 0; i < _levels.Count - 1; i++)
        {
            YandexGame.savesData.Levels.Add( new Level());
        }
    }
    private void LoadLevels()
    {
        for (int i = 0; i < YandexGame.savesData.Levels.Count; i++)
        {   
            if(YandexGame.savesData.Levels[i].IsOpen)
            {
                _levels[i].transform.Find("SkinItemBGLock").gameObject.SetActive(false);
                if (YandexGame.savesData.Levels[i].StarsCount == 1)
                {
                    _levels[i].gameObject.transform.Find("FirstStar").gameObject.SetActive(true);
                }
                else if (YandexGame.savesData.Levels[i].StarsCount == 2)
                {
                    _levels[i].gameObject.transform.Find("FirstStar").gameObject.SetActive(true);
                    _levels[i].gameObject.transform.Find("SecondStar").gameObject.SetActive(true);
                }
                else if(YandexGame.savesData.Levels[i].StarsCount ==3)
                {
                    _levels[i].gameObject.transform.Find("FirstStar").gameObject.SetActive(true);
                    _levels[i].gameObject.transform.Find("SecondStar").gameObject.SetActive(true);
                    _levels[i].gameObject.transform.Find("ThirdStar").gameObject.SetActive(true);
                }                
            }
        }
    }
    public void OnLevelChoose(int LVLNumber)
    {
        if (YandexGame.savesData.Levels[LVLNumber-1].IsOpen)
        {
            //YandexGame.savesData.selectedLVL = LVLNumber-1;
            SceneManager.LoadScene("LVL" + LVLNumber);
        }
        else
        {

        }
    }
    public void OnToMainMenuClick()
    {
        YandexGame.savesData.Save();
        SceneManager.LoadScene(0);
    }
}

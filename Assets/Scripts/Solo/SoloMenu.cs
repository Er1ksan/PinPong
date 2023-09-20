using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class SoloMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;
    [SerializeField] private GameObject _notificationG;
    [SerializeField] private TMP_Text _notificationT;
    private void OnEnable()
    {
        Debug.Log("User lvl count-> " + User.Levels.Count + " LVl count-> " + _levels.Count);
        if (User.Levels.Count != _levels.Count)
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
        User.Levels = new List<Level>(_levels.Count);
        User.Levels.Add(new Level(true));
        for(int i = 0; i < _levels.Count - 1; i++)
        {
            User.Levels.Add( new Level());
        }
    }
    private void LoadLevels()
    {
        for (int i = 0; i < User.Levels.Count; i++)
        {   
            if(User.Levels[i].IsOpen)
            {
                _levels[i].transform.Find("SkinItemBGLock").gameObject.SetActive(false);
                if (User.Levels[i].StarsCount == 1)
                {
                    _levels[i].gameObject.transform.Find("FirstStar").gameObject.SetActive(true);
                }
                else if (User.Levels[i].StarsCount == 2)
                {
                    _levels[i].gameObject.transform.Find("FirstStar").gameObject.SetActive(true);
                    _levels[i].gameObject.transform.Find("SecondStar").gameObject.SetActive(true);
                }
                else if(User.Levels[i].StarsCount ==3)
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
        if (User.Levels[LVLNumber-1].IsOpen)
        {
            User.SelectedLvl = LVLNumber-1;
            SceneManager.LoadScene(2+LVLNumber);
        }
        else
        {

        }
    }
    public void OnToMainMenuClick()
    {
        User.SaveToFile();
        SceneManager.LoadScene(0);
    }
}

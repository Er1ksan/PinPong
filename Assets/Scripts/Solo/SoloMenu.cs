using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SoloMenu : MonoBehaviour
{
    [SerializeField] private int _countOfLvls;
    [SerializeField] private List<Level> levels;
    private void Awake()
    {
        if (User.Levels == null)
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
        User.Levels = levels;
    }
    private void LoadLevels()
    {
        for (int i = 0; i < User.Levels.Count; i++)
        {
            levels[i] = User.Levels[i];
        }
    }
    public void OnLevelChoose()
    {
        Level choosenLevel = EventSystem.current.currentSelectedGameObject.GetComponent<Level>();
        if (choosenLevel.IsOpen)
        {
            User.SelectedLvl = choosenLevel._numberOfLevel;
            SceneManager.LoadScene(3);
        }
    }
    public void OnToMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }
}

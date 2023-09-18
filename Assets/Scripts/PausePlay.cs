using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePlay : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    public void OnPauseClick()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnPlayClick()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnExitToLobbyClick()
    {
        SceneManager.LoadScene(0);
    }
}

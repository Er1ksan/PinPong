using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int _numberOfLevel;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject _blockImage;
    public bool IsOpen = false;
    private int _starsCount = 0;
    [HideInInspector] public Vector2 positionsOfEnemy;
    void Start()
    {
        if(_numberOfLevel == 1)
        {
            IsOpen = true;
            _blockImage.SetActive(false);
        }
        SetStars();
    }
    private void SetStars()
    {
        for (int i = 0; i < _starsCount; i++)
        {
            _stars[i].SetActive(true);
        }
    }

    void Update()
    {
        
    }
}

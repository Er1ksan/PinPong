using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public bool IsOpen;
    public int StarsCount;

    public Level()
    {
        IsOpen = false;
        StarsCount = 0;
    }
    public Level(bool isOpen)
    {
        IsOpen = isOpen;
        StarsCount = 0;
    }
}

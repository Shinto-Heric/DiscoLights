using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayHandler : MonoBehaviour
{
    private int _rowCount;
    private int _columnCount;
    private bool clickStatus = false;
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
    }
    public void SetGridCount(int row, int column)
    {
        _rowCount = row;
        _columnCount = column;
    }

    public int GetRowCount()
    {
        return _rowCount;
    }

    public int GetColumnCount()
    {
        return _columnCount;
    }
    public void SetClickStatus(bool value)
    {
        clickStatus = value;
    }
    public bool GetClickStatus()
    {
        return clickStatus;
    }

}

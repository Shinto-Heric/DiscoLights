using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    int rowIndex, columnIndex;
    bool died = false;
    bool gridHighlighted = false;
    SpriteRenderer sprite;
    GameAreaHandler gameAreaHandler;
    GameplayHandler gameplayHandler;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameAreaHandler = FindObjectOfType<GameAreaHandler>();
        gameplayHandler = FindObjectOfType<GameplayHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseUp()
    {
        if (gameplayHandler.GetClickStatus())
            return;
        if (GetGridHighlighted())
            return;
        gameplayHandler.SetClickStatus(true);
        sprite.color = new Color(254f/255f, 216f/255f, 111f/255f);
        gameAreaHandler.SetCurrentClicked(rowIndex, columnIndex);
        gridHighlighted = true;
    }

    public bool GetGridHighlighted()
    {
        return gridHighlighted;
    }
    public void SetGridHighlighted(bool value)
    {
        gridHighlighted = value;
    }
    public bool GetDeadStatus()
    {
        return died;
    }
    public void SetDeadStatus(bool dead)
    {
         died = dead;
    }

    public void SetGridIndexes(int row , int column)
    {
        rowIndex = row;
        columnIndex = column;
    }
    public int GetRowIndex()
    {
        return rowIndex;
    }
    public int GetColumnIndex()
    {
        return columnIndex;
    }
    public void HighlightGrid()
    {
        if (GetDeadStatus())
            return;
        SetGridHighlighted(true);
        sprite.color = new Color(198f/255f, 223f/255f, 182f/255f);
    }
    public void GridDestroyed()
    {
        Debug.Log(GetDeadStatus());
        if (GetDeadStatus())
            return;
        Debug.Log("Get High "+GetGridHighlighted());

        if (GetGridHighlighted())
        {
            SetDeadStatus(true);
            sprite.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        }
    }
}

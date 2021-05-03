using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaHandler : MonoBehaviour
{
    GameplayHandler gameplayHandler;
    float width;
    float height;
    [SerializeField] GridHandler Grid;
    [SerializeField] GameObject GameOverText;
    GridHandler[,] Grids;
    int clickedRow, clickedColumn;
    GameObject GameAreaRect;
    bool GameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameplayHandler = FindObjectOfType<GameplayHandler>();
        GameOverText.SetActive(false);
        CreateGameArea();
    }

    private void CreateGameArea()
    {

        int row = gameplayHandler.GetRowCount();
        int column = gameplayHandler.GetColumnCount();
        int centerRow, centerCol;
        float tempX = 0; ;
        float tempY = 0;
        Grids = new GridHandler[row, column];
        
        
        for (int rowIndex = 0; rowIndex < row; rowIndex++)
        {
            for (int colIndex = 0; colIndex < column; colIndex++)
            {
                Grids[rowIndex,colIndex] = Instantiate<GridHandler>(Grid, new Vector3(rowIndex,colIndex, 0), Quaternion.identity);
                Grids[rowIndex, colIndex].gameObject.SetActive(false);
            }
        }

        if(row % 2 == 0)
        {
            centerRow = row / 2 - 1;
        }
        else
        {
            centerRow = row / 2;
        }
        if (column % 2 == 0)
        {
            centerCol = column / 2 - 1;
        }
        else
        {
            centerCol = column / 2;
        }
        if (row % 2 == 0 && column % 2 == 0)
        {
            tempX = Grids[centerRow, centerCol].transform.position.x;
            tempY = Grids[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0.5f;
            tempY = tempY + 0.5f;
        }
        else if (row % 2 == 0 && column % 2 != 0)
        {
            tempX = Grids[centerRow, centerCol].transform.position.x;
            tempY = Grids[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0.5f;
            tempY = tempY + 0f;
        }
        else if (row % 2 != 0 && column % 2 == 0)
        {
            tempX = Grids[centerRow, centerCol].transform.position.x;
            tempY = Grids[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0f;
            tempY = tempY + 0.5f;
        }
        else if (row % 2 != 0 && column % 2 != 0)
        {
            tempX = Grids[centerRow, centerCol].transform.position.x;
            tempY = Grids[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0f;
            tempY = tempY + 0f;
        }
        for (int rowIndex = 0; rowIndex < row; rowIndex++)
        {
            for (int colIndex = 0; colIndex < column; colIndex++)
            {
                Grids[rowIndex, colIndex].gameObject.transform.position = new Vector3((Grids[rowIndex, colIndex].transform.position.x - tempX), (Grids[rowIndex, colIndex].transform.position.y - tempY));
                Grids[rowIndex, colIndex].gameObject.SetActive(true);
                Grids[rowIndex, colIndex].SetGridIndexes(rowIndex, colIndex);

            }
        }
        
    }

    public void SetCurrentClicked(int row, int column)
    {
        clickedRow = row;
        clickedColumn = column;
        FindTheDiagonalPaths();
    }
    private void FindTheDiagonalPaths()
    {

        int row = clickedRow;
        int column = clickedColumn;

        FindTopRight(row, column);
        FindTopLeft(row, column);
        FindBottomRight(row, column);
        FindBottomLeft(row, column);
        StartCoroutine(WaitToKill());
    }

    private void KillTheGrids()
    {
        bool deadStatus = true;
        for (int rowIndex = 0; rowIndex < gameplayHandler.GetRowCount(); rowIndex++)
        {
            for (int colIndex = 0; colIndex < gameplayHandler.GetColumnCount(); colIndex++)
            {
                Grids[rowIndex, colIndex].GridDestroyed();
                if (Grids[rowIndex, colIndex].GetDeadStatus() == false)
                    deadStatus = false;
            }
        }
        if (deadStatus == true)
        {
            StartCoroutine(DisplayGameOver());
        }
        gameplayHandler.SetClickStatus(false);
    }

    IEnumerator WaitToKill()
    {
        yield return new WaitForSeconds(1);
        KillTheGrids();
    }
    IEnumerator DisplayGameOver()
    {
        yield return new WaitForSeconds(1);
        GameOverText.SetActive(true);
        for (int rowIndex = 0; rowIndex < gameplayHandler.GetRowCount(); rowIndex++)
        {
            for (int colIndex = 0; colIndex < gameplayHandler.GetColumnCount(); colIndex++)
            {
                Grids[rowIndex, colIndex].gameObject.SetActive(false);
            }
        }
    }
    private void FindBottomLeft(int row, int column)
    {
        while (row > 0 && column > 0)
        {
            row--;
            column--;
            if (Grids[row, column].GetGridHighlighted())
                break;
            Grids[row, column].HighlightGrid();
        }
    }

    private void FindBottomRight(int row, int column)
    {
        while (row < gameplayHandler.GetRowCount() - 1 && column > 0)
        {
            row++;
            column--;
            if (Grids[row, column].GetGridHighlighted())
                break;
            Grids[row, column].HighlightGrid();
        }
    }

    private void FindTopLeft(int row, int column)
    {
        while (row > 0 && column < gameplayHandler.GetColumnCount() - 1)
        {
            row--;
            column++;
            if (Grids[row, column].GetGridHighlighted())
                break;
            Grids[row, column].HighlightGrid();
        }
    }

    private void FindTopRight(int row, int column)
    {
        while(row < gameplayHandler.GetRowCount() - 1 && column < gameplayHandler.GetColumnCount() - 1)
        {
            row++;
            column++;
            if (Grids[row, column].GetGridHighlighted())
                break;
            Grids[row, column].HighlightGrid();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

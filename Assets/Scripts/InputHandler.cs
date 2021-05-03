using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TMP_InputField rowCount;
    [SerializeField] TMP_InputField columnCount;
    [SerializeField] GameObject rowWarning;
    [SerializeField] GameObject columnWarning;

    //defalut row and column count
    int DEFAULT_MIN_COUNT = 2;
    int DEFAULT_MAX_COUNT = 10;
    int rowCountValue = 0;
    int columnCountValue = 0;
    bool validRowInput = false;
    bool validColumninput = false;
    GameplayHandler gameplayHandler;
    void Start()
    {
        columnWarning.SetActive(false);
        rowWarning.SetActive(false);
        gameplayHandler = FindObjectOfType<GameplayHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        var rCount = rowCount.GetComponent<TMP_InputField>().text;
        var cCount = columnCount.GetComponent<TMP_InputField>().text;
        rowCountValue = 0;
        columnCountValue = 0;
        if (rCount.Length != 0)
            rowCountValue = System.Convert.ToInt32(rCount);
        if(cCount.Length != 0)
            columnCountValue = System.Convert.ToInt32(cCount);

        if (rowCountValue < DEFAULT_MIN_COUNT || rowCountValue > DEFAULT_MAX_COUNT)
        {
            rowWarning.SetActive(true);
            validRowInput = false;
        }
        else
        {
            validRowInput = true;
            rowWarning.SetActive(false);
        }
        if (columnCountValue < DEFAULT_MIN_COUNT || columnCountValue > DEFAULT_MAX_COUNT)
        {
            columnWarning.SetActive(true);
            validColumninput = false;
        }
        else
        {
            columnWarning.SetActive(false);
            validColumninput = true;
        }
        if(validColumninput == true && validRowInput == true)
        {
            gameplayHandler.SetGridCount(rowCountValue, columnCountValue);
            SceneManager.LoadScene(1);
        }
    }
}

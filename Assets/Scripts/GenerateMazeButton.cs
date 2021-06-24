using System;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMazeButton : MonoBehaviour
{
    public Button generateMaze;
    public InputField inputRows;
    public InputField inputColumns;
    public Dropdown algorithmChoose;
    public Canvas mainCanvas;
    public GameObject cellPrefab;
    private Cell[,] layout;

    private GameObject MainCamera;


    private void Awake()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void Generate()
    {
        if (inputColumns.text == "" || inputRows.text == "")
            return;
        int rows = int.Parse(inputColumns.text);
        int columns = int.Parse(inputRows.text);
        inputRows.text = "";
        inputColumns.text = "";

        mainCanvas.enabled = false;

        float scaleMaze;

        float fullSizeX = 0.96f * columns + 0.16f * (columns + 1);
        float fullSizeY = 0.96f * rows + 0.16f * (rows + 1);


        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // bottom-left corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // top-right corner
        float heightCamera = max.y - min.y;
        float widthCamera = max.y - min.y;

        if (heightCamera > widthCamera)
            scaleMaze = widthCamera / fullSizeX;
        else
            scaleMaze = heightCamera / fullSizeY;

        MainCamera.transform.position = new Vector3(
            (fullSizeX / 2 - (0.96f / 2 + 0.16f)) * scaleMaze,
            (fullSizeY / 2 - (0.96f / 2 + 0.16f)) * scaleMaze,
            -1);


        switch (algorithmChoose.value)
        {
            case 0:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 1:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 2:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 3:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 4:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 5:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 6:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 7:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 8:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 9:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 10:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 11:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 12:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
            case 13:
                layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);
                break;
        }


        DrawMaze(rows, columns, scaleMaze);
    }

    private void DrawMaze(int rows, int columns, float scaleMaze)
    {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
            {
                var temp = Instantiate(
                    cellPrefab,
                    new Vector3(
                        (float) 1.12 * j * scaleMaze,
                        (float) 1.12 * i * scaleMaze,
                        0),
                    Quaternion.identity);
    
    
                temp.transform.localScale = new Vector3(scaleMaze, scaleMaze, 1);
    
                var childsTransforms = temp.GetComponentsInChildren<Transform>();
                foreach (var child in childsTransforms)
                {
                    RemoveWalls(child.gameObject, i, j, rows, columns);
                }
            }
    }

    private void RemoveWalls(GameObject temp, int row, int column, int rows, int columns)
    {
        if (temp.CompareTag($"Floor"))
        {
            if (row == 0 && column == 0)
                temp.GetComponent<SpriteRenderer>().color = Color.green;
            if (row == (rows - 1) && column == (columns - 1))
                temp.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        if (temp.CompareTag("RightWall") && column != (columns - 1))
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (temp.CompareTag($"DownWall") && row != 0)
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (layout[row, column].Left == false && temp.CompareTag($"LeftWall") && column != 0)
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (layout[row, column].Up == false && temp.CompareTag($"UpWall") && row != (rows - 1))
            temp.GetComponent<SpriteRenderer>().enabled = false;
    }
}
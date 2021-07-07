using System;
using System.Diagnostics;
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
    private Cell[,] _layout;

    private Transform _mainCameraTransform;


    private void Awake()
    {
        _mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    public void Generate()
    {
        if (String.IsNullOrEmpty(inputColumns.text) || String.IsNullOrEmpty(inputRows.text))
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
        //float widthCamera = max.x - min.x;

        if (fullSizeX > fullSizeY)
            scaleMaze = heightCamera / fullSizeX;
        else
            scaleMaze = heightCamera / fullSizeY;


        _mainCameraTransform.transform.position = new Vector3(
            (fullSizeX / 2 - (0.96f / 2 + 0.16f)) * scaleMaze,
            (fullSizeY / 2 - (0.96f / 2 + 0.16f)) * scaleMaze,
            -1);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        _layout = algorithmChoose.value switch
        {
            0 => GenerateAlgorithms.AlgorithmRecursiveBacktracker(rows, columns),
            1 => GenerateAlgorithms.AlgorithmKruskal(rows, columns),
            2 => GenerateAlgorithms.AlgorithmAldousBroder(rows, columns),
            3 => GenerateAlgorithms.AlgorithmWilson(rows, columns),
            4 => GenerateAlgorithms.AlgorithmHuntKill(rows, columns),
            5 => GenerateAlgorithms.AlgorithmGrowingTree(rows, columns),
            6 => GenerateAlgorithms.AlgorithmGrowingForest(rows, columns),
            7 => GenerateAlgorithms.AlgorithmEller(rows, columns),
            8 => GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns),
            9 => GenerateAlgorithms.AlgorithmSidewinder(rows, columns)
        };
        stopwatch.Stop();
        print("Creating maze layout: " + stopwatch.Elapsed);

        DrawMaze(rows, columns, scaleMaze);

        AnalyzeMaze(_layout, rows, columns);
    }

    private void AnalyzeMaze(Cell[,] layout, int rows, int columns)
    {
        int deadlocks = 0;
        int Tcrossroads = 0;
        int crossroads = 0;
        int turns = 0;

        var IsPassage = new bool[4]; // 0 - left; 1- up; 2 - right; 3- down
        int tempTurns;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                for (var k = 0; k < IsPassage.Length; k++)
                    IsPassage[k] = true;
                tempTurns = 0;

                #region check for Passages
                //Left wall
                if (j == 0)
                    IsPassage[0] = false;
                else if (layout[i, j].Left)
                    IsPassage[0] = false;
                //right wall
                if (j == (columns - 1))
                    IsPassage[2] = false;
                else if (layout[i, j + 1].Left)
                    IsPassage[2] = false;
                //up wall
                if (i == (columns - 1))
                    IsPassage[1] = false;
                else if (layout[i, j].Up)
                    IsPassage[1] = false;
                //down wall
                if (i == 0)
                    IsPassage[3] = false;
                else if (layout[i - 1, j].Up)
                    IsPassage[3] = false;
                #endregion

                #region Count turns

                if (IsPassage[0] && IsPassage[1])
                    tempTurns++;
                if (IsPassage[1] && IsPassage[2])
                    tempTurns++;
                if (IsPassage[2] && IsPassage[3])
                    tempTurns++;
                if (IsPassage[3] && IsPassage[0])
                    tempTurns++;

                #endregion

                if (tempTurns == 0 &&
                    ((IsPassage[0] && IsPassage[2]) || (IsPassage[1] && IsPassage[3])))
                    continue;
                
                
                switch (tempTurns)
                {
                    case 0:
                        deadlocks++;
                        break;
                    case 1:
                        turns++;
                        break;
                    case 2:
                        Tcrossroads++;
                        break;
                    case 4:
                        crossroads++;
                        break;
                }
            }
        }

        print("deadlocks " + deadlocks);
        print("turns " + turns);
        print("Tcrossroads " + Tcrossroads);
        print("crossroads " + crossroads);
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
            else if (row == (rows - 1) && column == (columns - 1))
                temp.GetComponent<SpriteRenderer>().color = Color.blue;
            else
            {
                temp.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (temp.CompareTag("RightWall") && column != (columns - 1))
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (temp.CompareTag($"DownWall") && row != 0)
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (_layout[row, column].Left == false && temp.CompareTag($"LeftWall") && column != 0)
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (_layout[row, column].Up == false && temp.CompareTag($"UpWall") && row != (rows - 1))
            temp.GetComponent<SpriteRenderer>().enabled = false;
    }
}
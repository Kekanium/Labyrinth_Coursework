using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{

    public int rows;
    public int columns;
    

    public GameObject cellPrefab;
    private Cell[,] layout;

    private float fullSizeX;
    private float fullSizeY;
    private float scaleMaze;

    private void Start()
    {
        fullSizeX =  0.96f * columns +  0.16f * (columns + 1);
        fullSizeY = 0.96f * rows + 0.16f * (rows + 1);

        
        if (Screen.height > Screen.width)
            scaleMaze = Screen.width / fullSizeX;
        else
            scaleMaze = Screen.height / fullSizeY;

        this.transform.position = new Vector3(
            (fullSizeX / 2 - (0.96f / 2 + 0.16f)) * scaleMaze,
            (fullSizeY / 2 - (0.96f / 2 + 0.16f)) * scaleMaze,
            -1);
        
        
        layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);

        DrawMaze();
        
        
        
    }

    private void DrawMaze()
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
                removeWalls(child.gameObject,i,j);
            }
        }

    }

    private void removeWalls(GameObject temp, int row, int column)
    {
        if (temp.CompareTag($"RightWall") && column!=(columns-1))
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (temp.CompareTag($"DownWall") && row != 0)
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (layout[row, column].Left == false && temp.CompareTag($"LeftWall") && column != 0)
            temp.GetComponent<SpriteRenderer>().enabled = false;
        if (layout[row, column].Up == false && temp.CompareTag($"UpWall") && row != (rows - 1))
            temp.GetComponent<SpriteRenderer>().enabled = false;
    }
}

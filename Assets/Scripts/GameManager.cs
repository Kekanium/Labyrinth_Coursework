using System;
using UnityEngine;
using Unity.UI
public class GameManager : MonoBehaviour
{

    public int rows;
    public int columns;
    

    public GameObject cellPrefab;
    private Cell[,] layout;

    private float fullSizeX;
    private float fullSizeY;
    private float scaleMaze;
    void Start()
    {
        fullSizeX =  0.96f * columns +  0.16f * (columns + 1);
        fullSizeY = 0.96f * rows + 0.16f * (rows + 1);

        
        if (Screen.height > Screen.width)
            scaleMaze = Screen.width / fullSizeX;
        else
            scaleMaze = Screen.height / fullSizeY;
        
        this.transform.position = new Vector3(fullSizeX / 2 * scaleMaze-(0.96f/2+0.16f)* scaleMaze, fullSizeY / 2*scaleMaze-(0.96f/2+0.16f)* scaleMaze,-1);
        
        
        layout = GenerateAlgorithms.AlgorithmBinaryTrees(rows, columns);

        DrawMaze();
        
        
        
    }

    void DrawMaze()
    {
        int i;
        int j;
        
        for (i = 0; i < rows; i++)
        for (j = 0; j < columns; j++)
        {
            var temp = Instantiate(cellPrefab, new Vector3((float)1.12 * i*scaleMaze, (float)1.12 * j*scaleMaze, 0),Quaternion.identity);
            temp.transform.localScale=new Vector3(scaleMaze, scaleMaze, 1);
        }
        var objectsForLook = GameObject.FindGameObjectsWithTag("Cell");

    }
}

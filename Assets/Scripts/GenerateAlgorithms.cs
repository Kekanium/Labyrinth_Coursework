using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public static class GenerateAlgorithms
{
    public static Cell[,] AlgorithmBinaryTrees(int rows, int columns)
    {
        var maze = new Cell[rows, columns];

        var rand = new Random();

        for (var i = rows - 1; i >= 0; --i)
        for (var j = columns - 1; j >= 0; --j)
        {
            maze[i, j] = new Cell();
            if (rand.Next(0, 1) == 0)
            {
                if (j == 0)
                    maze[i, j].Up = false;
                else
                    maze[i, j].Left = false;
            }
            else
            {
                if (i == (rows - 1))
                    maze[i, j].Left = false;
                else
                    maze[i, j].Up = false;
            }
        }

        return maze;
    }
}
using System;
using System.Collections.Generic;

public static class GenerateAlgorithms
{
    public static Cell[,] AlgorithmBinaryTrees(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        var rand = new System.Random();

        for (int i = rows - 1; i >= 0; --i)
        for (int j = columns - 1; j >= 0; --j)
        {
            maze[i, j] = new Cell();
            if (rand.Next() % 2 == 0)
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

    public static Cell[,] AlgorithmRecursiveBacktracker(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                maze[i, j] = new Cell();
        
        Stack<int[]> stackMaze = new Stack<int[]>();

        //Index of the current cell
        int currIndexRow = 0;
        int currIndexColumn = 0;

        //track add cell to the stack or not; 1 - yes; 0 - no
        int[,] arrayOfStack = new int[rows, columns];

        var rand = new System.Random();

        do
        {
            int[] arrayOfWay = new int[4];
            
            bool way0 = false, way1 = false, way2 = false, way3 = false;
            int index = 0;
            
            if (((currIndexColumn + 1) < columns) && (arrayOfStack[currIndexRow, currIndexColumn + 1] == 0))
            {
                way2 = true;
                arrayOfWay[index] = 2;
                ++index;
            }
            if (((currIndexRow + 1) < rows) && (arrayOfStack[currIndexRow + 1, currIndexColumn] == 0))
            {
                way3 = true;
                arrayOfWay[index] = 3;
                ++index;
            }
            if (((currIndexColumn - 1) >= 0) && (arrayOfStack[currIndexRow, currIndexColumn - 1] == 0))
            {
                way0 = true;
                arrayOfWay[index] = 0;
                ++index;
            }
            if (((currIndexRow - 1) >= 0) && (arrayOfStack[currIndexRow - 1, currIndexColumn] == 0))
            {
                way1 = true;
                arrayOfWay[index] = 1;
                ++index;
            }

            if ((way0 == false) && (way1 == false) && (way3 == false) && (way2 == false))
            {
                if (arrayOfStack[currIndexRow, currIndexColumn] != 1)
                {
                    arrayOfStack[currIndexRow, currIndexColumn] = 1;
                }
                else
                {
                    stackMaze.Pop();
                }
                
                if (stackMaze.Count != 0)
                {
                    int[] tempArray = new int[2];
                    tempArray = stackMaze.Peek();
                    currIndexRow = tempArray[0];
                    currIndexColumn = tempArray[1];
                }
            }
            else
            {
                int randInd = rand.Next() % index;
                int way = arrayOfWay[randInd];

                if (way == 2)
                {
                    int[] arrayOfIndex = new int[2];
                    arrayOfIndex[0] = currIndexRow;
                    arrayOfIndex[1] = currIndexColumn;
                    
                    stackMaze.Push(arrayOfIndex);
                    
                    arrayOfStack[currIndexRow, currIndexColumn] = 1;
                    currIndexColumn += 1;

                    maze[currIndexRow, currIndexColumn].Left = false;
                }
                else if (way == 3)
                {
                    int[] arrayOfIndex = new int[2];
                    arrayOfIndex[0] = currIndexRow;
                    arrayOfIndex[1] = currIndexColumn;
                    
                    stackMaze.Push(arrayOfIndex);
                    
                    arrayOfStack[currIndexRow, currIndexColumn] = 1;
                    currIndexRow += 1;

                    maze[currIndexRow, currIndexColumn].Up = false;
                }
                else if (way == 0)
                {
                    int[] arrayOfIndex = new int[2];
                    arrayOfIndex[0] = currIndexRow;
                    arrayOfIndex[1] = currIndexColumn; 
                    
                    stackMaze.Push(arrayOfIndex);
                    
                    arrayOfStack[currIndexRow, currIndexColumn] = 1;
                    maze[currIndexRow, currIndexColumn].Left = false;
                    currIndexColumn -= 1;
                }
                else if (way == 1)
                {
                    int[] arrayOfIndex = new int[2];
                    arrayOfIndex[0] = currIndexRow;
                    arrayOfIndex[1] = currIndexColumn;
                    
                    stackMaze.Push(arrayOfIndex); 
                    
                    arrayOfStack[currIndexRow, currIndexColumn] = 1;
                    
                    maze[currIndexRow, currIndexColumn].Up = false;
                    currIndexRow -= 1;
                }
            }
        } while (!((currIndexRow == 0) && (currIndexColumn == 0)));

        Cell[,] avstraliezStas = new Cell[rows, columns];
        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            avstraliezStas[i, j] = new Cell();
            avstraliezStas[i, j].Left = maze[rows - i - 1, j].Left;
            avstraliezStas[i, j].Up = maze[rows - i - 1, j].Up;
        }

        return avstraliezStas;
    }
    
}

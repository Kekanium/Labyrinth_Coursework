using System;
using System.Collections.Generic;

public static class GenerateAlgorithms
{
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

        var rand = new Random();

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

    public static Cell[,] AlgorithmKruskal(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //array of cell with unique identifier
        int[,] arrayOfCell = new int[rows, columns];

        int identifier = 0;
        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            arrayOfCell[i, j] = identifier;
            ++identifier;
        }

        //list of all edges
        List<int[]> edges = new List<int[]>();

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            if (j - 1 >= 0)
            {
                int[] tempEdge = new int[4];
                tempEdge[0] = i;
                tempEdge[1] = j;
                tempEdge[2] = i;
                tempEdge[3] = j - 1;

                edges.Add(tempEdge);
            }

            if (i - 1 >= 0)
            {
                int[] tempEdge = new int[4];
                tempEdge[0] = i;
                tempEdge[1] = j;
                tempEdge[2] = i - 1;
                tempEdge[3] = j;

                edges.Add(tempEdge);
            }
        }

        var rand = new Random();

        //number of untreated edges
        int countEdges = edges.Count;

        while (edges.Count != 0)
        {
            int numRand = rand.Next() % countEdges;

            int indRowStart = edges[numRand][0],
                indColumnStart = edges[numRand][1],
                indRowFinish = edges[numRand][2],
                indColumnFinish = edges[numRand][3];

            if (arrayOfCell[indRowStart, indColumnStart] != arrayOfCell[indRowFinish, indColumnFinish])
            {
                int newId = arrayOfCell[indRowStart, indColumnStart],
                    oldId = arrayOfCell[indRowFinish, indColumnFinish];
                for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                {
                    if (arrayOfCell[i, j] == oldId)
                        arrayOfCell[i, j] = newId;
                }

                if (indColumnStart - 1 == indColumnFinish)
                    maze[indRowStart, indColumnStart].Left = false;
                else if (indRowStart - 1 == indRowFinish)
                    maze[indRowStart, indColumnStart].Up = false;
            }

            edges.Remove(edges[numRand]);
            --countEdges;
        }

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

    public static Cell[,] AlgorithmAldousBroder(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //array of cell; 1 - there is way; 0 - there is not way
        int[,] arrayOfCell = new int[rows, columns];

        var rand = new Random();
        int indRows = rand.Next() % rows, indColumns = rand.Next() % columns;
        arrayOfCell[indRows, indColumns] = 1;

        //array of possible ways
        int[] ways = new int[4];

        //cells that we not view
        int unviewedCells = rows * columns - 1;

        while (unviewedCells != 0)
        {
            int length = 0;
            if (indColumns - 1 >= 0)
            {
                ways[length] = 0;
                ++length;
            }

            if (indRows - 1 >= 0)
            {
                ways[length] = 1;
                ++length;
            }

            if (indColumns + 1 < columns)
            {
                ways[length] = 2;
                ++length;
            }

            if (indRows + 1 < rows)
            {
                ways[length] = 3;
                ++length;
            }

            int way = ways[rand.Next() % length];
            if (way == 0)
            {
                if (arrayOfCell[indRows, indColumns - 1] == 0)
                {
                    maze[indRows, indColumns].Left = false;
                    arrayOfCell[indRows, indColumns - 1] = 1;
                    --unviewedCells;
                }

                --indColumns;
            }
            else if (way == 2)
            {
                if (arrayOfCell[indRows, indColumns + 1] == 0)
                {
                    maze[indRows, indColumns + 1].Left = false;
                    arrayOfCell[indRows, indColumns + 1] = 1;
                    --unviewedCells;
                }

                ++indColumns;
            }
            else if (way == 1)
            {
                if (arrayOfCell[indRows - 1, indColumns] == 0)
                {
                    maze[indRows, indColumns].Up = false;
                    arrayOfCell[indRows - 1, indColumns] = 1;
                    --unviewedCells;
                }

                --indRows;
            }
            else if (way == 3)
            {
                if (arrayOfCell[indRows + 1, indColumns] == 0)
                {
                    maze[indRows + 1, indColumns].Up = false;
                    arrayOfCell[indRows + 1, indColumns] = 1;
                    --unviewedCells;
                }

                ++indRows;
            }
        }

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

    public static Cell[,] AlgorithmWilson(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //array of cell; 1 - current way; 0 - not considering; 2 - part of the labyrinth
        int[,] arrayOfCell = new int[rows, columns];

        var rand = new Random();
        int indRows = rand.Next() % rows, indColumns = rand.Next() % columns;
        arrayOfCell[indRows, indColumns] = 2;

        indRows = rand.Next() % rows;
        indColumns = rand.Next() % columns;

        //array of possible ways
        int[] ways = new int[4];

        //cells that we not view
        int unviewedCells = rows * columns - 1;

        while (unviewedCells != 0)
        {
            int currIndRow = indRows, currIndColumn = indColumns;
            bool flagNotValidWay = false;
            while (arrayOfCell[currIndRow, currIndColumn] != 2)
            {
                int length = 0;
                if ((currIndColumn - 1 >= 0) && (arrayOfCell[currIndRow, currIndColumn - 1] != 1))
                {
                    ways[length] = 0;
                    ++length;
                }

                if ((currIndRow - 1 >= 0) && (arrayOfCell[currIndRow - 1, currIndColumn] != 1))
                {
                    ways[length] = 1;
                    ++length;
                }

                if ((currIndColumn + 1 < columns) && (arrayOfCell[currIndRow, currIndColumn + 1] != 1))
                {
                    ways[length] = 2;
                    ++length;
                }

                if ((currIndRow + 1 < rows) && (arrayOfCell[currIndRow + 1, currIndColumn] != 1))
                {
                    ways[length] = 3;
                    ++length;
                }

                int way;
                if (length == 0)
                {
                    flagNotValidWay = true;
                    break;
                }
                else
                {
                    way = ways[rand.Next() % length];
                }

                if (way == 0)
                {
                    maze[currIndRow, currIndColumn].Left = false;
                    arrayOfCell[currIndRow, currIndColumn] = 1;
                    --currIndColumn;
                }
                else if (way == 2)
                {
                    maze[currIndRow, currIndColumn + 1].Left = false;
                    arrayOfCell[currIndRow, currIndColumn] = 1;
                    ++currIndColumn;
                }
                else if (way == 1)
                {
                    maze[currIndRow, currIndColumn].Up = false;
                    arrayOfCell[currIndRow, currIndColumn] = 1;
                    --currIndRow;
                }
                else if (way == 3)
                {
                    maze[currIndRow + 1, currIndColumn].Up = false;
                    arrayOfCell[currIndRow, currIndColumn] = 1;
                    ++currIndRow;
                }
            }

            if (flagNotValidWay)
            {
                for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (arrayOfCell[i, j] == 1)
                    {
                        maze[i, j].Left = true;
                        maze[i, j].Up = true;
                        arrayOfCell[i, j] = 0;
                    }
            }
            else
            {
                for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (arrayOfCell[i, j] == 1)
                    {
                        arrayOfCell[i, j] = 2;
                        --unviewedCells;
                    }
            }

            indRows = rand.Next() % rows;
            indColumns = rand.Next() % columns;
        }

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

    public static Cell[,] AlgorithmHuntKill(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //array of cell; 1 - cell in labyrinth; 0 - not in labyrinth
        int[,] arrayOfCell = new int[rows, columns];

        var rand = new Random();
        int indRows = rand.Next() % rows, indColumns = rand.Next() % columns;
        arrayOfCell[indRows, indColumns] = 1;

        //cells that we not view
        int unviewedCells = rows * columns - 1;

        //array of possible ways
        int[] ways = new int[4];

        while (unviewedCells != 0)
        {
            int length = 0;
            if ((indColumns - 1 >= 0) && (arrayOfCell[indRows, indColumns - 1] != 1))
            {
                ways[length] = 0;
                ++length;
            }

            if ((indRows - 1 >= 0) && (arrayOfCell[indRows - 1, indColumns] != 1))
            {
                ways[length] = 1;
                ++length;
            }

            if ((indColumns + 1 < columns) && (arrayOfCell[indRows, indColumns + 1] != 1))
            {
                ways[length] = 2;
                ++length;
            }

            if ((indRows + 1 < rows) && (arrayOfCell[indRows + 1, indColumns] != 1))
            {
                ways[length] = 3;
                ++length;
            }

            if (length == 0)
            {
                indRows = rand.Next() % rows;
                indColumns = rand.Next() % columns;
                while (arrayOfCell[indRows, indColumns] != 1)
                {
                    indRows = rand.Next() % rows;
                    indColumns = rand.Next() % columns;
                }
            }
            else
            {
                int way;
                way = ways[rand.Next() % length];

                if (way == 0)
                {
                    maze[indRows, indColumns].Left = false;
                    arrayOfCell[indRows, indColumns - 1] = 1;
                    --unviewedCells;
                    --indColumns;
                }
                else if (way == 2)
                {
                    maze[indRows, indColumns + 1].Left = false;
                    arrayOfCell[indRows, indColumns + 1] = 1;
                    --unviewedCells;
                    ++indColumns;
                    ;
                }
                else if (way == 1)
                {
                    maze[indRows, indColumns].Up = false;
                    arrayOfCell[indRows - 1, indColumns] = 1;
                    --unviewedCells;
                    --indRows;
                }
                else if (way == 3)
                {
                    maze[indRows + 1, indColumns].Up = false;
                    arrayOfCell[indRows + 1, indColumns] = 1;
                    --unviewedCells;
                    ++indRows;
                }
            }
        }

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

    public static Cell[,] AlgorithmGrowingTree(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //array of cell; 1 - cell in labyrinth; 0 - not in labyrinth
        int[,] arrayOfCell = new int[rows, columns];

        //list of the cells that include in labyrinth
        List<int[]> viewedСells = new List<int[]>();

        var rand = new Random();
        int indRows = rand.Next() % rows, indColumns = rand.Next() % columns;
        arrayOfCell[indRows, indColumns] = 1;

        int[] temp = new int[2];
        temp[0] = indRows;
        temp[1] = indColumns;
        viewedСells.Add(temp);

        //array of possible ways
        int[] ways = new int[4];

        while (viewedСells.Count != 0)
        {
            int length = 0;
            if ((indColumns - 1 >= 0) && (arrayOfCell[indRows, indColumns - 1] != 1))
            {
                ways[length] = 0;
                ++length;
            }

            if ((indRows - 1 >= 0) && (arrayOfCell[indRows - 1, indColumns] != 1))
            {
                ways[length] = 1;
                ++length;
            }

            if ((indColumns + 1 < columns) && (arrayOfCell[indRows, indColumns + 1] != 1))
            {
                ways[length] = 2;
                ++length;
            }

            if ((indRows + 1 < rows) && (arrayOfCell[indRows + 1, indColumns] != 1))
            {
                ways[length] = 3;
                ++length;
            }

            if (length == 0)
            {
                viewedСells.RemoveAt(viewedСells.FindIndex(x => x[0] == indRows && x[1] == indColumns));
            }
            else
            {
                int way;
                way = ways[rand.Next() % length];

                if (way == 0)
                {
                    maze[indRows, indColumns].Left = false;
                    int[] tempArray = new int[2];
                    tempArray[0] = indRows;
                    tempArray[1] = indColumns - 1;
                    viewedСells.Add(tempArray);
                    arrayOfCell[indRows, indColumns - 1] = 1;
                }
                else if (way == 2)
                {
                    maze[indRows, indColumns + 1].Left = false;
                    int[] tempArray = new int[2];
                    tempArray[0] = indRows;
                    tempArray[1] = indColumns + 1;
                    viewedСells.Add(tempArray);
                    arrayOfCell[indRows, indColumns + 1] = 1;
                }
                else if (way == 1)
                {
                    maze[indRows, indColumns].Up = false;
                    int[] tempArray = new int[2];
                    tempArray[0] = indRows - 1;
                    tempArray[1] = indColumns;
                    viewedСells.Add(tempArray);
                    arrayOfCell[indRows - 1, indColumns] = 1;
                }
                else if (way == 3)
                {
                    maze[indRows + 1, indColumns].Up = false;
                    int[] tempArray = new int[2];
                    tempArray[0] = indRows + 1;
                    tempArray[1] = indColumns;
                    viewedСells.Add(tempArray);
                    arrayOfCell[indRows + 1, indColumns] = 1;
                }
            }

            if (viewedСells.Count != 0)
            {
                int numList = rand.Next() % viewedСells.Count;
                indRows = viewedСells[numList][0];
                indColumns = viewedСells[numList][1];
            }
        }


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

    public static Cell[,] AlgorithmGrowingForest(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //array of cell with unique identifier
        int[,] arrayOfCell = new int[rows, columns];

        int identifier = 0;
        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            arrayOfCell[i, j] = identifier;
            ++identifier;
        }
        
        //list of the cells that we not view
        List<int[]> newСells = new List<int[]>();
        
        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            int[] tempArray = new int[2];
            tempArray[0] = i;
            tempArray[1] = j;
            newСells.Add(tempArray);
        }
        
        //list of the cells that we view now
        List<int[]> currentСells = new List<int[]>();
        
        var rand = new Random();
        int indRows, indColumns;
        int index = rand.Next() % newСells.Count;
        
        currentСells.Add(newСells[index]);
        indRows = newСells[index][0];
        indColumns = newСells[index][1];
        newСells.RemoveAt(index);

        //list of the cells that we view now
        List<int[]> viewedСells = new List<int[]>();

        //array of possible ways
        int[] ways = new int[4];

        while (currentСells.Count != 0)
        {
            index = rand.Next() % currentСells.Count;
            indRows = currentСells[index][0];
            indColumns = currentСells[index][1];
            
            int length = 0;
            if ((indColumns - 1 >= 0) && (newСells.FindIndex(x => x[0] == indRows && x[1] == (indColumns - 1)) != -1))
            {
                ways[length] = 0;
                ++length;
            }
            if ((indRows - 1 >= 0) && (newСells.FindIndex(x => x[0] == (indRows - 1) && x[1] == indColumns) != -1))
            {
                ways[length] = 1;
                ++length;
            }
            if ((indColumns + 1 < columns) && (newСells.FindIndex(x => x[0] == indRows && x[1] == (indColumns + 1)) != -1))
            {
                ways[length] = 2;
                ++length;
            }
            if ((indRows + 1 < rows) && (newСells.FindIndex(x => x[0] == (indRows + 1) && x[1] == indColumns) != -1))
            {
                ways[length] = 3;
                ++length;
            }

            if (length != 0)
            {
                int way = ways[rand.Next() % length];
                if (way == 0)
                {
                    maze[indRows, indColumns].Left = false;
                    arrayOfCell[indRows, indColumns - 1] = arrayOfCell[indRows, indColumns];
                    int indexInNew = newСells.FindIndex(x => x[0] == indRows && x[1] == (indColumns - 1));
                    currentСells.Add(newСells[indexInNew]);
                    newСells.RemoveAt(indexInNew);
                }
                else if (way == 2)
                {
                    maze[indRows, indColumns + 1].Left = false;
                    arrayOfCell[indRows, indColumns + 1] = arrayOfCell[indRows, indColumns];
                    int indexInNew = newСells.FindIndex(x => x[0] == indRows && x[1] == (indColumns + 1));
                    currentСells.Add(newСells[indexInNew]);
                    newСells.RemoveAt(indexInNew);
                }
                else if (way == 1)
                {
                    maze[indRows, indColumns].Up = false;
                    arrayOfCell[indRows - 1, indColumns] = arrayOfCell[indRows, indColumns];
                    int indexInNew = newСells.FindIndex(x => x[0] == (indRows - 1) && x[1] == indColumns);
                    currentСells.Add(newСells[indexInNew]);
                    newСells.RemoveAt(indexInNew);
                }
                else if (way == 3)
                {
                    maze[indRows + 1, indColumns].Up = false;
                    arrayOfCell[indRows + 1, indColumns] = arrayOfCell[indRows, indColumns];
                    int indexInNew = newСells.FindIndex(x => x[0] == (indRows + 1) && x[1] == indColumns);
                    currentСells.Add(newСells[indexInNew]);
                    newСells.RemoveAt(indexInNew);
                }
            }
            else
            {
                viewedСells.Add(currentСells[index]);
                currentСells.RemoveAt(index);
            }
        }

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

    public static Cell[,] AlgorithmEller(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();
        
        //array of cell with unique identifier (set)
        int[,] arrayOfCell = new int[rows, columns];
        
        int identifier = 0;
        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            arrayOfCell[i, j] = identifier;
            ++identifier;
        }

        var rand = new Random();
        for (int i = rows - 1; i > 0; --i)
        {
            for (int j = 1; j < columns; ++j)
            {
                int randWay = rand.Next() % 2;
                if ((randWay == 0) && (arrayOfCell[i, j] != arrayOfCell[i, j - 1]))
                {
                    maze[i, j].Left = false;
                    arrayOfCell[i, j] = arrayOfCell[i, j - 1];
                }
            }

            bool flagWay = false;
            int countOfSet = 0, numSet = arrayOfCell[i, 0];
            for (int j = 0; j < columns; ++j)
            {
                if (arrayOfCell[i, j] != numSet)
                {
                    numSet = arrayOfCell[i, j];
                    if ((countOfSet == 1) || (flagWay == false))
                    {
                        maze[i, j - 1].Up = false;
                        arrayOfCell[i - 1, j - 1] = arrayOfCell[i, j - 1];
                    }

                    flagWay = false;
                    countOfSet = 0;
                }
                ++countOfSet;
                
                int randWay = rand.Next() % 2;
                if (randWay == 0)
                {
                    flagWay = true;
                    maze[i, j].Up = false;
                    arrayOfCell[i - 1, j] = arrayOfCell[i, j];
                }
            }

            if (flagWay == false)
            {
                maze[i, columns - 1].Up = false;
                arrayOfCell[i - 1, columns - 1] = arrayOfCell[i, columns - 1];
            }
        }

        for (int j = 0; j < (columns - 1); ++j)
        {
            if (arrayOfCell[0, j] != arrayOfCell[0, j + 1])
            {
                arrayOfCell[0, j + 1] = arrayOfCell[0, j];
                maze[0, j + 1].Left = false;
            }
        }

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

    public static Cell[,] AlgorithmBinaryTrees(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        var rand = new Random();

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

    public static Cell[,] AlgorithmSidewinder(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows, columns];

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
            maze[i, j] = new Cell();

        //first row of maze without wall
        for (int i = 1; i < columns; ++i)
            maze[0, i].Left = false;

        var rand = new Random();

        for (int i = 1; i < rows; ++i)
        {
            int flagWallRight, countCell = 0, startindexPassage = 0;
            for (int j = 0; j < columns; ++j)
            {
                flagWallRight = rand.Next() % 2;
                ++countCell;
                if ((flagWallRight == 1) && (j != columns - 1))
                {
                    maze[i, j + 1].Left = false;
                }
                else if ((flagWallRight == 0) || ((flagWallRight == 1) && (j == columns - 1)))
                {
                    int wallUp = rand.Next() % countCell;
                    wallUp += startindexPassage;
                    maze[i, wallUp].Up = false;

                    startindexPassage = j + 1;
                    countCell = 0;
                }
            }
        }

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
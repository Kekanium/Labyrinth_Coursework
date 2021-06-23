using System;

public static class GenerateAlgorithms
{
    public static Cell[,] AlgorithmBinaryTrees(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows,columns];
        
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
                if (i == (rows-1))
                    maze[i, j].Left = false;
                else
                    maze[i, j].Up = false;
            }
        }
        
        
        
        
        return maze;
    }
    
    
    
    
    
    
    
    
    
    
}

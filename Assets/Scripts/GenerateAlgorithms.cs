using System;

public class GenerateAlgorithms
{
    public Cell[,] AlgorithmBinaryTrees(int rows, int columns)
    {
        Cell[,] maze = new Cell[rows,columns];
        
        var rand = new System.Random();

        for (int i = 0; i < rows; ++i)
        for (int j = 0; j < columns; ++j)
        {
            if (rand.Next() % 2 == 0)
                maze[i, j].Left = false;
            else
                maze[i, j].Up = false;
        }
        
        
        
        
        return maze;
    }
    
    
    
    
    
    
    
    
    
    
}

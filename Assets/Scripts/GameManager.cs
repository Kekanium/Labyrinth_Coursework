using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Cell[,] layout;
    void Start()
    {
        layout = GenerateAlgorithms.AlgorithmBinaryTrees(10, 10);
    }
}

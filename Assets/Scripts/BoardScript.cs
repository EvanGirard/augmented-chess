using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static int[,] BoardMatrix;
    private Camera _camera;
    
    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        
        BoardMatrix = new int[8, 8];
        Debug.Log(BoardMatrix);
        for (var i = 0; i < 8; i++)
        {
            BoardMatrix[i, 0] = 1;
            BoardMatrix[i, 1] = 1;
            BoardMatrix[i, 6] = 2;
            BoardMatrix[i, 7] = 2;
        }
    }

}

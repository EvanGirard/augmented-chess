using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<(int,int)> KnightPossibleMoves(int i, int j)
    {
        var moves = new List<(int,int)>();
        if (i+1 < 8 && j-2 >= 0 && BoardScript.BoardMatrix[i+1,j-2] == 0) moves.Add((i+1,j-2)); 
        if (i-1 >= 0 && j+2 < 8 && BoardScript.BoardMatrix[i-1,j+2] == 0) moves.Add((i-1,j+2));
        if (i+2 < 8 && j-1 >= 0 && BoardScript.BoardMatrix[i+2,j-1] == 0) moves.Add((i+2,j-1)); 
        if (i-2 >= 0 && j+1 < 8 && BoardScript.BoardMatrix[i-2,j+1] == 0) moves.Add((i-2,j+1)); 
        if (i+1 < 8 && j+2 < 8 && BoardScript.BoardMatrix[i+1,j+2] == 0) moves.Add((i+1,j+2)); 
        if (i-1 >= 0 && j-2 >= 0 && BoardScript.BoardMatrix[i-1,j-2] == 0) moves.Add((i-1,j-2)); 
        if (i+2 < 8 && j+1 < 8 && BoardScript.BoardMatrix[i+2,j+1] == 0) moves.Add((i+2,j+1)); 
        if (i-2 >= 0 && j-1 >= 0 && BoardScript.BoardMatrix[i-2,j-1] == 0) moves.Add((i-2,j-1)); 

        return moves;
    }

    public static List<(int, int)> KnightPossibleAttacks(int i, int j)
    {
        var attacks = new List<(int,int)>();
        if (i+1 < 8 && j-2 >= 0 && BoardScript.BoardMatrix[i+1,j-2] != 0) attacks.Add((i+1,j-2)); 
        if (i-1 >= 0 && j+2 < 8 && BoardScript.BoardMatrix[i-1,j+2] != 0) attacks.Add((i-1,j+2));
        if (i+2 < 8 && j-1 >= 0 && BoardScript.BoardMatrix[i+2,j-1] != 0) attacks.Add((i+2,j-1)); 
        if (i-2 >= 0 && j+1 < 8 && BoardScript.BoardMatrix[i-2,j+1] != 0) attacks.Add((i-2,j+1)); 
        if (i+1 < 8 && j+2 < 8 && BoardScript.BoardMatrix[i+1,j+2] != 0) attacks.Add((i+1,j+2)); 
        if (i-1 >= 0 && j-2 >= 0 && BoardScript.BoardMatrix[i-1,j-2] != 0) attacks.Add((i-1,j-2)); 
        if (i+2 < 8 && j+1 < 8 && BoardScript.BoardMatrix[i+2,j+1] != 0) attacks.Add((i+2,j+1)); 
        if (i-2 >= 0 && j-1 >= 0 && BoardScript.BoardMatrix[i-2,j-1] != 0) attacks.Add((i-2,j-1)); 

        return attacks;
    }
}

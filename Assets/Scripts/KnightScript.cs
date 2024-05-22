using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : PiecesScript
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<(int,int)> Moves()
    {
        var moves = new List<(int,int)>();
        if (Position.Item1+1 < 8 && Position.Item2-2 >= 0 && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2-2] == 0) 
            moves.Add((Position.Item1+1,Position.Item2-2)); 
        if (Position.Item1-1 >= 0 && Position.Item2+2 < 8 && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2+2] == 0) 
            moves.Add((Position.Item1-1,Position.Item2+2));
        if (Position.Item1+2 < 8 && Position.Item2-1 >= 0 && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2-1] == 0) 
            moves.Add((Position.Item1+2,Position.Item2-1)); 
        if (Position.Item1-2 >= 0 && Position.Item2+1 < 8 && BoardScript.BoardMatrix[Position.Item1-2,Position.Item2+1] == 0) 
            moves.Add((Position.Item1-2,Position.Item2+1)); 
        if (Position.Item1+1 < 8 && Position.Item2+2 < 8 && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2+2] == 0) 
            moves.Add((Position.Item1+1,Position.Item2+2)); 
        if (Position.Item1-1 >= 0 && Position.Item2-2 >= 0 && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2-2] == 0) 
            moves.Add((Position.Item1-1,Position.Item2-2)); 
        if (Position.Item1+2 < 8 && Position.Item2+1 < 8 && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2+1] == 0) 
            moves.Add((Position.Item1+2,Position.Item2+1)); 
        if (Position.Item1-2 >= 0 && Position.Item2-1 >= 0 && BoardScript.BoardMatrix[Position.Item1-2,Position.Item2-1] == 0) 
            moves.Add((Position.Item1-2,Position.Item2-1)); 

        return moves;
    }

    public override List<(int, int)> Attacks()
    {
        var attacks = new List<(int,int)>();
        if (Position.Item1+1 < 8 && Position.Item2-2 >= 0 && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2-2] == EnemiesInt) 
            attacks.Add((Position.Item1+1,Position.Item2-2)); 
        if (Position.Item1-1 >= 0 && Position.Item2+2 < 8 && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2+2] == EnemiesInt) 
            attacks.Add((Position.Item1-1,Position.Item2+2));
        if (Position.Item1+2 < 8 && Position.Item2-1 >= 0 && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2-1] == EnemiesInt) 
            attacks.Add((Position.Item1+2,Position.Item2-1)); 
        if (Position.Item1-2 >= 0 && Position.Item2+1 < 8 && BoardScript.BoardMatrix[Position.Item1-2,Position.Item2+1] == EnemiesInt) 
            attacks.Add((Position.Item1-2,Position.Item2+1)); 
        if (Position.Item1+1 < 8 && Position.Item2+2 < 8 && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2+2] == EnemiesInt) 
            attacks.Add((Position.Item1+1,Position.Item2+2)); 
        if (Position.Item1-1 >= 0 && Position.Item2-2 >= 0 && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2-2] == EnemiesInt) 
            attacks.Add((Position.Item1-1,Position.Item2-2)); 
        if (Position.Item1+2 < 8 && Position.Item2+1 < 8 && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2+1] == EnemiesInt) 
            attacks.Add((Position.Item1+2,Position.Item2+1)); 
        if (Position.Item1-2 >= 0 && Position.Item2-1 >= 0 && BoardScript.BoardMatrix[Position.Item1-2,Position.Item2-1] == EnemiesInt) 
            attacks.Add((Position.Item1-2,Position.Item2-1)); 

        return attacks;
    }
}

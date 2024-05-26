using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : PiecesScript
{
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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
        if (Position is { Item1: <= 6, Item2: >= 2 } && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2-2] == EnemiesInt) 
            attacks.Add((Position.Item1+1,Position.Item2-2)); 
        if (Position is { Item1: >= 1, Item2: <= 5 } && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2+2] == EnemiesInt) 
            attacks.Add((Position.Item1-1,Position.Item2+2));
        if (Position is { Item1: <= 5, Item2: >= 1 } && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2-1] == EnemiesInt) 
            attacks.Add((Position.Item1+2,Position.Item2-1)); 
        if (Position is { Item1: >= 2, Item2: < 7 } && BoardScript.BoardMatrix[Position.Item1-2,Position.Item2+1] == EnemiesInt) 
            attacks.Add((Position.Item1-2,Position.Item2+1)); 
        if (Position is { Item1: < 7, Item2: <= 5 } && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2+2] == EnemiesInt) 
            attacks.Add((Position.Item1+1,Position.Item2+2)); 
        if (Position is { Item1: >= 1, Item2: >= 2 } && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2-2] == EnemiesInt) 
            attacks.Add((Position.Item1-1,Position.Item2-2)); 
        if (Position is { Item1: <= 5, Item2: <= 6 } && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2+1] == EnemiesInt) 
            attacks.Add((Position.Item1+2,Position.Item2+1)); 
        if (Position is { Item1: >= 2, Item2: >= 1 } && BoardScript.BoardMatrix[Position.Item1-2,Position.Item2-1] == EnemiesInt) 
            attacks.Add((Position.Item1-2,Position.Item2-1)); 

        return attacks;
    }

    public override bool IsAttacking(int i, int j)
    {
        return (Math.Abs(Position.Item1 - i) == 2 && Math.Abs(Position.Item2 - j) == 1)
               || (Math.Abs(Position.Item1 - i) == 1 && Math.Abs(Position.Item2 - j) == 2);
    }
    
}

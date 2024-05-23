using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class KingScript : PiecesScript
{
    private GameObject[] _enemies;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    

    public override List<(int, int)> Moves()
    {
        var moves = new List<(int, int)>();
        var attacked = new List<(int, int)>();
        foreach (var e in _enemies)
        {
            if (e.TryGetComponent(out PiecesScript move)) attacked.AddRange(move.Attacks());
        }

        if (Position is { Item1: < 7, Item2: >= 1 } &&
            BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2 - 1] == 0) ;
        if (Position is { Item1: >= 1, Item2: < 7 } && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2+1] == 0) 
            moves.Add((Position.Item1-1,Position.Item2+1));
        if (Position.Item2 >= 1 && BoardScript.BoardMatrix[Position.Item1,Position.Item2-1] == 0) 
            moves.Add((Position.Item1,Position.Item2-1)); 
        if (Position.Item2< 7 && BoardScript.BoardMatrix[Position.Item1,Position.Item2+1] == 0) 
            moves.Add((Position.Item1,Position.Item2+1)); 
        if (Position is { Item1: < 7, Item2: < 7 } && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2+1] == 0) 
            moves.Add((Position.Item1+1,Position.Item2+1)); 
        if (Position is { Item1: >= 1, Item2: >= 1 } && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2-1] == 0) 
            moves.Add((Position.Item1-1,Position.Item2-1)); 
        if (Position is { Item1: < 7, Item2: < 8 } && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2] == 0) 
            moves.Add((Position.Item1+1,Position.Item2)); 
        if (Position is { Item1: >= 1, Item2: >= 0 } && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2] == 0) 
            moves.Add((Position.Item1-1,Position.Item2)); 
        
        
        return moves;
    }

    public override List<(int, int)> Attacks()
    {
        var attacks = new List<(int, int)>();
        return attacks;
    }

    public override bool IsAttacking(int i, int j)
    {
        return (Position.Item1 != i || Position.Item2 != j)
            && Math.Abs(Position.Item1 - i) <= 1 && Math.Abs(Position.Item2 - j) <= 1;
    }
}

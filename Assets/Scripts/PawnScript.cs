using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript: PiecesScript
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
    
    public override List<(int, int)> Moves()
    {
        var moves = new List<(int, int)>();
        if (EnemiesInt == 2)
        {
            if (!(Position.Item1 + 1 < 8 & BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2] == 0)) return moves;
            moves.Add((Position.Item1+1,Position.Item2)); 
            if (Position.Item1 == 1 && BoardScript.BoardMatrix[Position.Item1+2,Position.Item2] == 0) 
                moves.Add((Position.Item1+2,Position.Item2));
        }
        else
        {
            if (!(Position.Item1 - 1 >= 0 & BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2] == 0)) return moves;
            moves.Add((Position.Item1+1,Position.Item2)); 
            if (Position.Item1 == 6 && BoardScript.BoardMatrix[Position.Item1 - 2,Position.Item2] == 0) 
                moves.Add((Position.Item1-2,Position.Item2));
        }
        
        return moves;
    }

    public override List<(int, int)> Attacks()
    {
        var attacks = new List<(int, int)>();
        if (EnemiesInt == 2)
        {
            if (Position is { Item1: >= 1, Item2: <= 7 } && BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2 + 1] == EnemiesInt) 
                attacks.Add((Position.Item1 - 1, Position.Item2 + 1));
            if (Position is { Item1: <= 7, Item2: <= 7 } && BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2 + 1] == EnemiesInt) 
                attacks.Add((Position.Item1 + 1, Position.Item2 + 1));
        }
        else
        {
            if (Position is { Item1: >= 1, Item2: >= 1 } && BoardScript.BoardMatrix[Position.Item1-1,Position.Item2-1] == EnemiesInt) 
                attacks.Add((Position.Item1-1,Position.Item2-1));
            if (Position is { Item1: <= 7, Item2: >= 1 } && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2-1] == EnemiesInt) 
                attacks.Add((Position.Item1+1,Position.Item2-1));
        }
        return attacks;
    }

}

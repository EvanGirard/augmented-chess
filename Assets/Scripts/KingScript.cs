using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KingScript : PiecesScript
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
        var enemies = GameObject.FindGameObjectsWithTag(gameObject.CompareTag("Black") ? "White" : "Black");
        var attacked = new List<(int, int)>();
        foreach (var e in enemies)
        {
            if (TryGetComponent(out PiecesScript move)) attacked.AddRange(move.Attacks());
        }
        if (Position is { Item1: < 7, Item2: >= 1 } && BoardScript.BoardMatrix[Position.Item1+1,Position.Item2-1] == 0) 
            moves.Add((Position.Item1+1,Position.Item2-1)); 
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
    
}

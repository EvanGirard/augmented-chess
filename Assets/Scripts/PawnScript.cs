using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PawnScript: PiecesScript
{
    [SerializeField] private Mesh whiteMesh;
    [SerializeField] private Mesh blackMesh;

    private Mesh _queenMesh;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _queenMesh = EnemiesInt == 2 ? whiteMesh : blackMesh;
        var position = gameObject.transform.position;
        Position = ((int, int))(position.x, position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if ((Position.Item2 != 7 || EnemiesInt != 2) && (Position.Item2 != 0 || EnemiesInt != 1)) return;
        gameObject.AddComponent<QueenScript>();
        gameObject.GetComponent<MeshFilter>().mesh = _queenMesh;
        
        Destroy(this,0.1f);
    }
    
    public override List<(int, int)> Moves()
    {
        var moves = new List<(int, int)>();
        if (EnemiesInt == 2)
        {
            if (Position.Item2 >= 7 || BoardScript.BoardMatrix[Position.Item1, Position.Item2+1] != 0) return moves;
            moves.Add((Position.Item1,Position.Item2+1)); 
            if (Position.Item2 == 1 && BoardScript.BoardMatrix[Position.Item1,Position.Item2+2] == 0) 
                moves.Add((Position.Item1,Position.Item2+2));
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

    public override bool IsAttacking(int i, int j)
    {
        if (EnemiesInt == 2)
        {
            if (j == Position.Item2 + 1 && (i == Position.Item1 + 1 || i == Position.Item1 - 1)) return true;
        }
        else
        {
            if (j == Position.Item2 - 1 && (i == Position.Item1 + 1 || i == Position.Item1 - 1)) return true;
        }

        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopScript : PiecesScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        var position = gameObject.transform.position;
        Position = ((int, int))(position.x, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<(int, int)> Moves()
    {
        var moves = new List<(int, int)>();
        var k = 1;
        while (Position.Item1+k < 8 && Position.Item2+k < 8)
        {
            if (BoardScript.BoardMatrix[Position.Item1+k,Position.Item2+k] != 0) break;
            moves.Add((Position.Item1+k,Position.Item2+k));
            k++;
        }
        k = 1;
        while (Position.Item1-k >= 0 && Position.Item2-k >= 0)
        {
            if (BoardScript.BoardMatrix[Position.Item1-k,Position.Item2-k] != 0) break;
            moves.Add((Position.Item1-k,Position.Item2-k));
            k++;
        }

        k = 1;
        while (Position.Item1-k >= 0 && Position.Item2+k < 8)
        {
            if (BoardScript.BoardMatrix[Position.Item1-k,Position.Item2+k] != 0) break;
            moves.Add((Position.Item1-k,Position.Item2+k));
            k++;
        }
        k = 1;
        while (Position.Item1+k < 8 && Position.Item2-k >= 0)
        {
            if (BoardScript.BoardMatrix[Position.Item1+k,Position.Item2-k] != 0) break;
            moves.Add((Position.Item1+k,Position.Item2-k));
            k++;
        }
        return moves;
    }
    
    public override List<(int, int)> Attacks()
    {
        var attacks = new List<(int, int)>();
        var k = 1;
        while (Position.Item1+k < 8 && Position.Item2+k < 8)
        {
            if (BoardScript.BoardMatrix[Position.Item1 + k, Position.Item2 + k] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1 + k, Position.Item2 + k] == EnemiesInt) 
                attacks.Add((Position.Item1+k,Position.Item2+k));
            break;
        }
        k = 1;
        while (Position.Item1-k >= 0 && Position.Item2-k >= 0)
        {
            if (BoardScript.BoardMatrix[Position.Item1-k,Position.Item2-k] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1 - k, Position.Item2 - k] == EnemiesInt) 
                attacks.Add((Position.Item1-k,Position.Item2-k));
            break;
        }

        k = 1;
        while (Position.Item1-k >= 0 && Position.Item2+k < 8)
        {
            if (BoardScript.BoardMatrix[Position.Item1-k,Position.Item2+k] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1 - k, Position.Item2 + k] == EnemiesInt) 
                attacks.Add((Position.Item1-k,Position.Item2+k));
            break;
        }
        k = 1;
        while (Position.Item1+k < 8 && Position.Item2-k >= 0)
        {
            if (BoardScript.BoardMatrix[Position.Item1 + k, Position.Item2 - k] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1 + k, Position.Item2 - k] == EnemiesInt) 
                attacks.Add((Position.Item1+k,Position.Item2-k));
            break;
        }
        return attacks;
    }

    public override bool IsAttacking(int i, int j)
    {
        if (Math.Abs(Position.Item1 - i) != Math.Abs(Position.Item2 - j) ||
            (i == Position.Item1 && j == Position.Item2)) return false;
        var xDir = (i - Position.Item1) / Math.Abs(Position.Item1 - i);
        var yDir = (j - Position.Item2) / Math.Abs(Position.Item2 - j);
        do
        {
            i -= xDir;
            j -= yDir;
        } while ((i,j) != Position || BoardScript.BoardMatrix[i,j] == 0);
        return (i, j) == Position;
    }
}

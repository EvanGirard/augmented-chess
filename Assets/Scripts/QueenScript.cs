using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenScript : PiecesScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (EnemiesInt == 2) Position = new ValueTuple<int, int>(0, 4);
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
        
        k = 1;
        while (Position.Item1+k<8)
        {
            if (BoardScript.BoardMatrix[Position.Item1+k,Position.Item2] != 0) break;
            moves.Add((Position.Item1+k,Position.Item2));
            k++;
        }
        k = 1;
        while (Position.Item1-k>=0)
        {
            if (BoardScript.BoardMatrix[Position.Item1-k,Position.Item2] != 0) break;
            moves.Add((Position.Item1-k,Position.Item2));
            k++;
        }

        k = 1;
        while (Position.Item2+k<8)
        {
            if (BoardScript.BoardMatrix[Position.Item1,Position.Item2+k] != 0) break;
            moves.Add((Position.Item1,Position.Item2+k));
            k++;
        }
        k = 1;
        while (Position.Item2-k>=0)
        {
            if (BoardScript.BoardMatrix[Position.Item1,Position.Item2-k] != 0) break;
            moves.Add((Position.Item1,Position.Item2-k));
            k++;
        }
        return moves;
    }
    
    public override List<(int, int)> Attacks()
    {
        var attacks = new List<(int,int)>();
        var k = 1;
        while (Position.Item1+k<8)
        {
            if (BoardScript.BoardMatrix[Position.Item1+k,Position.Item2] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1 + k, Position.Item2] == EnemiesInt) 
                attacks.Add((Position.Item1+k,Position.Item2));
            break;
        }
        k = 1;
        while (Position.Item1-k>=0)
        {
            if (BoardScript.BoardMatrix[Position.Item1-k,Position.Item2] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1 - k, Position.Item2] == EnemiesInt) 
                attacks.Add((Position.Item1-k,Position.Item2));
            break;
        }

        k = 1;
        while (Position.Item2+k<8)
        {
            if (BoardScript.BoardMatrix[Position.Item1,Position.Item2+k] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1, Position.Item2 + k] == EnemiesInt) 
                attacks.Add((Position.Item1,Position.Item2+k));
            break;
        }
        k = 1;
        while (Position.Item2-k>=0)
        {
            if (BoardScript.BoardMatrix[Position.Item1,Position.Item2-k] == 0)
            {
                k++;
                continue;
            }
            if (BoardScript.BoardMatrix[Position.Item1, Position.Item2 - k] == EnemiesInt) 
                attacks.Add((Position.Item1,Position.Item2-k));
            break;
        }
        
        k = 1;
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
            if (BoardScript.BoardMatrix[Position.Item1 - k, Position.Item2 + k] == EnemiesInt) 
                attacks.Add((Position.Item1+k,Position.Item2-k));
            break;
        }
        return attacks;
    }
}

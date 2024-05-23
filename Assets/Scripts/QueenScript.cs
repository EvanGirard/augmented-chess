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
        if (TryGetComponent(out PawnScript pawn)) Position = pawn.GetPosition();
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

    public override bool IsAttacking(int i, int j)
    {
        if (i == Position.Item1 && j == Position.Item2) return false;
        int xDir;
        if (Math.Abs(Position.Item1 - i) != 0 && Math.Abs(Position.Item2 - j) == 0)
        {
            xDir = (Position.Item1 - i) / Math.Abs(Position.Item1 - i);
            do
            {
                i += xDir;
            } while (i != Position.Item1 || BoardScript.BoardMatrix[i,j] == 0);

            return i == Position.Item1;
        }

        int yDir;
        if (Math.Abs(Position.Item1 - i) == 0 && Math.Abs(Position.Item2 - j) != 0)
        {
            yDir = (Position.Item2 - j) / Math.Abs(Position.Item2 - j);
            do
            {
                j += yDir;
            } while (j != Position.Item2 || BoardScript.BoardMatrix[i,j] == 0);
        
            return j == Position.Item2;
        }

        if (Math.Abs(Position.Item1 - i) != Math.Abs(Position.Item2 - j)) return false;
        xDir = (Position.Item1 - i) / Math.Abs(Position.Item1 - i);
        yDir = (Position.Item2 - j) / Math.Abs(Position.Item2 - j);
        do
        { 
            i += xDir; 
            j += yDir;
        } while ((i,j) != Position || BoardScript.BoardMatrix[i,j] == 0); 
        return (i, j) == Position;

    }
}

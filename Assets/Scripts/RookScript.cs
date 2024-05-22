using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookScript : PiecesScript
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
        var moves = new List<(int,int)>();
        var k = 1;
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
        return attacks;
    }
    
}

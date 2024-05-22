using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<(int, int)> BishopPossibleMoves(int i, int j)
    {
        var moves = new List<(int, int)>();
        var k = 1;
        while (i+k < 8 && j+k < 8)
        {
            if (BoardScript.BoardMatrix[i+k,j+k] != 0) break;
            moves.Add((i+k,j+k));
            k++;
        }
        k = 1;
        while (i-k >= 0 && j-k >= 0)
        {
            if (BoardScript.BoardMatrix[i-k,j-k] != 0) break;
            moves.Add((i-k,j-k));
            k++;
        }

        k = 1;
        while (i-k >= 0 && j+k < 8)
        {
            if (BoardScript.BoardMatrix[i-k,j+k] != 0) break;
            moves.Add((i-k,j+k));
            k++;
        }
        k = 1;
        while (i+k < 8 && j-k >= 0)
        {
            if (BoardScript.BoardMatrix[i+k,j-k] != 0) break;
            moves.Add((i+k,j-k));
            k++;
        }
        return moves;
    }
    
    public static List<(int, int)> BishopPossibleAttacks(int i, int j)
    {
        var attacks = new List<(int, int)>();
        var k = 1;
        while (i+k < 8 && j+k < 8)
        {
            if (BoardScript.BoardMatrix[i + k, j + k] == 0)
            {
                k++;
                continue;
            }
            attacks.Add((i+k,j+k));
            break;
        }
        k = 1;
        while (i-k >= 0 && j-k >= 0)
        {
            if (BoardScript.BoardMatrix[i-k,j-k] == 0)
            {
                k++;
                continue;
            }
            attacks.Add((i-k,j-k));
            break;
        }

        k = 1;
        while (i-k >= 0 && j+k < 8)
        {
            if (BoardScript.BoardMatrix[i-k,j+k] == 0)
            {
                k++;
                continue;
            }
            attacks.Add((i-k,j+k));
            break;
        }
        k = 1;
        while (i+k < 8 && j-k >= 0)
        {
            if (BoardScript.BoardMatrix[i + k, j - k] == 0)
            {
                k++;
                continue;
            }
            attacks.Add((i+k,j-k));
            break;
        }
        return attacks;
    }
}

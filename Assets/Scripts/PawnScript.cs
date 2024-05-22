using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour
{
    protected static int _startLign;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<(int, int)> PawnPossibleMoves(int i, int j)
    {
        var moves = new List<(int, int)>();
        if (i + 1 < 8 & BoardScript.BoardMatrix[i + 1, j] == 0)
        {
            moves.Add((i+1,j));
            if (i == _startLign && BoardScript.BoardMatrix[i+2,j] == 0) moves.Add((i+2,j));
        }
        
        return moves;
    }
}

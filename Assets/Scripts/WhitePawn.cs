using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePawn: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static List<(int, int)> WhitePawnPossibleMoves(int i, int j)
    {
        var moves = new List<(int, int)>();
        if (!(i + 1 < 8 & BoardScript.BoardMatrix[i + 1, j] == 0)) return moves;
        moves.Add((i+1,j));
        if (i == 1 && BoardScript.BoardMatrix[i+2,j] == 0) moves.Add((i+2,j));

        return moves;
    }

    public static List<(int, int)> WhitePawnPossibleAttacks(int i, int j)
    {
        var attacks = new List<(int, int)>();
        if (i >= 1 && BoardScript.BoardMatrix[i-1,j+1] != 0) attacks.Add((i-1,j+1));
        if (i <= 7 && BoardScript.BoardMatrix[i+1,j+1] != 0) attacks.Add((i+1,j+1));
        return attacks;
    }
    
}

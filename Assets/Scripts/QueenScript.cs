using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<(int, int)> QueenPossibleMoves(int i, int j)
    {
        var moves = RookScript.RookPossibleMoves(i, j);
        moves.AddRange(BishopScript.BishopPossibleMoves(i, j));
        return moves;
    }
    
    public static List<(int, int)> QueenPossibleAttacks(int i, int j)
    {
        var attacks = RookScript.RookPossibleAttacks(i, j);
        attacks.AddRange(BishopScript.BishopPossibleAttacks(i, j));
        return attacks;
    }
}

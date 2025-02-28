using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PiecesScript : MonoBehaviour
{
    protected (int, int) Position;

    public int EnemiesInt;

    protected Game Game;

    protected List<GameObject> whites;
    
    protected List<GameObject> blacks;
    
    protected GameObject[] cases;
    
    protected virtual void Awake()
    {
        Game = GameObject.Find("ImageTarget").GetComponent<Game>();
        whites = Game.GetWhitePieces();
        blacks = Game.GetBlackPieces();
        cases = Game.GetCase();
        var isWhite = whites.Contains(gameObject);
        var pos = 0;
        EnemiesInt = isWhite ? 2 : 1;
        if (isWhite)
        {
            for (var i = 0; i < whites.Count; i++)
            {
                if (whites[i].gameObject.name != name) continue;
                pos = i;
                break;
            }
        }
        else
        {
            for (var i = 0; i < blacks.Count; i++)
            {
                if (blacks[i].gameObject.name != name) continue;
                pos = i + 48 ;
                break;
            }
        }
        var column = Math.DivRem(pos, 8, out var line);
        Position = (line, column);
    }
    
    public abstract List<(int, int)> Moves();
    
    public abstract List<(int, int)> Attacks();

    public abstract bool IsAttacking(int i, int j);
    
    public void SetPosition(int i, int j)
    {
        Position = (i, j);
    }

    public (int, int) GetPosition()
    {
        return Position;
    }
    
}

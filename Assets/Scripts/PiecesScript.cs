using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PiecesScript : MonoBehaviour
{
    protected (int, int) Position;

    protected int EnemiesInt;

    [SerializeField] protected Game game;

    protected GameObject[] whites;
    
    protected GameObject[] blacks;
    
    protected GameObject[] cases;
    
    protected virtual void Start()
    {
        game = GameObject.Find("ImageTarget").GetComponent<Game>();
        whites = game.GetWhitePieces();
        blacks = game.GetBlackPieces();
        cases = game.GetCase();
        var isWhite = whites.Contains(gameObject);
        EnemiesInt = isWhite ? 2 : 1;
        if (isWhite)
        {
            for (var i = 0; i < whites.Length; i++)
            {
                if (whites[i].gameObject.name != name) continue;
                var pos = i;
                break;
            }
        }
        else
        {
            for (var i = 0; i < blacks.Length; i++)
            {
                if (blacks[i].gameObject.name != name) continue;
                var pos = i + 48 ;
                break;
            }
        }
        
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

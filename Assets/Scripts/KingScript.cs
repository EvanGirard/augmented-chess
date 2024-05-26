using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class KingScript : PiecesScript
{
    private List<GameObject> _enemies;
    public bool check;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _enemies = EnemiesInt == 2 ? blacks : whites;
    }


    public override List<(int, int)> Moves()
    {
        var moves = new List<(int, int)>();

        if (Position is { Item1: <= 6, Item2: >= 1 } &&
            BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2 - 1] == 0)
            moves.Add((Position.Item1 + 1, Position.Item2 - 1));

        if (Position is { Item1: >= 1, Item2: <= 6 } &&
            BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2 + 1] == 0)
        {
            moves.Add((Position.Item1 - 1, Position.Item2 + 1));
            
        }

        if (Position.Item2 >= 1 && BoardScript.BoardMatrix[Position.Item1, Position.Item2 - 1] == 0)
            moves.Add((Position.Item1, Position.Item2 - 1));

        if (Position.Item2 <= 6 && BoardScript.BoardMatrix[Position.Item1, Position.Item2 + 1] == 0)
            moves.Add((Position.Item1, Position.Item2 + 1));

        if (Position is { Item1: <= 6, Item2: <= 6 } &&
            BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2 + 1] == 0)
            moves.Add((Position.Item1 + 1, Position.Item2 + 1));

        if (Position is { Item1: >= 1, Item2: >= 1 } &&
            BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2 - 1] == 0)
            moves.Add((Position.Item1 - 1, Position.Item2 - 1));

        if (Position.Item1 <= 6 && BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2] == 0)
            moves.Add((Position.Item1 + 1, Position.Item2));

        if (Position.Item1 >= 1 && BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2] == 0)
            moves.Add((Position.Item1 - 1, Position.Item2));

        var copy = new List<(int, int)>(moves);
        foreach (var elem in copy)
        {
            foreach (var enm in _enemies)
            {
                if (enm.GetComponent<PiecesScript>().IsAttacking(elem.Item1, elem.Item2))
                {
                    moves.Remove(elem);
                    break;
                }
            }
        }
        return moves;
    }

    public override List<(int, int)> Attacks()
    {
        var attacks = new List<(int, int)>();
        if (Position is { Item1: <= 6, Item2: >= 1 } &&
            BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2 - 1] == EnemiesInt)
            attacks.Add((Position.Item1 + 1, Position.Item2 - 1));

        if (Position is { Item1: >= 1, Item2: <= 6 } &&
            BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2 + 1] == EnemiesInt)
            attacks.Add((Position.Item1 - 1, Position.Item2 + 1));

        if (Position.Item2 >= 1 &&
            BoardScript.BoardMatrix[Position.Item1, Position.Item2 - 1] == EnemiesInt)
            attacks.Add((Position.Item1, Position.Item2 - 1));

        if (Position.Item2 <= 6 &&
            BoardScript.BoardMatrix[Position.Item1, Position.Item2 + 1] == EnemiesInt)
            attacks.Add((Position.Item1, Position.Item2 + 1));

        if (Position is { Item1: <= 6, Item2: <= 6 } &&
            BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2 + 1] == EnemiesInt)
            attacks.Add((Position.Item1 + 1, Position.Item2 + 1));

        if (Position is { Item1: >= 1, Item2: >= 1 } &&
            BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2 - 1] == EnemiesInt)
            attacks.Add((Position.Item1 - 1, Position.Item2 - 1));

        if (Position.Item1 <= 6 &&
            BoardScript.BoardMatrix[Position.Item1 + 1, Position.Item2] == EnemiesInt)
            attacks.Add((Position.Item1 + 1, Position.Item2));

        if (Position.Item1 >= 1 &&
            BoardScript.BoardMatrix[Position.Item1 - 1, Position.Item2] == EnemiesInt)
            attacks.Add((Position.Item1 - 1, Position.Item2));

        var copy = new List<(int, int)>(attacks);
        foreach (var elem in copy)
        {
            foreach (var enm in _enemies)
            {
                if (enm.GetComponent<PiecesScript>().IsAttacking(elem.Item1, elem.Item2))
                {
                    attacks.Remove(elem);
                    break;
                }
            }
        }

        return attacks;
    }

public override bool IsAttacking(int i, int j)
    {
        return (Position.Item1 != i || Position.Item2 != j)
            && Math.Abs(Position.Item1 - i) <= 1 && Math.Abs(Position.Item2 - j) <= 1;
    }
    
    public void SetEnemies(List<GameObject> enemies)
    {
        _enemies = enemies;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PiecesScript : MonoBehaviour
{
    protected (int, int) Position;

    protected int EnemiesInt;

    protected virtual void Start()
    {
        EnemiesInt = gameObject.CompareTag("White") ? 2 : 1;
    }
    
    public abstract List<(int, int)> Moves();
    
    public abstract List<(int, int)> Attacks();

    public void SetPosition(int i, int j)
    {
        Position = (i, j);
    }

    public (int, int) GetPosition()
    {
        return Position;
    }
}

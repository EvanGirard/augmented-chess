using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static int[,] BoardMatrix;
    public GameObject whitePawnPrefab;
    public GameObject blackPawnPrefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        BoardMatrix = new int[8, 8];
        Debug.Log(BoardMatrix);
        for (var i = 0; i < 8; i++)
        {
            BoardMatrix[i, 0] = 1;
            BoardMatrix[i, 7] = 1;
        }
        for (var i = 0; i < 8; i++)
        {
            if (BoardMatrix[i, 0] == 1)
            {
                Instantiate(whitePawnPrefab, new Vector3(i*1.5f -5.25f, 0, -5.25f)*0.01888f, Quaternion.identity, transform); 
                Debug.Log("Instance created for i and j:" + i + "and 0");
            }

            if (BoardMatrix[i, 7] == 1)
            {
                Instantiate(blackPawnPrefab, new Vector3(i*1.5f -5.25f, 0, 5.25f)*0.01888f, Quaternion.identity, transform); 
                Debug.Log("Instance created for i and j: " + i + "and 7");
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            var attacks = QueenScript.QueenPossibleAttacks(2, 5);
            foreach (var t in attacks)
            {
                Debug.Log(t);
            }
        } 
    }
}

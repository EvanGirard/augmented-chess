using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static int[,] BoardMatrix;
    public GameObject whitePawnPrefab;
    public GameObject blackPawnPrefab;
    private Camera _camera;
    
    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        
        BoardMatrix = new int[8, 8];
        Debug.Log(BoardMatrix);
        for (var i = 0; i < 8; i++)
        {
            BoardMatrix[i, 0] = 1;
            BoardMatrix[i, 1] = 1;
            BoardMatrix[i, 6] = 2;
            BoardMatrix[i, 7] = 2;
        }

        /*
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
            
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;
        Debug.Log(hit.collider.GameObject().name);
        if (!hit.collider.gameObject.TryGetComponent(out PiecesScript p)) return;
        var move = p.Moves();
        foreach (var v in move)
        {
            Debug.Log(v.Item1 +" et "+v.Item2);
        }
    }

}

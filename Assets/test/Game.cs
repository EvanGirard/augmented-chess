using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

enum  PlayerTurn    
{
    White,
    Black
}

public class Game : MonoBehaviour {
    
    private GameObject selectedChessPiece = null;
    private GameObject imageTarget;
    private PlayerTurn playerTurn;
    
    [SerializeField] private GameObject[] BlackPieces;
    [SerializeField] private GameObject[] WhitePieces;
    [SerializeField] private GameObject[] Case;

    void Start()
    {
        imageTarget = GameObject.Find("ImageTarget");
        playerTurn = PlayerTurn.White;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if(playerTurn == PlayerTurn.White)
                {
                    if (WhitePieces.Contains(hit.transform.gameObject))
                    {
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece, Color.white);
                        }
                        selectedChessPiece = hit.transform.gameObject;
                        ChangeColor(selectedChessPiece, Color.yellow);
                    }
                    
                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
                        selectedChessPiece.transform.position = hit.point;
                        ChangeColor(selectedChessPiece, Color.white);
                        selectedChessPiece = null;
                        playerTurn = PlayerTurn.Black;
                    }
                }
                
                else if (playerTurn == PlayerTurn.Black)
                {
                    if (BlackPieces.Contains(hit.transform.gameObject))
                    {
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece, Color.black);
                        }
                        selectedChessPiece = hit.transform.gameObject;
                        ChangeColor(selectedChessPiece, Color.yellow);
                    }
                    
                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
                        selectedChessPiece.transform.position = hit.point;
                        ChangeColor(selectedChessPiece, Color.black);
                        selectedChessPiece = null;
                        playerTurn = PlayerTurn.White;
                    }
                }
            }
        }
    }

    void ChangeColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
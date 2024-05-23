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
    
    private PiecesScript selectedChessPiece = null;
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
                            ChangeColor(selectedChessPiece.gameObject, Color.white);
                        }
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();
                        ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                    }
                    
                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
                        //if(hit.collider.gameObject.name = selectedChessPiece.position) return;
                        selectedChessPiece.transform.position = hit.point;
                        ChangeColor(selectedChessPiece.gameObject, Color.white);
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
                            ChangeColor(selectedChessPiece.gameObject, Color.black);
                        }
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();
                        ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                    }
                    
                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
                        //if(hit.collider.gameObject.name = selectedChessPiece.position) return;
                        selectedChessPiece.transform.position = hit.point;
                        ChangeColor(selectedChessPiece.gameObject, Color.black);
                        selectedChessPiece = null;
                        playerTurn = PlayerTurn.White;
                    }
                }
            }
        }
    }
    
    public GameObject[] GetBlackPieces()
    {
        return BlackPieces;
    }
    
    public GameObject[] GetWhitePieces()
    {
        return WhitePieces;
    }
    
    public GameObject[] GetCase()
    {
        return Case;
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
using System;
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
        Debug.Log(BlackPieces[0]);
        imageTarget = gameObject;
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
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();
                        
                        List<(int,int)> moves = selectedChessPiece.Moves();
                        List<(int,int)> attacks = selectedChessPiece.Attacks();
                        
                        foreach (var elem in moves)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green);
                        }

                        foreach (var elem in attacks)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red);
                        }
                        
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.white);
                        }
                        
                        
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();
                        ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                    }
                    
                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
                        // Ne pas faire le move si le roi est toujours en échecs
                        PiecesScript king = null;
                        
                        foreach (var elem in WhitePieces)
                        {
                            if (elem.name == "King")
                            {
                                king = elem.GetComponent<PiecesScript>();
                                break;
                            }
                        }
                        
                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1, selectedChessPiece.GetPosition().Item2] = 0;
                        
                        foreach (var elem in BlackPieces)
                        {
                            Debug.Log(selectedChessPiece.GetPosition());
                            if (elem.GetComponent<PiecesScript>().IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2))
                            {
                                BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1, selectedChessPiece.GetPosition().Item2] = 1;
                                return;
                            }
                        }
                        
                        if (hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.green || hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.red) return;
                        
                        int caseIndex = 0;
                        
                        for (int i = 0; i < Case.Length; i++)
                        {
                            if (Case[i] == hit.transform.gameObject)
                            {
                                caseIndex = i;
                                break;
                            }
                        }
                        
                        var column = Math.DivRem(caseIndex, 8, out int line);
                        selectedChessPiece.SetPosition(line, column);
                        BoardScript.BoardMatrix[line, column] = 1;

                        foreach (var elem in BlackPieces)
                        {
                            if (selectedChessPiece.GetPosition() == elem.GetComponent<PiecesScript>().GetPosition())
                            {
                                BlackPieces.ToList().Remove(elem);
                                Destroy(elem.gameObject);
                                break;
                            }
                        }
                        
                        
                        ChangeColor(selectedChessPiece.gameObject, Color.white);
                        selectedChessPiece.transform.position = hit.transform.gameObject.transform.position;
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
                        //Permet de pas bouger la piece sur elle meme
                        //if(hit.collider.gameObject.transform.position = selectedChessPiece.position) return;
                        selectedChessPiece.transform.position = hit.transform.gameObject.transform.position;
                        ChangeColor(selectedChessPiece.gameObject, Color.black);
                        selectedChessPiece = null;
                        playerTurn = PlayerTurn.White;
                    }
                }
            }
        }
    }
    
    //lors du déplacement modifier la matrice dans boardScript 1=W 2=B 0=empty
    
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
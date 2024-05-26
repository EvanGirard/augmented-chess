using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    
    [SerializeField] private List<GameObject> BlackPieces;
    [SerializeField] private List<GameObject> WhitePieces;
    [SerializeField] private GameObject[] Case;

    void Start()
    {
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
                if (playerTurn == PlayerTurn.White)
                {
                    KingScript king = null;

                    //On récupère le roi blanc
                    foreach (var elem in WhitePieces)
                    {
                        if (elem.name != "White King E") continue;
                        king = elem.GetComponent<KingScript>();
                        break;
                    }

                    if (WhitePieces.Contains(hit.transform.gameObject))
                    {
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject,Color.white);
                            foreach (var elem in selectedChessPiece.Moves())
                            {
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                            }
                        }

                        
                        
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();

                        var moves = selectedChessPiece.Moves();
                        var attacks = selectedChessPiece.Attacks();



                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 0;

                        foreach (var elem in moves)
                        {
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green);

                            foreach (var bpieces in BlackPieces)
                            {
                                if (!bpieces.GetComponent<PiecesScript>()
                                        .IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2)) continue;

                                Debug.Log("échecs");
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                                break;
                            }

                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 0;
                        }

                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 1;

                        foreach (var elem in attacks)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red);
                        }

                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                        }




                    }

                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
                        // Ne pas faire le move si le roi est toujours en échecs

                        //On ne se déplace pas sur les case verte et rouge
                        if (hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.green &&
                            hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.red) return;

                        var caseIndex = 0;

                        for (var i = 0; i < Case.Length; i++)
                        {
                            if (Case[i] != hit.transform.gameObject) continue;
                            caseIndex = i;
                            break;
                        }


                        var moves = selectedChessPiece.Moves();
                        foreach (var elem in moves)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                        }

                        var attacks = selectedChessPiece.Attacks();
                        foreach (var elem in attacks)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red, 0f);
                        }

                        var column = Math.DivRem(caseIndex, 8, out var line);
                        selectedChessPiece.SetPosition(line, column);
                        BoardScript.BoardMatrix[line, column] = 1;

                        //Prise de pièce
                        foreach (var elem in BlackPieces)
                        {
                            if (selectedChessPiece.GetPosition() != elem.GetComponent<PiecesScript>().GetPosition())
                                continue;
                            BlackPieces.Remove(elem);
                            Destroy(elem.gameObject);
                            king.SetEnemies(BlackPieces);
                            break;
                        }


                        ChangeColor(selectedChessPiece.gameObject, Color.white);

                        selectedChessPiece.transform.position = hit.transform.gameObject.transform.position;
                        selectedChessPiece = null;

                        playerTurn = PlayerTurn.Black;
                    }
                }









                // TOUR DES NOIRS
                else if (playerTurn == PlayerTurn.Black)
                {
                    KingScript king = null;

                    //On récupère le roi noir
                    foreach (var elem in BlackPieces)
                    {
                        if (elem.name != "Black King E") continue;
                        king = elem.GetComponent<KingScript>();
                        break;
                    }

                    if (BlackPieces.Contains(hit.transform.gameObject))
                    {
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject,Color.white);
                            foreach (var elem in selectedChessPiece.Moves())
                            {
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                            }
                        }
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();

                        var moves = selectedChessPiece.Moves();
                        var attacks = selectedChessPiece.Attacks();
                        

                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 0;

                        foreach (var elem in moves)
                        {
                            Debug.Log(elem);
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green);

                            foreach (var bpieces in WhitePieces)
                            {
                                if (!bpieces.GetComponent<PiecesScript>()
                                        .IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2)) continue;

                                Debug.Log("échec");    
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                                break;
                            }

                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 0;
                        }

                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 2;

                        foreach (var elem in attacks)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red);
                        }

                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                        }


                    }

                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {

                        //On ne se déplace pas sur les case verte et rouge
                        if (hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.green &&
                            hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.red) return;

                        var caseIndex = 0;

                        for (var i = 0; i < Case.Length; i++)
                        {
                            if (Case[i] != hit.transform.gameObject) continue;
                            caseIndex = i;
                            break;
                        }


                        var moves = selectedChessPiece.Moves();
                        foreach (var elem in moves)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                        }

                        var attacks = selectedChessPiece.Attacks();
                        foreach (var elem in attacks)
                        {
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red, 0f);
                        }

                        var column = Math.DivRem(caseIndex, 8, out var line);
                        selectedChessPiece.SetPosition(line, column);
                        BoardScript.BoardMatrix[line, column] = 2;

                        //Prise de pièce
                        foreach (var elem in WhitePieces)
                        {
                            if (selectedChessPiece.GetPosition() != elem.GetComponent<PiecesScript>().GetPosition())
                                continue;
                            WhitePieces.Remove(elem);
                            Destroy(elem.gameObject);
                            king.SetEnemies(WhitePieces);
                            break;
                        }


                        ChangeColor(selectedChessPiece.gameObject, Color.black);

                        selectedChessPiece.transform.position = hit.transform.gameObject.transform.position;
                        selectedChessPiece = null;
                        playerTurn = PlayerTurn.White;
                    }
                }
            }
        }
    }

    //lors du déplacement modifier la matrice dans boardScript 1=W 2=B 0=empty
    
    public List<GameObject> GetBlackPieces()
    {
        return BlackPieces;
    }
    
    public List<GameObject> GetWhitePieces()
    {
        return WhitePieces;
    }
    
    public GameObject[] GetCase()
    {
        return Case;
    }
    

    void ChangeColor(GameObject obj, Color color , float alpha = 1.0f)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Color rendererColor = renderer.material.color;
        if (renderer != null)
        {
            rendererColor = color;
            rendererColor.a = alpha;
            renderer.material.color = rendererColor;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

enum PlayerTurn    
{
    White,
    Black
}

public class Game : MonoBehaviour {
    
    private PiecesScript selectedChessPiece = null;
    private GameObject imageTarget;
    private PlayerTurn playerTurn;
    private KingScript WhiteKing;
    private KingScript BlackKing;
    private bool promote;
    
    [SerializeField] private GameObject CanvasPromote;
    [SerializeField] private List<GameObject> BlackPieces;
    [SerializeField] private List<GameObject> WhitePieces;
    [SerializeField] private GameObject[] Case;
    [SerializeField] private GameObject PromoteBlackBishop;
    [SerializeField] private GameObject PromoteBlackKnight;
    [SerializeField] private GameObject PromoteBlackQueen;
    [SerializeField] private GameObject PromoteBlackRook;
    [SerializeField] private GameObject PromoteWhiteBishop;
    [SerializeField] private GameObject PromoteWhiteKnight;
    [SerializeField] private GameObject PromoteWhiteQueen;
    [SerializeField] private GameObject PromoteWhiteRook;

    void Start()
    {
        imageTarget = gameObject;
        playerTurn = PlayerTurn.White;
        
        foreach (var elem in WhitePieces)
        {
            if (elem.name != "White King E") continue;
            WhiteKing = elem.GetComponent<KingScript>();
            break;
        }
        
        foreach (var elem in BlackPieces)
        {
            if (elem.name != "Black King E") continue;
            BlackKing = elem.GetComponent<KingScript>();
            break;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (playerTurn == PlayerTurn.White)
                {
                    if (WhitePieces.Contains(hit.transform.gameObject))
                    {
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.white);
                            foreach (var elem in selectedChessPiece.Moves())
                            {
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                            }
                            
                            foreach (var elem in selectedChessPiece.Attacks())
                            {
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red, 0f);
                            }
                        }
                        
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();

                        var moves = selectedChessPiece.Moves();
                        var attacks = selectedChessPiece.Attacks();

                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 0;

                        var pos = WhiteKing.GetPosition();
                        foreach (var elem in moves)
                        {
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green);
                            if (selectedChessPiece == WhiteKing) WhiteKing.SetPosition(elem.Item1, elem.Item2);

                            foreach (var bpieces in BlackPieces)
                            {
                                if (!bpieces.GetComponent<PiecesScript>()
                                        .IsAttacking(WhiteKing.GetPosition().Item1, WhiteKing.GetPosition().Item2)) continue;

                                WhiteKing.check = true;
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                                break;
                            }

                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 0;
                        }

                        foreach (var elem in attacks)
                        {
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red);
                            if (selectedChessPiece == WhiteKing) WhiteKing.SetPosition(elem.Item1, elem.Item2);
                            
                            foreach (var bpieces in BlackPieces)
                            {
                                if (!bpieces.GetComponent<PiecesScript>()
                                        .IsAttacking(WhiteKing.GetPosition().Item1, WhiteKing.GetPosition().Item2) || bpieces.GetComponent<PiecesScript>().GetPosition() == elem) continue;
                                WhiteKing.check = true;
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red, 0f);
                                break;
                            }
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;    
                        }

                        WhiteKing.SetPosition(pos.Item1, pos.Item2);
                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 1;
                        
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                        }
                    }

                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
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
                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1, selectedChessPiece.GetPosition().Item2] = 0;
                        selectedChessPiece.SetPosition(line, column);
                        BoardScript.BoardMatrix[line, column] = 1;

                        foreach (var elem in BlackPieces)
                        {
                            if (selectedChessPiece.GetPosition() != elem.GetComponent<PiecesScript>().GetPosition())
                                continue;
                            BlackPieces.Remove(elem);
                            Destroy(elem.gameObject);
                            WhiteKing.SetEnemies(BlackPieces);
                            break;
                        }

                        CheckMate(BlackKing); 
                        ChangeColor(selectedChessPiece.gameObject, Color.white);

                        selectedChessPiece.transform.position = hit.transform.gameObject.transform.position;
                        if (selectedChessPiece.gameObject.GetComponent<PawnScript>() && selectedChessPiece.GetPosition().Item2 == 7)
                        {
                            CanvasPromote.SetActive(true);
                            promote = true;
                            StartCoroutine(Wait(PlayerTurn.Black));
                        }

                        if (promote == false)
                        {
                            playerTurn = PlayerTurn.Black;
                        }
                        
                        selectedChessPiece = null;
                        WhiteKing.check = false;
                    }
                }
                else if (playerTurn == PlayerTurn.Black)
                {
                    if (BlackPieces.Contains(hit.transform.gameObject))
                    {
                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.black);
                            foreach (var elem in selectedChessPiece.Moves())
                            {
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                            }
                            
                            foreach (var elem in selectedChessPiece.Attacks())
                            {
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red, 0f);
                            }
                        }
                        selectedChessPiece = hit.transform.gameObject.GetComponent<PiecesScript>();

                        var moves = selectedChessPiece.Moves();
                        var attacks = selectedChessPiece.Attacks();


                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 0;
                        
                        var pos = BlackKing.GetPosition();
                        foreach (var elem in moves)
                        {
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green);
                            if (selectedChessPiece == BlackKing) BlackKing.SetPosition(elem.Item1, elem.Item2);
                            foreach (var bpieces in WhitePieces)
                            {
                                if (!bpieces.GetComponent<PiecesScript>()
                                        .IsAttacking(BlackKing.GetPosition().Item1, BlackKing.GetPosition().Item2)) continue;

                                BlackKing.check = true;  
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.green, 0f);
                                break;
                            }

                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 0;
                        }
                        
                        foreach (var elem in attacks)
                        {
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;
                            ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red);
                            if (selectedChessPiece == BlackKing) BlackKing.SetPosition(elem.Item1, elem.Item2);
                            
                            foreach (var wpieces in WhitePieces)
                            {
                                if (!wpieces.GetComponent<PiecesScript>()
                                        .IsAttacking(BlackKing.GetPosition().Item1, BlackKing.GetPosition().Item2) || wpieces.GetComponent<PiecesScript>().GetPosition() == elem) continue;
                                
                                BlackKing.check = true;
                                ChangeColor(Case[elem.Item1 + elem.Item2 * 8], Color.red, 0f);
                                break;
                            }
                            BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;    
                        }
                        
                        BlackKing.SetPosition(pos.Item1, pos.Item2);
                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1,
                            selectedChessPiece.GetPosition().Item2] = 2;

                        if (selectedChessPiece != null)
                        {
                            ChangeColor(selectedChessPiece.gameObject, Color.yellow);
                        }
                    }

                    if (Case.Contains(hit.collider.gameObject) && selectedChessPiece != null)
                    {
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
                        BoardScript.BoardMatrix[selectedChessPiece.GetPosition().Item1, selectedChessPiece.GetPosition().Item2] = 0;
                        selectedChessPiece.SetPosition(line, column);
                        BoardScript.BoardMatrix[line, column] = 2;

                        foreach (var elem in WhitePieces)
                        {
                            if (selectedChessPiece.GetPosition() != elem.GetComponent<PiecesScript>().GetPosition())
                                continue;
                            WhitePieces.Remove(elem);
                            Destroy(elem.gameObject);
                            BlackKing.SetEnemies(WhitePieces);
                            break;
                        }

                        CheckMate(WhiteKing);

                        ChangeColor(selectedChessPiece.gameObject, Color.black);

                        selectedChessPiece.transform.position = hit.transform.gameObject.transform.position;
                        
                        if (selectedChessPiece.gameObject.GetComponent<PawnScript>() && selectedChessPiece.GetPosition().Item2 == 0)
                        {
                            CanvasPromote.SetActive(true);
                            promote = true;
                            StartCoroutine(Wait(PlayerTurn.White));
                        }

                        if (promote == false)
                        {
                            playerTurn = PlayerTurn.White;
                        }
                        
                        selectedChessPiece = null;
                        BlackKing.check = false;
                    }
                }
            }
        }
    }

    IEnumerator Wait(PlayerTurn turn)
    {
        yield return new WaitUntil(() => promote == false);
        playerTurn = turn;
    }
    
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
    
    bool CheckMate(KingScript king)
    {
        if (king.EnemiesInt == 1)
        {
            if (king.Moves().Count == 0 && king.Attacks().Count == 0)
            {
                foreach (var bpieces in BlackPieces)
                {
                    BoardScript.BoardMatrix[bpieces.GetComponent<PiecesScript>().GetPosition().Item1,
                        bpieces.GetComponent<PiecesScript>().GetPosition().Item2] = 0;
                    foreach (var elem in bpieces.GetComponent<PiecesScript>().Moves())
                    {
                        int valMove = 0;
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;
                        foreach (var wpiece in WhitePieces)
                        {
                            if (wpiece.GetComponent<PiecesScript>()
                                .IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2))
                            {
                                valMove += 1;
                                break;
                            }
                        }
                        
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 0;
                        BoardScript.BoardMatrix[bpieces.GetComponent<PiecesScript>().GetPosition().Item1,
                            bpieces.GetComponent<PiecesScript>().GetPosition().Item2] = 2;
                        if (valMove == 0)
                        {
                            return false;
                        }
                    }
                    
                    foreach (var elem in bpieces.GetComponent<PiecesScript>().Attacks())
                    {
                        int valAtt = 0;
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;
                        foreach (var wpiece in WhitePieces)
                        {
                            if (wpiece.GetComponent<PiecesScript>().GetPosition() == elem) continue;
                            if (wpiece.GetComponent<PiecesScript>()
                                .IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2))
                            {
                                valAtt += 1;
                                break;
                            }
                        }
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;
                        BoardScript.BoardMatrix[bpieces.GetComponent<PiecesScript>().GetPosition().Item1,
                            bpieces.GetComponent<PiecesScript>().GetPosition().Item2] = 2;
                        
                        if (valAtt == 0)
                        {
                            return false;
                        }
                    }
                }
                Debug.Log("MAT");
                return true;
            }
        }
        else
        {
            if (king.Moves().Count == 0 && king.Attacks().Count == 0)
            {
                foreach (var wpieces in WhitePieces)
                {
                    BoardScript.BoardMatrix[wpieces.GetComponent<PiecesScript>().GetPosition().Item1,
                        wpieces.GetComponent<PiecesScript>().GetPosition().Item2] = 0;
                    foreach (var elem in wpieces.GetComponent<PiecesScript>().Moves())
                    {
                        int valMove = 0;
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;
                        foreach (var bpiece in BlackPieces)
                        {
                            if (bpiece.GetComponent<PiecesScript>()
                                .IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2))
                            {
                                valMove += 1;
                                break;
                            }
                        }
                        
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 0;
                        BoardScript.BoardMatrix[wpieces.GetComponent<PiecesScript>().GetPosition().Item1,
                            wpieces.GetComponent<PiecesScript>().GetPosition().Item2] = 1;
                        if (valMove == 0)
                        {
                            return false;
                        }
                    }
                    
                    foreach (var elem in wpieces.GetComponent<PiecesScript>().Attacks())
                    {
                        int valAtt = 0;
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 1;
                        foreach (var bpiece in BlackPieces)
                        {
                            if (bpiece.GetComponent<PiecesScript>().GetPosition() == elem) continue;
                            if (bpiece.GetComponent<PiecesScript>()
                                .IsAttacking(king.GetPosition().Item1, king.GetPosition().Item2))
                            {
                                valAtt += 1;
                                break;
                            }
                        }
                        BoardScript.BoardMatrix[elem.Item1, elem.Item2] = 2;
                        BoardScript.BoardMatrix[wpieces.GetComponent<PiecesScript>().GetPosition().Item1,
                            wpieces.GetComponent<PiecesScript>().GetPosition().Item2] = 1;
                        
                        if (valAtt == 0)
                        {
                            return false;
                        }
                    }
                }
                Debug.Log("MAT");
                return true;
            }
        }
        return false;
    }
    
    public void Promote(int newPiece)
    {
        if (PlayerTurn.White == playerTurn)
        {
            foreach (var wpieces in WhitePieces)
            {
                if (wpieces.TryGetComponent<PawnScript>(out var wpawn))
                {
                    if (wpawn.GetPosition().Item2 == 7)
                    {
                        var pos = wpawn.GetPosition();
                        switch(newPiece)
                        {
                            case 1:
                                GameObject promotedB = Instantiate(PromoteWhiteBishop, wpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(8));
                                WhitePieces.Add(promotedB.gameObject);
                                promotedB.SetActive(true);
                                promotedB.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                            case 2:
                                GameObject promotedK = Instantiate(PromoteWhiteKnight, wpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(8));
                                WhitePieces.Add(promotedK.gameObject);
                                promotedK.SetActive(true);
                                promotedK.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                            case 3:
                                GameObject promotedQ = Instantiate(PromoteWhiteQueen, wpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(8));
                                WhitePieces.Add(promotedQ.gameObject);
                                promotedQ.SetActive(true);
                                promotedQ.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                            case 4:
                                GameObject promotedR = Instantiate(PromoteWhiteRook, wpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(8));
                                WhitePieces.Add(promotedR.gameObject);
                                promotedR.SetActive(true);
                                promotedR.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                        }
                        WhitePieces.Remove(wpawn.gameObject);
                        Destroy(wpawn.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (var bpieces in BlackPieces)
            {
                if (bpieces.TryGetComponent<PawnScript>(out var bpawn))
                {
                    if (bpawn.GetPosition().Item2 == 0)
                    {
                        var pos = bpawn.GetPosition();
                        switch(newPiece)
                        {
                            case 1:
                                GameObject promotedB = Instantiate(PromoteBlackBishop, bpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(9));
                                BlackPieces.Add(promotedB.gameObject);
                                promotedB.SetActive(true);
                                promotedB.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                            case 2:
                                GameObject promotedK = Instantiate(PromoteBlackKnight, bpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(9));
                                BlackPieces.Add(promotedK.gameObject);
                                promotedK.SetActive(true);
                                promotedK.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                            case 3:
                                GameObject promotedQ = Instantiate(PromoteBlackQueen, bpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(9));
                                BlackPieces.Add(promotedQ.gameObject);
                                promotedQ.SetActive(true);
                                promotedQ.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                            case 4:
                                GameObject promotedR = Instantiate(PromoteBlackRook, bpawn.gameObject.transform.position, Quaternion.identity,
                                    gameObject.transform.GetChild(0).transform.GetChild(9));
                                BlackPieces.Add(promotedR.gameObject);
                                promotedR.SetActive(true);
                                promotedR.GetComponent<PiecesScript>().SetPosition(pos.Item1, pos.Item2);
                                break;
                        }
                        BlackPieces.Remove(bpawn.gameObject);
                        Destroy(bpawn.gameObject);
                        break;
                    }
                }
            }
        }
        
        CanvasPromote.SetActive(false);
        promote = false;
    }
}

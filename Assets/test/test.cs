using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class test : MonoBehaviour {
    
    // Référence à l'objet d'échecs sélectionné
    private GameObject selectedChessPiece = null;
    // Référence au plan de l'image cible
    private GameObject imageTarget;
    
    [SerializeField] private GameObject[] BlackPieces;
    [SerializeField] private GameObject[] WhitePieces;
    [SerializeField] private GameObject[] Case;

    void Start()
    {
        // Trouver l'image cible dans la scène (assurez-vous que l'image cible est nommée correctement dans Unity)
        imageTarget = GameObject.Find("ImageTarget");
        // Trouver le cube dans la scène (assurez-vous que le cube est nommé correctement dans Unity)
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Vérifier si le rayon a touché une pièce d'échecs
                if (hit.transform.CompareTag("ChessPiece"))
                {
                    // Si une pièce est déjà sélectionnée, remettre sa couleur d'origine
                    if (selectedChessPiece != null)
                    {
                        ResetColor(selectedChessPiece);
                    }
                    
                    // Sélectionner la nouvelle pièce d'échecs et changer sa couleur
                    selectedChessPiece = hit.transform.gameObject;
                    ChangeColor(selectedChessPiece, Color.yellow); // Change la couleur de la pièce sélectionnée à jaune
                }
                /*else if (hit.collider.gameObject == cube && selectedChessPiece != null)
                {
                    // Placer la pièce sélectionnée sur le cube
                    selectedChessPiece.transform.position = hit.point;
                    // Remettre la couleur d'origine de la pièce après l'avoir déplacée
                    ResetColor(selectedChessPiece);
                    selectedChessPiece = null;
                }*/
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

    void ResetColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Remettre la couleur d'origine (blanc dans cet exemple)
            renderer.material.color = Color.white;
        }
    }
}
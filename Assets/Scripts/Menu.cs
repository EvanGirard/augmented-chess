using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject controls;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        controls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SamplesResources/Scenes/0-Main");
    }

    public void ToControls()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void ToMenu()
    {
        menu.SetActive(true);
        controls.SetActive(false);
    }
    
    public void QuitApplication()
    {
        Application.Quit();
    }
}

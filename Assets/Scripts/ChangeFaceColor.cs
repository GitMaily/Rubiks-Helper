using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ChangeFaceColor : MonoBehaviour, IEventSystemHandler
{
    
    public Button[] colorButtons; // Tableau de chaque bouton de couleur
    private Button lastClickedButton; // Dernier bouton cliqué

    private void Start()
    {
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i; // Variable temporaire pour stocker la valeur de i
            colorButtons[index].onClick.AddListener(() => OnColorButtonClick(colorButtons[index]));
        }
    }

    private void OnColorButtonClick(Button clickedButton)
    {
        if (clickedButton == lastClickedButton) // Si on clique deux fois sur le même bouton
        {
            lastClickedButton = null; // Réinitialiser le dernier bouton cliqué
        }
        else // Si un nouveau bouton est cliqué
        {
            lastClickedButton = clickedButton; // Mettre à jour le dernier bouton cliqué
        }
    }

    


    private void Update()
    {
        if (lastClickedButton != null)
        {
            // Si la souris clique sur un GameObject
            if (Input.GetMouseButtonDown(0))
            {
                // Récupérer le rayon de la caméra à la position de la souris
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Si le rayon touche un GameObject
                if (Physics.Raycast(ray, out hit))
                {
                    // Vérifier si le GameObject est une face de Rubik's Cube
                    if (hit.transform.gameObject.CompareTag("CubeFace"))
                    {
                        // Changer la couleur de la face
                        Renderer renderer = hit.transform.gameObject.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.color = lastClickedButton.image.color;
                        }
                        else
                        {
                            Debug.LogWarning("Pas de Renderer pour " + hit.transform.gameObject.name);
                        }
                    }
                }
            }
        }
    }

    public void BoutonRetour()
    {
        SceneManager.LoadScene("MenuUI");
    }
    
    /*public Button[] buttons; // Tableau de chaque bouton de couleur

    private Button lastClickedButton; // Dernier bouton cliqué

    public List<GameObject> faceChangeObjects; // Liste de gameObjects à changer de couleur
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Variable temporaire pour stocker la valeur de i
            buttons[index].onClick.AddListener(() => OnButtonClick(buttons[index]));
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (clickedButton == lastClickedButton) // Si on clique deux fois sur le même bouton
        {
            // Active tous les boutons
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            }

            lastClickedButton = null; // Réinitialiser le dernier bouton cliqué
        }
        else // Si un nouveau bouton est cliqué
        {
            // Désactive tous les autres boutons
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != clickedButton)
                {
                    buttons[i].interactable = false;
                }
            }

            lastClickedButton = clickedButton; // Mettre à jour le dernier bouton cliqué
            
            switch (clickedButton.name)
            {
                case "Rouge":
                    ChangeFacesColor(Color.red);
                    break;
                case "Bleu":
                    ChangeFacesColor(Color.blue);
                    break;
                case "Blanc":
                    ChangeFacesColor(Color.white);
                    break;
                case "Vert":
                    ChangeFacesColor(Color.green);
                    break;
                case "Jaune":
                    ChangeFacesColor(Color.yellow);
                    break;
                case "Orange":
                    ChangeFacesColor(new Color(1.0f, 0.5f, 0.0f));
                    break;
                default:
                    break;
            }
        }
    }

    private void ChangeFacesColor(Color color)
    {
        foreach (GameObject face in faceChangeObjects)
        {
            Renderer renderer = face.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
            else
            {
                Debug.LogWarning("ChangeFaceColor : Renderer missing from " + face.name);
            }
        }
    }


    /*public Button[] buttons; // Tableau de chaque bouton de couleur

    private Button lastClickedButton; // Dernier bouton cliqué

    public List<GameObject> FaceChangeColor;
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Variable temporaire pour stocker la valeur de i
            buttons[index].onClick.AddListener(() => OnButtonClick(buttons[index]));
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (clickedButton == lastClickedButton) // Si on clique deux fois sur le même bouton
        {
            // Active tous les boutons
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            }

            lastClickedButton = null; // Réinitialiser le dernier bouton cliqué
        }
        else // Si un nouveau bouton est cliqué
        {
            // Désactive tous les autres boutons
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != clickedButton)
                {
                    buttons[i].interactable = false;
                }
            }

            lastClickedButton = clickedButton; // Mettre à jour le dernier bouton cliqué
            
            switch (clickedButton.name)
            {
                case "Rouge":
                    FaceChangeColor.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case "Bleu":
                    FaceChangeColor.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case "Blanc":
                    FaceChangeColor.GetComponent<Renderer>().material.color = Color.white;
                    break;
                case "Vert":
                    FaceChangeColor.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case "Jaune":
                    FaceChangeColor.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case "Orange":
                    FaceChangeColor.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.0f);
                    break;
                default:
                    break;
            }
        }
    }*/
    
}


